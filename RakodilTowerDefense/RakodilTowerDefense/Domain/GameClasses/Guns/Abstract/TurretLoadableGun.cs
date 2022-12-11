using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;

/// <summary>
/// Guns of that type has the only addition to TurretGun - its turret shows whether its loaded or unloaded.
/// </summary>
public abstract class TurretLoadableGun: TurretGun
{
    protected TurretLoadableGun(Vector2 position, string configName, float defaultRotation) : base(position, configName, defaultRotation)
    {
    }

    protected override Rectangle GetTurretSourceRectangle(Texture2D turretTexture)
    {
        //if turret is loaded - show second frame of texture. unloaded - show first.
        var width = turretTexture.Width / 2;
        var rectangleStartX = Loaded ? width : 0;

        return new Rectangle(rectangleStartX, 0, width, turretTexture.Height);
    }
}