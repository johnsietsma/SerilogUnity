using Serilog.Core;
using Serilog.Events;
using UnityEngine;

namespace Serilog
{

    public class SerilogSettings : ScriptableObject
    {
        public LogEventLevel LogLevel { get => m_LogLevel; set => m_LogLevel = value; }

        [SerializeField]
        private LogEventLevel m_LogLevel = LogEventLevel.Debug;

        public static SerilogSettings Instance;
        public static LoggingLevelSwitch LevelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);

        public void OnEnable()
        {
            Debug.Log("Enable Serilog");

            LevelSwitch = new LoggingLevelSwitch(LogLevel);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.UnityConsole()
                .MinimumLevel.ControlledBy(LevelSwitch)
                .CreateLogger();

            Instance = this;
        }
    }
}