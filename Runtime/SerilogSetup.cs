using Serilog;
using Serilog.Core;

namespace Serilog
{
    public static class SerilogSetup
    {
        public static LoggingLevelSwitch LevelSwitch;

        static SerilogSetup()
        {
            DefaultSetup();   
        }

        public static void DefaultSetup()
        {
            var settings = SerilogSettings.GetOrCreateSettings();
            LevelSwitch = new LoggingLevelSwitch(settings.LogLevel);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.UnityConsole()
                .MinimumLevel.ControlledBy(LevelSwitch)
                .CreateLogger();
        }

    }
}