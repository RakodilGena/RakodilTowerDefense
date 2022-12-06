using Microsoft.Xna.Framework;

namespace RakodilTowerDefense.Domain;

public interface IRotate
{
    /// <summary>
    /// Rotates this object to certain angle.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="diffAngle">Difference between current and expected angles.</param>
    protected void Rotate(GameTime gameTime, float diffAngle);
}