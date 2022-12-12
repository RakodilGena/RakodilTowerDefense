using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using RakodilTowerDefense.Config.Configs;
using RakodilTowerDefense.Config.Configs.ButtonConfigs;
using RakodilTowerDefense.Config.Configs.EnemyConfigs;
using RakodilTowerDefense.Config.Configs.ExplosionsConfig;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Config.Configs.GunConfigs;
using RakodilTowerDefense.Config.Configs.ProjectileConfigs;
using RakodilTowerDefense.Config.JsonConfigs;
using RakodilTowerDefense.Domain.GameClasses.AimingStrategies;
using RakodilTowerDefense.Exceptions.ConfigFactory;

namespace RakodilTowerDefense.Config;

/// <summary>
/// Class - container for configs of objects.
/// </summary>
public class ConfigFactory
{
    #region Fields

    private static ConfigFactory? _factory;

    private readonly IDictionary<string, CommonConfig> _configs;

    private const string PATH_TO_CONFIG = @"./Config/config.json";
    
    #endregion

    #region Methods

    /// <summary>
    /// Returns config with specified name.
    /// </summary>
    /// <param name="configName"></param>
    /// <returns></returns>
    public static CommonConfig GetConfig(string configName)
    {
        return _factory?._configs[configName] ?? throw new ConfigFactoryNotInitializedException();
    }

    /// <summary>
    /// Initializes config factory.
    /// </summary>
    /// <param name="textures"></param>
    /// <param name="aimingStrategies"></param>
    public static void Initialize(
        IDictionary<string, Texture2D> textures,
        IDictionary<string, AimingStrategy> aimingStrategies)
    {
        _factory = new ConfigFactory(textures, aimingStrategies);
    }


    private ConfigFactory(
        IDictionary<string, Texture2D> textures,
        IDictionary<string, AimingStrategy> aimingStrategies)
    {
        //read JSON-file.
        var keeper = ReadJsonConfig();
        
        //get configs
        _configs = GetConfigs(keeper, textures, aimingStrategies);
    }


    /// <summary>
    /// Reads config file and returns deserialized config from it.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ConfigFileNotFoundException"></exception>
    /// <exception cref="ConfigFileEmptyException"></exception>
    private static AllConfigsKeeperJson ReadJsonConfig()
    {
        if (!File.Exists(PATH_TO_CONFIG))
            throw new ConfigFileNotFoundException();

        using StreamReader r = new StreamReader(PATH_TO_CONFIG);

        string json = r.ReadToEnd();
        AllConfigsKeeperJson? keeper = JsonConvert.DeserializeObject<AllConfigsKeeperJson>(json);
        return keeper ?? throw new ConfigFileEmptyException();
    }

    /// <summary>
    /// Returns dictionary of configs with key of its unique names which are used to fetch them later.
    /// </summary>
    /// <param name="keeper"></param>
    /// <param name="textures"></param>
    /// <param name="aimingStrategies"></param>
    /// <returns></returns>
    private static IDictionary<string, CommonConfig> GetConfigs(
        AllConfigsKeeperJson keeper,
        IDictionary<string, Texture2D> textures,
        IDictionary<string, AimingStrategy> aimingStrategies)
    {
        var dictionary = new Dictionary<string, CommonConfig>();
        AddEnemyConfigs(dictionary, keeper, textures);
        AddExplosionConfigs(dictionary, keeper, textures);
        AddProjectileConfigs(dictionary, keeper, textures);
        AddGunConfigs(dictionary, keeper, aimingStrategies, textures);
        AddButtonConfigs(dictionary, keeper, textures);
        AddGunPanelIconConfigs(dictionary, keeper, textures);
        AddBackgroundConfigs(dictionary, keeper, textures);
        
        return dictionary;
    }


    private static void AddEnemyConfigs(
        IDictionary<string, CommonConfig> dictionary, 
        AllConfigsKeeperJson keeper, 
        IDictionary<string, Texture2D> textures)
    {
        dictionary.Add(ConfigNames.Enemies.Standard, new EnemyConfig(keeper.Enemies.Standard, textures));
        
        dictionary.Add(ConfigNames.Enemies.Fast, new EnemyConfig(keeper.Enemies.Fast, textures));
        
        dictionary.Add(ConfigNames.Enemies.Durable, new EnemyConfig(keeper.Enemies.Durable, textures));
        
        dictionary.Add(ConfigNames.Enemies.Boss, new EnemyConfig(keeper.Enemies.Boss, textures));
    }

    private static void AddExplosionConfigs(
        IDictionary<string, CommonConfig> dictionary, 
        AllConfigsKeeperJson keeper, 
        IDictionary<string, Texture2D> textures)
    {
        dictionary.Add(ConfigNames.Explosions.Gatling, new ExplosionWithSparkConfig(keeper.Explosions.Gatling, textures));
        
        dictionary.Add(ConfigNames.Explosions.Cannon, new ExplosionWithSparkConfig(keeper.Explosions.Cannon, textures));
        
        dictionary.Add(ConfigNames.Explosions.Laser, new BeamShotConfig(keeper.Explosions.Laser, textures));
        
        dictionary.Add(ConfigNames.Explosions.Tesla, new TeslaLightningConfig(keeper.Explosions.TeslaLightning, textures));
        
        dictionary.Add(ConfigNames.Explosions.Missile, new ExplosionConfig(keeper.Explosions.Missile, textures));
        
        dictionary.Add(ConfigNames.Explosions.Enemy, new ExplosionConfig(keeper.Explosions.Enemy, textures));
    }

    private static void AddProjectileConfigs(
        IDictionary<string, CommonConfig> dictionary,
        AllConfigsKeeperJson keeper,
        IDictionary<string, Texture2D> textures)
    {
        dictionary.Add(ConfigNames.Projectiles.Missile, new MissileConfig(keeper.Projectiles.Missile, textures));
    }

    private static void AddGunConfigs(
        IDictionary<string, CommonConfig> dictionary,
        AllConfigsKeeperJson keeper,
        IDictionary<string, AimingStrategy> aimingStrategies,
        IDictionary<string, Texture2D> textures)
    {
        dictionary.Add(ConfigNames.Guns.Cannon, new GunTurretConfig(keeper.Guns.Cannon, textures, aimingStrategies));
        
        dictionary.Add(ConfigNames.Guns.Gatling, new GatlingConfig(keeper.Guns.Gatling, textures, aimingStrategies));
        
        dictionary.Add(ConfigNames.Guns.Laser, new GunTurretConfig(keeper.Guns.Laser, textures, aimingStrategies));
        
        dictionary.Add(ConfigNames.Guns.Tesla, new TeslaConfig(keeper.Guns.Tesla, textures, aimingStrategies));
        
        dictionary.Add(ConfigNames.Guns.Missile, new GunTurretConfig(keeper.Guns.Missile, textures, aimingStrategies));
    }


    private static void AddButtonConfigs(
        IDictionary<string, CommonConfig> dictionary,
        AllConfigsKeeperJson keeper,
        IDictionary<string, Texture2D> textures)
    {
        dictionary.Add(ConfigNames.Buttons.CommonWhite, new CommonConfig(keeper.Buttons.CommonWhite, textures));
        dictionary.Add(ConfigNames.Buttons.CommonBlack, new CommonConfig(keeper.Buttons.CommonBlack, textures));
        dictionary.Add(ConfigNames.Buttons.Cancel, new CommonConfig(keeper.Buttons.Cancel, textures));
        dictionary.Add(ConfigNames.Buttons.Exit, new CommonConfig(keeper.Buttons.Exit, textures));
        dictionary.Add(ConfigNames.Buttons.Pause, new PauseButtonConfig(keeper.Buttons.Pause, textures));
    }


    private static void AddGunPanelIconConfigs(
        IDictionary<string, CommonConfig> dictionary,
        AllConfigsKeeperJson keeper,
        IDictionary<string, Texture2D> textures)
    {
        dictionary.Add(ConfigNames.GunPanelIcons.Gatling, new CommonConfig(keeper.GunPanelIcons.Gatling, textures));
        dictionary.Add(ConfigNames.GunPanelIcons.Cannon, new CommonConfig(keeper.GunPanelIcons.Cannon, textures));
        dictionary.Add(ConfigNames.GunPanelIcons.Laser, new CommonConfig(keeper.GunPanelIcons.Laser, textures));
        dictionary.Add(ConfigNames.GunPanelIcons.Tesla, new CommonConfig(keeper.GunPanelIcons.Tesla, textures));
        dictionary.Add(ConfigNames.GunPanelIcons.Missile, new CommonConfig(keeper.GunPanelIcons.Missile, textures));
    }


    private static void AddBackgroundConfigs(
        IDictionary<string, CommonConfig> dictionary,
        AllConfigsKeeperJson keeper,
        IDictionary<string, Texture2D> textures)
    {
        dictionary.Add(ConfigNames.Backgrounds.GunPanel, new CommonConfig(keeper.Backgrounds.GunPanel, textures));
        dictionary.Add(ConfigNames.Backgrounds.PopupWindow, new CommonConfig(keeper.Backgrounds.PopupWindow, textures));
    }

    #endregion

}