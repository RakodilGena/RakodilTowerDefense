using System;
using Microsoft.Xna.Framework;

namespace RakodilTowerDefense.Extensions;

public static class Vector2Extensions
{
    /// <summary>
    /// Returns new vector that is vector moved towards specified direction at a specified distance. 
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="distance"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector2 MovedInDirection(this Vector2 vector, float distance, float direction)
    {
        return vector + new Vector2(
            x: (float)Math.Cos(direction) * distance,
            y: (float)Math.Sin(direction) * distance);
    }

    /// <summary>
    /// Returns distance to specified vector.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="anotherVector"></param>
    /// <returns></returns>
    public static float DistanceTo(this Vector2 vector, Vector2 anotherVector)
    {
        return (vector - anotherVector).Length();
    }
}