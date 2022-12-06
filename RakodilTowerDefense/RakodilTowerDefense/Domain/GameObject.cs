using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs;

namespace RakodilTowerDefense.Domain;

public abstract class GameObject: IObject
{
    #region Fields

    protected CommonConfig _config;

    #endregion
    
    #region Properties
    
    /// <summary>
    /// Position of game object;
    /// </summary>
    public Vector2 Position { get; private set; }

    #endregion


    #region Methods

    protected GameObject(Vector2 position, CommonConfig cfg)
    {
        Position = position;
        _config = cfg;
    }

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    public abstract void Update(GameTime gameTime);

    #endregion
    
}