using System;
using RakodilTowerDefense.Domain.GameClasses.Explosions;

namespace RakodilTowerDefense.Domain.GameClasses.Interfaces;

/// <summary>
/// Implementor of this can create explosions. 
/// </summary>
public interface IExplosionCreator
{
    /// <summary>
    /// Explosion created.
    /// </summary>
    public event EventHandler<Explosion> ExplosionCreated;
}