// C# editor script
using UnityEngine;
using UnityEditor;
using System.Reflection;

static class UsefulShortcuts
{
    [MenuItem("Tools/Clear Console %#v")] // CMD + SHIFT + V
    static void ClearConsole()
    {
        // var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
        // var type = assembly.GetType("UnityEditorInternal.LogEntries");
        // var method = type.GetMethod("Clear");
        // method.Invoke(new object(), null);

        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");

        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

        clearMethod.Invoke(null, null);

    }
}