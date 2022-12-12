using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Config.Configs.GunConfigs;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Extensions;

namespace RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;

public abstract class TurretGun : Gun
{
    #region Fields

    private float _turretRotation;

    private readonly float _defaultRotation;

    #endregion

    #region Properties

    /// <summary>
    /// Current rotation angle of turret. Stays between (-Pi; Pi].
    /// </summary>
    protected float TurretRotation
    {
        get => _turretRotation;
        set
        {
            double buffer = value;
            buffer = buffer switch
            {
                > Math.PI => buffer - 2 * Math.PI,
                <= -Math.PI => buffer + 2 * Math.PI,
                _ => buffer
            };

            _turretRotation = (float)buffer;
        }
    }

    protected new GunTurretConfig Config => (GunTurretConfig)base.Config;

    #endregion

    #region Methods

    protected TurretGun(Vector2 position, string configName, float defaultRotation) : base(position, configName)
    {
        TurretRotation = _defaultRotation = defaultRotation;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //this method draws gun fundament (with base call) 
        //and the turret a bit higher that fundament.
        base.Draw(gameTime, spriteBatch);

        GunTurretConfig cfg = Config;
        Texture2D turretTexture = cfg.TurretTexture;

        spriteBatch.Draw(
            texture: turretTexture,
            position: GetGamePosition(),
            sourceRectangle: GetTurretSourceRectangle(turretTexture),
            color: Color.White,
            rotation: TurretRotation,
            origin: new Vector2(cfg.RotationCenterX, turretTexture.Height / 2f),
            scale: new Vector2(cfg.TurretTextureScale) * GlobalConfig.GameFieldScale,
            effects: SpriteEffects.None,
            layerDepth: DrawLayers.Gun + DrawLayers.Increment);
    }

    /// <summary>
    /// Returns position of barrel end for creating shoot spark mostly.
    /// </summary>
    /// <returns></returns>
    protected Vector2 GetBarrelEnd()
    {
        var cfg = Config;
        var barrelLength = cfg.BarrelLength * cfg.TurretTextureScale;
        
        if (barrelLength <= 0)
            return Position;

        var barrelEnd = Position.MovedInDirection(barrelLength, TurretRotation);
        return barrelEnd;
    }

    /// <summary>
    /// Returns piece of texture to draw as turret.
    /// </summary>
    /// <param name="turretTexture"></param>
    /// <returns></returns>
    protected virtual Rectangle GetTurretSourceRectangle(Texture2D turretTexture)
    {
        return new Rectangle(0, 0, turretTexture.Width, turretTexture.Height);
    }

    /// <summary>
    /// Returns value that is a difference between current gun rotation angle and angle that required to fire to enemy.
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public float GetRotationToTarget(Enemy enemy)
    {
        //get vector between enemy and gun.
        var vector = enemy.Position - Position;

        //get angle based on vector.
        var expectedRotation = (float)Math.Atan2(vector.Y, vector.X);

        return GetRotationToNewAngle(expectedRotation);
    }

    /// <summary>
    /// Returns value that is a difference between current gun rotation angle and expected angle.
    /// </summary>
    /// <param name="expectedRotation"></param>
    /// <returns></returns>
    private float GetRotationToNewAngle(float expectedRotation)
    {
        //get angle between current rotation and expected.
        var diff = expectedRotation - TurretRotation;

        //normalizing angle so gun will always pick the shortest way to target enemy
        double finalDiff = (double)diff switch
        {
            > Math.PI => diff - Math.PI * 2,
            < -Math.PI => diff + Math.PI * 2,
            _ => diff
        };

        return (float)finalDiff;
    }

    /// <summary>
    /// Rotates turret to specified angle at speed of turret rotation. 
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="diffAngle"></param>
    /// <returns></returns>
    private bool RotateTurret(GameTime gameTime, float diffAngle)
    {
        if (diffAngle == 0)
            return true;

        //get maximum angle to rotate on this step.
        float rotationOnThisStep = Config.RotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        //if we can reach expected angle at this step we do it and say rotation is done.
        if (Math.Abs(diffAngle) < rotationOnThisStep)
        {
            TurretRotation += diffAngle;
            return true;
        }

        //otherwise we just rotate this to maximum angle in requested side. 
        if (diffAngle < 0)
            TurretRotation -= rotationOnThisStep;
        else
            TurretRotation += rotationOnThisStep;

        //expected angle not reached yet.
        return false;
    }

    public override void Update(GameTime gameTime)
    {
        //if gun is not loaded, reload it. gun cant shoot at the same step it finishes reloading!.
        var loadedRightNow = Loaded;
        if (!loadedRightNow)
            Reload(gameTime);

        //check if gun has valid target.
        var (hasTarget, canFire) = CheckTarget();

        //if gun has no target it tries to return to default angle.
        if (!hasTarget)
        {
            var diffToDefault = GetRotationToNewAngle(_defaultRotation);
            RotateTurret(gameTime, diffToDefault);
            return;
        }

        //if gun got target in this step or has no target - it can do nothing more.
        if (!canFire) return;

        //if gun has target and can fire right now - gun tries to aim enemy.
        var diffToEnemy = GetRotationToTarget(CurrentTarget!);
        var aimed = RotateTurret(gameTime, diffToEnemy);

        //gun can shoot only if it successfully aimed and WAS reloaded BEFORE this step.  
        if (aimed && loadedRightNow)
            Fire();
    }

    #endregion
}