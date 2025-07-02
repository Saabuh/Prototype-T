using UnityEngine;

namespace Prototype_S
{
/// <summary>
/// A custom, conditional logger with a runtime toggle.
/// </summary>
public static class Log
{
    // THIS IS THE RUNTIME SWITCH.
    // It's 'public' so other scripts can change it.
    // It's 'static' so it can be accessed via Log.IsEnabled.
    // It defaults to true, so logs are on by default in a debug build.
    public static bool IsEnabled = true;

    // The [Conditional] attribute is the COMPILE-TIME switch.
    // If ENABLE_LOGGING is not defined, this entire method and all calls to it are removed.
    [System.Diagnostics.Conditional("ENABLE_LOGGING")]
    public static void Info(object message)
    {
        // This is the RUNTIME check.
        // Even in a debug build, this method will do nothing if the flag is false.
        if (!IsEnabled) return;

        Debug.Log(message);
    }

    [System.Diagnostics.Conditional("ENABLE_LOGGING")]
    public static void Warning(object message)
    {
        if (!IsEnabled) return;

        Debug.LogWarning(message);
    }

    // We still want errors to always show up, so we don't modify this one.
    public static void Error(object message)
    {
        Debug.LogError(message);
    }
}
}
