using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RakodilTowerDefense.Config;
using RakodilTowerDefense.Config.JsonConfigs;
using RakodilTowerDefense.Content.TexturesLoaders;
using RakodilTowerDefense.Domain;
using RakodilTowerDefense.Domain.CommonEventArgs;
using RakodilTowerDefense.Domain.GameClasses.AimingStrategies;
using RakodilTowerDefense.Domain.GameClasses.Enemies;
using RakodilTowerDefense.Domain.GameClasses.Enemies.Concrete;
using RakodilTowerDefense.Domain.GameClasses.Explosions.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Guns.Abstract;
using RakodilTowerDefense.Domain.GameClasses.Guns.Concrete;
using RakodilTowerDefense.Domain.GameClasses.Interfaces;
using RakodilTowerDefense.Domain.GameClasses.Projectiles.Abstract;

namespace RakodilTowerDefense;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();

        SetFullScreen();

        var textures = GameTexturesLoader.GetTextures(Content);
        var strategies = AimingStrategyPack.GetStrategies();
        ConfigFactory.Initialize(textures, strategies);

        _enemies = new List<Enemy>{
            new FastEnemy(new Vector2(100, 540), 0){TraveledDistance = 0},
            
            new StandardEnemy(new Vector2(400, 540), 0){TraveledDistance = 300},
            
            new DurableEnemy(new Vector2(700, 540), 0){TraveledDistance = 600},
            
            new BossEnemy(new Vector2(1000, 540), 0){TraveledDistance = 900},
            };
        

        var cannon = new CannonTower(new Vector2(900, 100), MathHelper.ToRadians(90));
        var gatling = new GatlingTower(new Vector2(900, 100), MathHelper.ToRadians(-90));
        var laser = new LaserGun(new Vector2(900, 100), MathHelper.ToRadians(0));
        var tesla = new TeslaTower(new Vector2(900, 900));
        var missile = new MissileTower(new Vector2(900, 900), MathHelper.ToRadians(90));
        _guns = new List<Gun>
        {
            //cannon, 
            // gatling, 
             laser, 
            // tesla, 
            // missile
        };

        foreach (var g in _guns)
        {
            g.AskedForEnemies += OnAskedForEnemies;
            
            if (g is IExplosionCreator exc)
                exc.ExplosionCreated += OnExplosionAdded;
            
            if (g is IProjectileCreator prc)
                prc.ProjectileCreated += OnProjectileAdded;
        }

        _projectiles = new List<Projectile>();
        _projectilesToAdd = new List<Projectile>();
        _projectilesToRemove = new List<Projectile>();
        
        _explosions = new List<Explosion>();
        _explosionsToAdd = new List<Explosion>();
        _explosionsToRemove = new List<Explosion>();
    }

    private void SetFullScreen()
    {
        _graphics.HardwareModeSwitch = false;
        var screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        var screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferHeight = screenHeight;
        _graphics.PreferredBackBufferWidth = screenWidth;
        
        _graphics.ApplyChanges();
        _graphics.HardwareModeSwitch = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }


    private List<Enemy> _enemies;
    private List<Gun> _guns;
    private List<Projectile> _projectiles, _projectilesToAdd, _projectilesToRemove;
    private List<Explosion> _explosions, _explosionsToAdd, _explosionsToRemove;
    
    
    private void OnAskedForEnemies(object? sender, EnemiesEventArgs e)
    {
        e.Enemies = _enemies;
    }

    private void OnProjectileAdded(object? sender, Projectile p)
    {
        _projectilesToAdd.Add(p);
        p.Removed += OnProjectileRemoved;

        if (p is ITargeting it)
            it.AskedForEnemies += OnAskedForEnemies;

        if (p is IExplosionCreator c)
            c.ExplosionCreated += OnExplosionAdded;
    }
    
    private void OnProjectileRemoved(object? sender, EventArgs e)
    {
        if (sender is not Projectile p)
            return;
        _projectilesToRemove.Add(p);
    }

    private void OnExplosionAdded(object? sender, Explosion e)
    {
        _explosionsToAdd.Add(e);
        e.Removed += OnExplosionRemoved;
    }
    
    private void OnExplosionRemoved(object? sender, EventArgs e)
    {
        if (sender is not Explosion ex)
            return;
        _explosionsToRemove.Add(ex);
    }

    private IEnumerable<CommonObject> GetAllObjects()
    {
        foreach (var enemy in _enemies)
            yield return enemy;

        foreach (var gun in _guns)
            yield return gun;

        foreach (var e in _explosions)
            yield return e;

        foreach (var p in _projectiles)
            yield return p;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        foreach (var obj in GetAllObjects())
            obj.Update(gameTime);
        
        _projectiles.AddRange(_projectilesToAdd);
        _projectiles.RemoveAll(c => _projectilesToRemove.Contains(c));
        
        _explosions.AddRange(_explosionsToAdd);
        _explosions.RemoveAll(c => _explosionsToRemove.Contains(c));

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        //SpriteSortMode.FrontToBack
        _spriteBatch.Begin();
        
        // TODO: Add your drawing code here
        foreach (var obj in GetAllObjects())
        {
            obj.Draw(gameTime, _spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}