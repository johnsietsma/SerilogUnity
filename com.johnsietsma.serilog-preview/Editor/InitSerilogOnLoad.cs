using Serilog;
using UnityEditor;
using UnityEngine;

namespace Serilog.Editor
{
    // Handle editor case
    [InitializeOnLoad]
    public class InitSerilogOnLoad : MonoBehaviour
    {
        static InitSerilogOnLoad()
        {
            SerilogSettingsProvider.GetOrCreateSettings();
        }
    }
}