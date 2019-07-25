using Serilog;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class InitSerilogOnLoad : MonoBehaviour
{
    static InitSerilogOnLoad()
    {
        Debug.Log("Loading Serilog");
        SerilogSetup.CreateLogger();
    }
}
