using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions.Concrete;

public class LaserShot: BeamShot
{
    public LaserShot(Vector2 sparkPosition, Vector2 explosionPosition, float rotation) 
        : base(explosionPosition, 
            configName: ConfigNames.Explosions.Laser, 
            rotation, sparkPosition)
    {
    }
}