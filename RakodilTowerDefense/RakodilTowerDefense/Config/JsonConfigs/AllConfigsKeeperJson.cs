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

}