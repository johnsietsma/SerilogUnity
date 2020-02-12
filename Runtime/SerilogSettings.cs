using Serilog.Core;
using Serilog.Events;
using UnityEngine;

namespace Serilog
{

    /// <summary>
    /// Holds the log level.
    /// Sets up a default logging configuration that writes to the Unity Console.
    /// Change this by assigning to <c>Serilog.Log.Logger</c>.
    /// </summary>
    public class SerilogSettings : ScriptableObject
    {
        public static SerilogSettings Instance;

        public LogEventLevel LogLevel { get => m_LogLevel; set { m_LogLevel = value; m_LevelSwitch.MinimumLevel = value; } }

        [SerializeField]
        LogEventLevel m_LogLevel = LogEventLevel.Debug;
        
        // The level switch allows changing the level dynamically
        LoggingLevelSwitch m_LevelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);

        public void OnEnable()
        {
            m_LevelSwitch = new LoggingLevelSwitch(LogLevel);

            // Setup a default logging configuration
            Log.Logger = new LoggerConfiguration()
                .WriteTo.UnityConsole()
                .MinimumLevel.ControlledBy(m_LevelSwitch)
                .CreateLogger();

            Instance = this;
        }
    }
}