using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.CommonInterfaces;

namespace RakodilTowerDefense.Domain.GameClasses;

public abstract class GameObject: CommonObject
{
    #region Methods

    protected GameObject(Vector2 position, string configName):base(position, configName)
    {
        
    }

    /// <summary>
    /// Returns actual position on visible game field.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetGamePosition()
    {
        var normalPosition = Position * GlobalConfig.GameFieldScale;
        normalPosition.X += GlobalConfig.GamePadding;
        return normalPosition;
    }

    /// <summary>
    /// Returns actual position on visible game field.
    /// </summary>
    /// <returns></returns>
    protected static Vector2 GetGamePosition(Vector2 position)
    {
        var normalPosition = position * GlobalConfig.GameFieldScale;
        normalPosition.X += GlobalConfig.GamePadding;
        return normalPosition;
    }

    #endregion
    
}