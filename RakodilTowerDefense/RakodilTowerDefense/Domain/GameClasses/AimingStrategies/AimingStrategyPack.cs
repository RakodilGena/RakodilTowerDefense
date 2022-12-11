using System.Collections.Generic;

namespace RakodilTowerDefense.Domain.GameClasses.AimingStrategies;

/// <summary>
/// 
/// </summary>
public static class AimingStrategyPack
{
    /// <summary>
    /// Returns pack of aiming strategies.
    /// </summary>
    /// <returns></returns>
    public static IDictionary<string, AimingStrategy> GetStrategies()
    {
        var dict = new Dictionary<string, AimingStrategy>(4)
        {
            { "Closest", new AimingStrategyClosest() },
            { "Fastest", new AimingStrategyFastest() },
            { "First", new AimingStrategyFirst() },
            { "Rank", new AimingStrategyRank() }
        };

        return dict;
    }
}