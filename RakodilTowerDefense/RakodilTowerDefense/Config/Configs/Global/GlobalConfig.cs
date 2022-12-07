using System;

namespace RakodilTowerDefense.Config.Configs.Global;

public sealed class GlobalConfig
{
    private float 
        _resolutionScale = 1.0f,
        _gameFieldScale = 1.0f,
        _padding = 0;
    
    

    private static GlobalConfig? _instance;

    /// <summary>
    /// Scale that applies to non-game - components (controls backgrounds etc).
    /// </summary>
    public static float ResolutionScale
    {
        get => GetInstance()._resolutionScale;
        set
        {
            if (value is > 0 and <= 1)
                GetInstance()._resolutionScale = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value), "Resolution scale was out of range (0; 1]");
        }
    }

    /// <summary>
    ///  Scale that applies to game - components (guns enemies etc).
    /// </summary>
    public static float GameFieldScale
    {
        get => GetInstance()._gameFieldScale;
        set
        {
            if (value is > 0 and <= 1)
                GetInstance()._gameFieldScale = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value), "Game field scale was out of range (0; 1]");
        }
    }

    /// <summary>
    /// Padding that applies to game - components (guns enemies etc).
    /// </summary>
    public static float GamePadding
    {
        get => GetInstance()._padding;
        set
        {
            if (value > 0)
                GetInstance()._padding = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value), "Game padding was out of range (less than zero)");
        }
    }
    
    private static GlobalConfig GetInstance()
    {
        return _instance ??= new GlobalConfig();
    }

}