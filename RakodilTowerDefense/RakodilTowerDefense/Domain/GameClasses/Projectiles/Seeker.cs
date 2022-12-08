﻿using System;
using Microsoft.Xna.Framework;
using RakodilTowerDefense.Config.Configs.ProjectileConfigs;
using RakodilTowerDefense.Domain.GameClasses.Enemies;

namespace RakodilTowerDefense.Domain.GameClasses.Projectiles;

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

    protected abstract void OnTargetReached(object? sender, EventArgs e);

    public override void Update(GameTime gameTime)
    {
        //this will lead seeker to target and raise event when enemy is reached

        var config = Config;

        //get distance vector and rotation out of it.
        var distance = Position - Target.Position;

        
        //get distance addition on this step.
        var distanceAddition = config.Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        //if this seeker could reach enemy at this step
        if (distance.Length() - distanceAddition <= config.CollisionRange)
        {
            //telling that enemy is reached
            TargetReached.Invoke(this, EventArgs.Empty);
            
            //and removing seeker since it destroyed on hit.
            Remove();
            
            return;
        }
        
        //if cant reach enemy yet -> change rotation and position.
        Rotation = (float)Math.Atan2(distance.Y, distance.X);

        Position += new Vector2(
            x: (float)Math.Cos(Rotation) * distanceAddition,
            y: (float)Math.Sin(Rotation) * distanceAddition);
    }

    #endregion
}