using System;

namespace RakodilTowerDefense.VersionControl;

/// <summary>
/// Stores current app version.
/// </summary>
public class AppVersion
{
    /// <summary>
    /// Returns current app version.
    /// </summary>
    public static Version Current => GetInstance()._version;
    
    private static AppVersion? _instance;
    private static AppVersion GetInstance() => _instance ??= new AppVersion();

    private AppVersion()
    {
        _version = new Version(0, 1, 2);
    }

    private readonly Version _version;
}