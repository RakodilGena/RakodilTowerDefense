using Microsoft.Xna.Framework;

namespace RakodilTowerDefense.Domain.CommonInterfaces;

public interface IRotate
{
    /// <summary>
    /// Rotates this object to certain angle. Returns value that indicates if expected angle reached.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="diffAngle">Difference between current and expected angles.</param>
    public bool Rotate(GameTime gameTime, float diffAngle);
}