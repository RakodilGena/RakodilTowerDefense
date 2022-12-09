using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions.Concrete;

public class GatlingShot: ExplosionWithSpark
{
    public GatlingShot(Vector2 sparkPosition, Vector2 explosionPosition, float rotation) 
        : base(explosionPosition, 
            configName: ConfigNames.Explosions.Gatling, 
            rotation, sparkPosition)
    {
    }
}