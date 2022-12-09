using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions.Concrete;

public class CannonShot: ExplosionWithSpark
{
    public CannonShot( Vector2 sparkPosition, Vector2 explosionPosition, float rotation) 
        : base(explosionPosition, 
            configName: ConfigNames.Explosions.Cannon, 
            rotation, sparkPosition)
    {
    }
}