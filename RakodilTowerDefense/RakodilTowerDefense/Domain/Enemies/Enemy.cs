using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.EnemyConfigs;

namespace RakodilTowerDefense.Domain.Enemies;

public abstract class Enemy
{
    #region Fields

    private float _direction;

    private float _currentHP;
    
    protected static readonly EnemyConfig Config;

    #endregion

    #region Properties

    /// <summary>
    /// Current enemy position;
    /// </summary>
    public Vector2 Position { get; private set; }
    
    /// <summary>
    /// Total distance traveled by this enemy.
    /// </summary>
    public float TraveledDistance { get; private set; }

    /// <summary>
    /// Returns moving speed of enemy.
    /// </summary>
    public float MovingSpeed => Config.MovingSpeed;

    /// <summary>
    /// Returns enemy rank.
    /// </summary>
    public int Rank => Config.Rank;

    #endregion

    #region Methods



    #endregion



}