using RakodilTowerDefense.Config.JsonConfigs.ButtonConfigs;
using RakodilTowerDefense.Config.JsonConfigs.EnemyConfigs;
using RakodilTowerDefense.Config.JsonConfigs.ExplosionsConfig;
using RakodilTowerDefense.Config.JsonConfigs.GunConfigs;
using RakodilTowerDefense.Config.JsonConfigs.ProjectileConfigs;

namespace RakodilTowerDefense.Config.JsonConfigs;

public class AllConfigsKeeperJson
{
    #region Gun Configs

    public class GunConfigsJson
    {
        public GatlingConfigJson Gatling;

        public GunTurretConfigJson Cannon, Laser, Missile;

        public TeslaConfigJson Tesla;
    }

    public GunConfigsJson Guns;

    #endregion



    #region Projectile Configs

    public class ProjectileConfigsJson
    {
        public MissileConfigJson Missile;
    }

    public ProjectileConfigsJson Projectiles;

    #endregion



    #region Explosion Configs

    public class ExplosionConfigsJson
    {
        public ExplosionWithSparkConfigJson Gatling, Cannon;

        public BeamShotConfigJson Laser;

        public TeslaLightningConfigJson TeslaLightning;

        public ExplosionConfigJson Missile, Enemy;
    }

    public ExplosionConfigsJson Explosions;

    #endregion



    #region Enemy Configs

    public class EnemyConfigsJson
    {
        public EnemyConfigJson Standard, Fast, Durable, Boss;
    }

    public EnemyConfigsJson Enemies;

    #endregion
    


    #region Buttons

    public class ButtonConfigsJson
    {
        public CommonConfigJson CommonWhite, CommonBlack, Exit, Cancel;
        public PauseButtonConfigJson Pause;
    }

    public ButtonConfigsJson Buttons;

    #endregion
    
    

    #region GunPanelIcons

    public class GunPanelIconConfigsJson
    {
        public CommonConfigJson Gatling, Cannon, Laser, Tesla, Missile;
    }

    public GunPanelIconConfigsJson GunPanelIcons;

    #endregion
    
    

    #region Backgrounds

    public class BackgroundConfigsJson
    {
        public CommonConfigJson GunPanel, PopupWindow;
    }

    public BackgroundConfigsJson Backgrounds;

    #endregion

}