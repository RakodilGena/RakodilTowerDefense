using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions.Concrete;

public class EnemyExplosion: Explosion
{
    public EnemyExplosion(Vector2 position) 
        : base(position, 
            rotation: 0,
            configName: ConfigNames.Explosions.Enemy)
    {
    }
}