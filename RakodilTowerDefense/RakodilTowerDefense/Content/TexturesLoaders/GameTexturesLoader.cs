using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RakodilTowerDefense.Content.TexturesLoaders;

public static class GameTexturesLoader
{
    public static IDictionary<string, Texture2D> GetTextures(ContentManager contentManager)
    {
        var dictionary = new Dictionary<string, Texture2D>();
        dictionary.LoadEnemyTextures(contentManager);
        dictionary.LoadGunTextures(contentManager);
        dictionary.LoadProjectileTextures(contentManager);
        dictionary.LoadExplosionTextures(contentManager);
        
        //todo load other textures.

        return dictionary;
    }

    private static void LoadEnemyTextures(this IDictionary<string, Texture2D> dictionary, ContentManager contentManager)
    {
        const string path = @"Textures/Enemies/";
        dictionary.AddTexture(contentManager, path + "enemy_standard_720");
        dictionary.AddTexture(contentManager, path + "enemy_durable_900");
        dictionary.AddTexture(contentManager, path + "enemy_fast_900");
        dictionary.AddTexture(contentManager, path + "enemy_boss_800");
    }

    private static void LoadGunTextures(this IDictionary<string, Texture2D> dictionary, ContentManager contentManager)
    {
        const string path = @"Textures/Guns/";
        dictionary.AddTexture(contentManager, path + "gun_cannon_720x330_270_435");
        dictionary.AddTexture(contentManager, path + "gun_laser_850(1700)x540_300_455");
        dictionary.AddTexture(contentManager, path + "gun_machine_830x350_330_480");
        dictionary.AddTexture(contentManager, path + "gun_missile_700(1400)x400_385");
        dictionary.AddTexture(contentManager, path + "gun_tesla_740");
        
        dictionary.AddTexture(contentManager, path + "tesla_idle_lightning_740");
        
        dictionary.AddTexture(contentManager, path + "turret_cannon_200");
        dictionary.AddTexture(contentManager, path + "turret_laser_200");
        dictionary.AddTexture(contentManager, path + "turret_machine_200");
        dictionary.AddTexture(contentManager, path + "turret_missile_200");
    }

    private static void LoadProjectileTextures(this IDictionary<string, Texture2D> dictionary, ContentManager contentManager)
    {
        const string path = @"Textures/Projectiles/";
        dictionary.AddTexture(contentManager, path + "missile_flying_1000x400_720");
    }

    private static void LoadExplosionTextures(this IDictionary<string, Texture2D> dictionary, ContentManager contentManager)
    {
        const string path = @"Textures/Explosions/";
        dictionary.AddTexture(contentManager, path + "beam_laser_100x20");
        dictionary.AddTexture(contentManager, path + "beam_lightning_200x100x4");
        
        dictionary.AddTexture(contentManager, path + "explo_cannon_500x500");
        dictionary.AddTexture(contentManager, path + "explo_enemy_350x350_6");
        dictionary.AddTexture(contentManager, path + "explo_laser_50x50");
        dictionary.AddTexture(contentManager, path + "explo_lightning_32x32");
        dictionary.AddTexture(contentManager, path + "explo_machine_90x90");
        dictionary.AddTexture(contentManager, path + "explo_missile_350x350_7");
        dictionary.AddTexture(contentManager, path + "explo_missile_512x512");
        
        dictionary.AddTexture(contentManager, path + "shot_bullet_500x300x4");
        dictionary.AddTexture(contentManager, path + "shot_cannon_500x550x4");
        dictionary.AddTexture(contentManager, path + "shot_laser_80x50");
    }


    private static void AddTexture(
        this IDictionary<string, Texture2D> dictionary, 
        ContentManager contentManager,
        string name)
    {
        var texture = contentManager.Load<Texture2D>(name);
        dictionary.Add(name, texture);
    }
}