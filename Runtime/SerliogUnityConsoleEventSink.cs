using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using UnityEngine;


public static class SerilogUnityConsoleExtensions
{
    /// <summary>
    /// Add a sink that writes to the Unity console.
    /// <code>
    /// Log.Logger = new LoggerConfiguration()
    /// .WriteTo.UnityConsole()
    /// .CreateLogger();
    /// </code>
    /// </summary>
    public static LoggerConfiguration UnityConsole(this LoggerSinkConfiguration sinkConfiguration)
    {
        return sinkConfiguration.Sink(new SerliogUnityConsoleEventSink());
    }
}

/// <summary>
/// Serilog event sink that writes to the Unity console.
/// </summary>
public class SerliogUnityConsoleEventSink : ILogEventSink
{
    [System.Diagnostics.DebuggerHidden]
    public void Emit(LogEvent logEvent)
    {
        switch (logEvent.Level)
        {
            case LogEventLevel.Debug: // fallthrough
            case LogEventLevel.Verbose: // fallthrough
            case LogEventLevel.Information:
                Debug.Log(logEvent.MessageTemplate.Render(logEvent.Properties));
                break;
            case LogEventLevel.Warning:
                Debug.LogWarning(logEvent.MessageTemplate.Render(logEvent.Properties));
                break;
            case LogEventLevel.Error: // fallthrough
            case LogEventLevel.Fatal:
                Debug.LogError(logEvent.MessageTemplate.Render(logEvent.Properties));
                break;
        }

    }
}
