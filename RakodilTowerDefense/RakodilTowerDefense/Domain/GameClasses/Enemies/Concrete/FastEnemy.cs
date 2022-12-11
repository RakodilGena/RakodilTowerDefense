using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;

namespace RakodilTowerDefense.Domain.GameClasses.Enemies.Concrete;

public class FastEnemy: Enemy
{
    public FastEnemy(Vector2 position, float initialRotation) 
        : base(
            position, 
            initialRotation,
            configName: ConfigNames.Enemies.Fast)
    {
    }
}