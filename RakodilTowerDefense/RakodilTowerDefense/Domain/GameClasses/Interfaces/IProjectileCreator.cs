using System;
using RakodilTowerDefense.Domain.GameClasses.Projectiles;

namespace RakodilTowerDefense.Domain.GameClasses.Interfaces;

/// <summary>
/// Implementor of this can create projectiles. 
/// </summary>
public interface IProjectileCreator
{
    /// <summary>
    /// Projectile created.
    /// </summary>
    public event EventHandler<Projectile> ProjectileCreated;
}