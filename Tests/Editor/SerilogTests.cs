using NUnit.Framework;
using UnityEngine;

namespace Serilog.Editor.Tests
{
    public class SerilogTests
    {
        [Test]
        public void UnityConsoleSinkReceivesMessage()
        {
            bool logReceived = false;
            Application.logMessageReceived += (logString, stackTrace, type) => logReceived = true;
            var logger = new LoggerConfiguration()
                .WriteTo.UnityConsole()
                .MinimumLevel.Debug()
                .CreateLogger();
            logger.Debug("Test");
            Assert.That(logReceived, Is.True);

        }
    }
}