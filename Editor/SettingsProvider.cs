using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Serilog.Editor
{
    /// <summary>
    /// A Serilog settings panel inside Project Settings
    /// </summary>
    static class SettingsProvider
    {
        static readonly string SettingsDirectory = "Settings/";

        /// <summary>
        /// The settings save location. This is inside the "Assets" folder like the SRP settings, not in "ProjectSettings"
        /// like "Graphics","Player", etc. This means it can be checked in and I can use a ScriptableObject to store the
        /// data. rather then writing json files. 
        /// </summary>
        static readonly string SettingsFile = $"Assets/{SettingsDirectory}SerilogSettings.asset";

        // The settings data
        static SerilogSettings m_Settings;

        static SettingsProvider()
        {
            TryCreateSettings();
        }
        
        [SettingsProvider]
        public static UnityEditor.SettingsProvider CreateSettingsProvider()
        {
            var provider = new UnityEditor.SettingsProvider("Project/Serilog", SettingsScope.Project)
            {
                label = "Serilog",

                activateHandler = (searchContext, rootElement) =>
                {
                    m_Settings = SerilogSettings.Instance;
                    var logLevelField = new EnumField("Log Level", m_Settings.LogLevel);
                    logLevelField.RegisterCallback<ChangeEvent<Enum>>(SerilogSettings_OnValueChanged);
                    rootElement.Add(logLevelField);
                },
                keywords = new HashSet<string>(new[] { "Logging", "Serilog","Log Level" })
            };

            return provider;
        }

        static void TryCreateSettings()
        {
            m_Settings = AssetDatabase.LoadAssetAtPath<SerilogSettings>(SettingsFile);
            if (m_Settings == null)
            {
                m_Settings = ScriptableObject.CreateInstance<SerilogSettings>();

                string settingsPath = Application.dataPath + "/" + SettingsDirectory;
                if (!Directory.Exists(settingsPath)) Directory.CreateDirectory(settingsPath);
                AssetDatabase.CreateAsset(m_Settings, SettingsFile);
                AssetDatabase.SaveAssets();

                var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
                preloadedAssets.Add(m_Settings);
                PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            }
        }

        static void SerilogSettings_OnValueChanged( ChangeEvent<Enum> evt )
        {
            m_Settings.LogLevel = (LogEventLevel)evt.newValue;
        }
    }

}
 