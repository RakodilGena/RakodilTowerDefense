using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Config.Configs;
using RakodilTowerDefense.Domain.CommonInterfaces;

namespace RakodilTowerDefense.Domain;

public abstract class CommonObject: IDrawUpdate
{
    #region Fields

    

    #endregion
    
    #region Properties
    
    protected CommonConfig Config { get; }
    
    /// <summary>
    /// Position of object;
    /// </summary>
    public Vector2 Position { get; protected set; }

    #endregion


    #region Methods

    protected CommonObject(Vector2 position, string configName)
    {
        Position = position;
        //Config = cfg;
        //todo add config fetch
    }

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    public abstract void Update(GameTime gameTime);

    #endregion
}