using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Config.Configs.GunConfigs;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Concrete;
using RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Interfaces;
using RakodilTowerDefense.Extensions;

namespace RakodilTowerDefense.Domain.GameClasses.Guns.Concrete;

public class TeslaTower: NonTurretGun, IExplosionCreator
{
    //todo remove if anything is ok.
    //как будет работать тесла.
    //после установки - заряжен и заряжена вспышка.
    //Если заряжен и заряжена вспышка - рисуем вспышку. Когда время рисования вспышки прошло - вспышка разряжена.
    //Если заряжен, вспышка не заряжена - вспышка заряжается, ее не рисуем.. Когда время зарядки вспышки прошло - вспышка заряжена.
    
    //Если выстрелил - ставим что вспышка разряжена, и время перезарядки - 1.
    //для чего это сделано. если оружие перезаряжается на данном шаге, то оно на нем стрельнуть не сможет. но на следующем сможет.
    //и нужно сделать так чтобы на этом конкретном кадре вспышка покоя НЕ ОТРИСОВАЛАСЬ.
    
    //если разряжен - вспышка не рисуется.
    
    //по совокупности данных условий выходит так, что когда орудие заряжено и ничего не делает - иногда отрисовывается вспышка.
    //если орудие разряжена - вспышка не рисуется.
    //если орудие зарядилось и не выстрелило мгновенно - вспышка будет отрисована.
    
    #region Fields
    
    private const float IDLE_SPARK_ROTATION = 0.4f * (float)Math.PI;
    private const int IDLE_SPARK_MAX_ROTATION = 5;

    /// <summary>
    /// Current behaviour of idle spark.
    /// </summary>
    private IdleSparkMode _idleSparkMode;
    
    /// <summary>
    /// How many time left to switch idle spark behaviour.
    /// </summary>
    private double _idleSparkModeTime;

    private SpriteEffects _idleSparkEffects;

    private float _idleSparkRotation;
    

    #endregion
    
    

    #region Properties

    private new TeslaConfig Config => (TeslaConfig)base.Config;
    
    public event EventHandler<Explosion>? ExplosionCreated;

    #endregion
    
    

    #region Methods

    public TeslaTower(Vector2 position, string configName) : base(position, configName)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        
        if (!Loaded || _idleSparkMode == IdleSparkMode.Reload)
            return;
        
        //drawing idle spark only if its loaded and gun is loaded.
        TeslaConfig cfg = Config;
        Texture2D idleSparkTexture = cfg.IdleSparkTexture;
        
        spriteBatch.Draw(
            texture: idleSparkTexture,
            position: GetGamePosition(),
            sourceRectangle: new Rectangle(0, 0, idleSparkTexture.Width, idleSparkTexture.Height),
            color: Color.White,
            rotation: _idleSparkRotation,
            origin: new Vector2(idleSparkTexture.Width / 2f, idleSparkTexture.Height / 2f),
            scale: new Vector2(cfg.IdleSparkTextureScale) * GlobalConfig.GameFieldScale,
            effects: _idleSparkEffects,
            layerDepth: DrawLayers.Gun + DrawLayers.Increment); //idle spark higher than gun
    }
    
    protected override void Fire()
    {
        //this unloads gun and deals x1 damage to 1st target.
        base.Fire();

        //get another enemies that damaged by lightning jumps.
        var damagedEnemies = GetDamagedEnemies();
        var count = damagedEnemies.Count;
        if (count > 0)
        {
            //that is the damage that 1st enemy received.
            float damage = Config.Damage;
            
            for (int i = 0; i < count; i++)
            {
                //every next enemy gets damage of previous x 0.5.
                damage *= 0.5f;
                damagedEnemies[i].ReceiveDamage(damage);
            }
        }
        
        //add first target to damaged list to create proper lightning.
        damagedEnemies.Insert(0, CurrentTarget!);

        var lightning = new TeslaLightning(Position, damagedEnemies);
        ExplosionCreated?.Invoke(this, lightning);

        //in this combination we assure that spark will be drawn NEXT frame after gun is loaded.
        //so if in next frame gun is shooting => unloading, no one frame of idle spark will be drawn as it is planned.
        _idleSparkMode = IdleSparkMode.Reload;
        _idleSparkModeTime = 1;
    }

    /// <summary>
    /// Returns enemies that were damaged by lightning jumps.
    /// </summary>
    /// <returns></returns>
    private List<Enemy> GetDamagedEnemies()
    {
        var cfg = Config;
        
        var traveledDistanceOfTarget = CurrentTarget!.TraveledDistance;
        
        //take maximum targets that can be hit by lightning jumps
        var targets =
            GetAliveEnemies()
                .Where(e => e.TraveledDistance < traveledDistanceOfTarget)
                .OrderBy(e => e.TraveledDistance)
                .Take(cfg.MaximumTargets);

        var damagedEnemies = new List<Enemy>(cfg.MaximumTargets);
        
        Enemy source = CurrentTarget;
        var jumpRange = cfg.LightningJumpRange;
        foreach (var receiver in targets)
        {
            //if next enemy is not in range of jump stop searching.
            if (source.Position.DistanceTo(receiver.Position) > jumpRange)
                break;
            
            //but if it is in range
            damagedEnemies.Add(receiver);
            
            //next step lightning will jump from current receiver.
            source = receiver;
        }

        return damagedEnemies;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        //if gun is loaded after this step we perform idle spark calculations
        if (Loaded)
            CalculateIdleSparkMode(gameTime);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameTime"></param>
    private void CalculateIdleSparkMode(GameTime gameTime)
    {
        Debug.Assert(Loaded);
        
        //if time to switch mode
        if (_idleSparkModeTime <= 0)
        {
            if (_idleSparkMode == IdleSparkMode.Reload)
            {
                //if spark reloaded so we draw it.
                _idleSparkMode = IdleSparkMode.Draw;
                _idleSparkModeTime = Config.IdleSparkDrawTime;

                GetRandomIdleSparkState();
            }
            else //if Draw
            {
                _idleSparkMode = IdleSparkMode.Reload;
                _idleSparkModeTime = Config.IdleSparkReloadTime;
            }
            return;
        }

        //else if theres some time left lower it.
        _idleSparkModeTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
    }

    private void GetRandomIdleSparkState()
    {
        var random = new Random();
        
        //get effect - spark flips or not.
        var sparkFlip = random.Next(2);
        _idleSparkEffects = sparkFlip switch
        {
            0 => SpriteEffects.None,
            _ => SpriteEffects.FlipHorizontally
        };

        int sparkFrame = random.Next(IDLE_SPARK_MAX_ROTATION);
        _idleSparkRotation = IDLE_SPARK_ROTATION * sparkFrame;
    }


    #endregion
    
    
    private enum IdleSparkMode
    {
        Draw,
        Reload
    }

}