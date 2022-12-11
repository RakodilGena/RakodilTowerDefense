using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs.EnemyConfigs;
using RakodilTowerDefense.Config.Configs.Global;
using RakodilTowerDefense.Extensions;

namespace RakodilTowerDefense.Domain.GameClasses.Enemies;

public abstract class Enemy:GameObject
{
    #region Fields

    private float _rotation;

    private float _currentHp;
    

    #endregion

    #region Properties

    private new EnemyConfig Config => (EnemyConfig)base.Config;

    private float Rotation
    {
        get => _rotation;
        set { _rotation = value; }
    }

    /// <summary>
    /// Returns flag that says if enemy is alive.
    /// </summary>
    public bool IsAlive => _currentHp > 0;

    /// <summary>
    /// Returns moving speed of enemy.
    /// </summary>
    public float MovingSpeed => Config.MovingSpeed;

    /// <summary>
    /// Returns enemy rank.
    /// </summary>
    public int Rank => Config.Rank;
    
    /// <summary>
    /// Total distance traveled by this enemy.
    /// </summary>
    public float TraveledDistance { get; /*private*/ set; }

    #endregion

    #region Methods


    protected Enemy(Vector2 position, float initialRotation, string configName) : base(position, configName)
    {
        Rotation = initialRotation;
        _currentHp = Config.HP;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //this method draws enemy

        EnemyConfig cfg = Config;
        Texture2D texture = cfg.Texture;

        spriteBatch.Draw(
            texture: texture,
            position: GetGamePosition(),
            sourceRectangle: new Rectangle(0,0, texture.Width, texture.Height),
            color: Color.White,
            rotation: Rotation,
            origin: new Vector2(texture.Width / 2f, texture.Height / 2f),
            scale: new Vector2(cfg.TextureScale) * GlobalConfig.GameFieldScale,
            effects: SpriteEffects.None,
            layerDepth: DrawLayers.Enemy);
    }



    public void ReceiveDamage(float damage)
    {
        if (damage <= 0)
            return;

        _currentHp -= damage;
        //todo Add code to destroy if hp < 0 and create explosion.
    }

    public override void Update(GameTime gameTime)
    {
        var addition = (float)gameTime.ElapsedGameTime.TotalMilliseconds * Config.MovingSpeed;
        Position = Position.MovedInDirection(addition, Rotation);
        TraveledDistance += addition;

        if (TraveledDistance > 1600)
        {
            //пройдет 1600 - равернется.
            TraveledDistance = 0;
            Rotation += MathHelper.ToRadians(180);
        }
    }

    #endregion
}