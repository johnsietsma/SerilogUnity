using Serilog.Events;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Serilog.Editor
{
    static class SerilogSettingsUIElementsRegister
    {
        private static SerilogSettings settings;

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Settings window for the Project scope.
            var provider = new SettingsProvider("Project/Serilog", SettingsScope.Project)
            {
                label = "Serilog",
                // activateHandler is called when the user clicks on the Settings item in the Settings window.
                activateHandler = (searchContext, rootElement) =>
                {
                    settings = SerilogSettings.GetOrCreateSettings();
                    var logLevelField = new EnumField("Log Level", settings.LogLevel);
                    logLevelField.RegisterCallback<ChangeEvent<Enum>>(SerilogSettings_OnValueChanged);
                    rootElement.Add(logLevelField);

                    //var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Serilog/Editor/Serilog.uss");
                    //rootElement.styleSheets.Add(styleSheet);
                },

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(new[] { "Log Level" })
            };

            return provider;
        }

        private static void SerilogSettings_OnValueChanged( ChangeEvent<Enum> evt )
        {
            if (SerilogSetup.LevelSwitch == null) SerilogSetup.LevelSwitch = new Serilog.Core.LoggingLevelSwitch(LogEventLevel.Debug);
            var logLevel = (LogEventLevel)evt.newValue;
            SerilogSetup.LevelSwitch.MinimumLevel = logLevel;
            settings.LogLevel = logLevel;
        }
    }

}
 