#nullable enable
using System.Collections.Generic;

namespace RakodilTowerDefense.Domain.AimingStrategies;

public abstract class AimingStrategy
{
    /// <summary>
    /// Returns most suitable enemy for targeting. 
    /// </summary>
    /// <param name="enemies"></param>
    /// <returns></returns>
    public abstract Enemy? GetTarget(IEnumerable<Enemy> enemies);
}