using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.EnemyConfigs;

namespace RakodilTowerDefense.Domain.GameClasses.Enemies;

public abstract class Enemy:GameObject
{
    #region Fields

    private float _direction;

    private float _currentHP;
    
    protected static readonly EnemyConfig Config;

    #endregion

    #region Properties

    /// <summary>
    /// Returns flag that says if enemy is alive.
    /// </summary>
    public bool IsAlive => _currentHP > 0;

    /// <summary>
    /// Returns moving speed of enemy.
    /// </summary>
    public float MovingSpeed => Config.MovingSpeed;

    /// <summary>
    /// Returns enemy rank.
    /// </summary>
    public int Rank => Config.Rank;
    
    /// <summary>
    /// Total distance traveled by this enemy.
    /// </summary>
    public float TraveledDistance { get; private set; }

    #endregion

    #region Methods


    protected Enemy(Vector2 position, string configName) : base(position, configName)
    {
        
    }
    
    public void ReceiveDamage(float damage)
    {
        if (damage<=0)
            return;

        _currentHP -= damage;
        //todo Add code to destroy if hp < 0 and create explosion.
    }

    #endregion
}