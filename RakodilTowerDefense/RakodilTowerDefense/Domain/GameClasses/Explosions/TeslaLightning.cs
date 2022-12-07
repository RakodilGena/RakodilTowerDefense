using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RakodilTowerDefense.Domain.GameClasses.Explosions;

public class TeslaLightning: Explosion
{
    //todo ADD CODE
    #region Fields

    

    #endregion
    
    #region Properties

    

    #endregion
    
    #region Methods

    public TeslaLightning(Vector2 towerPosition, string configName) : base(towerPosition, configName)
    {
        
    }

    #endregion
    
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }
}