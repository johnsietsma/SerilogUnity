using Serilog.Events;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PointClouds
{

    // Create a new type of Settings Asset.
    public class SerilogSettings : ScriptableObject
    {
        public static readonly string SETTINGS_DIRECTORY = "Settings/";
        public static readonly string SETTINGS_FILE = $"Assets/{SETTINGS_DIRECTORY}SerilogSettings.asset";

        public LogEventLevel LogLevel { get => m_LogLevel; set => m_LogLevel = value; }

        [SerializeField]
        private LogEventLevel m_LogLevel;

        public static SerilogSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<SerilogSettings>(SETTINGS_FILE);
            if (settings == null)
            {
                settings = CreateInstance<SerilogSettings>();

                settings.m_LogLevel = LogEventLevel.Debug;

                string settingsPath = Application.dataPath + "/" + SETTINGS_DIRECTORY;
                if (!Directory.Exists(settingsPath)) Directory.CreateDirectory(settingsPath);
                AssetDatabase.CreateAsset(settings, SETTINGS_FILE);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }
    }

}