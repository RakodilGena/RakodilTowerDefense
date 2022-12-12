namespace RakodilTowerDefense.Config.Configs.Global;

public static class ConfigNames
{
    public static class Explosions
    {
        private const string PREFIX = "Explosion";

        public const string 
            Missile = PREFIX + "Missile",
            Cannon = PREFIX + "Cannon", 
            Laser = PREFIX + "Laser", 
            Gatling = PREFIX + "Gatling",
            Tesla = PREFIX + "Tesla",
            Enemy = PREFIX + "Enemy";
    }

    public static class Projectiles
    {
        private const string PREFIX = "Projectile";

        public const string Missile = PREFIX + "Missile";
    }

    public static class Guns
    {
        private const string PREFIX = "Gun";

        public const string 
            Gatling = PREFIX + "Gatling",
            Cannon = PREFIX + "Cannon",
            Laser = PREFIX + "Laser",
            Tesla = PREFIX + "Tesla",
            Missile = PREFIX + "Missile";
    }

    public static class Enemies
    {
        private const string PREFIX = "Enemy";

        public const string 
            Standard = PREFIX + "Standard",
            Fast = PREFIX + "Fast",
            Durable = PREFIX + "Durable",
            Boss = PREFIX + "Boss";
    }

    public static class Buttons
    {
        private const string PREFIX = "Button";

        public const string 
            CommonWhite = PREFIX + "CommonWhite",
            CommonBlack = PREFIX + "CommonBlack",
            Cancel = PREFIX + "Cancel",
            Exit = PREFIX + "Exit",
            Pause = PREFIX + "Pause";
    }
    
    public static class GunPanelIcons
    {
        private const string PREFIX = "GunPanelIcon";

        public const string 
            Gatling = PREFIX + "Gatling",
            Cannon = PREFIX + "Cannon",
            Laser = PREFIX + "Laser",
            Tesla = PREFIX + "Tesla",
            Missile = PREFIX + "Missile";
    }
    
    public static class Backgrounds
    {
        private const string PREFIX = "Background";

        public const string 
            GunPanel = PREFIX + "GunPanel",
            PopupWindow = PREFIX + "PopupWindow";
    }
    

}