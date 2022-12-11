using System;
using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Config.Configs.ProjectileConfigs;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Extensions;

namespace RakodilTowerDefense.Domain.GameClasses.Projectiles.Abstract;

/// <summary>
/// Abstract projectile that follows its target.
/// </summary>
public abstract class Seeker: Projectile
{

    #region Fields

    

    #endregion

    #region Properties

    private new SeekerConfig Config => (SeekerConfig)base.Config;
    
    protected Enemy Target { get; }

    private event EventHandler TargetReached; 

    #endregion

    
    #region Methods

    protected Seeker(
        Vector2 position, 
        string configName, 
        float rotation,
        Enemy target) 
        : base(position, configName, rotation)
    {
        Target = target;
        
        TargetReached += OnTargetReached;
    }

    /// <summary>
    /// Actions to perform after target is reached. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected abstract void OnTargetReached(object? sender, EventArgs e);
    

    public override void Update(GameTime gameTime)
    {
        //this will lead seeker to target and raise event when enemy is reached

        var config = Config;

        //get distance vector and rotation out of it.
        var distance = Target.Position - Position;

        
        //get distance addition on this step.
        var distanceAddition = config.Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        //if this seeker could reach enemy at this step
        if (distance.Length() - distanceAddition <= config.CollisionRange * config.TextureScale)
        {
            //telling that enemy is reached
            TargetReached.Invoke(this, EventArgs.Empty);
            
            //and removing seeker since it destroyed on hit.
            Remove();
            
            return;
        }
        
        //if cant reach enemy yet -> change rotation and position.
        Rotation = (float)Math.Atan2(distance.Y, distance.X);

        Position = Position.MovedInDirection(distanceAddition, Rotation);
    }

    #endregion
}