using System;
using RakodilTowerDefense.Domain.CommonEventArgs;

namespace RakodilTowerDefense.Domain.GameClasses.Interfaces;

/// <summary>
/// Implementor of this could ask for alive enemies.
/// </summary>
public interface ITargeting
{
    /// <summary>
    /// Raised when object asking for alive enemies.
    /// </summary>
    public event EventHandler<EnemiesEventArgs> AskedForEnemies;
}