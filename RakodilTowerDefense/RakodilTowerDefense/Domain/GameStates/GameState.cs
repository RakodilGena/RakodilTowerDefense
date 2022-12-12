using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RakodilTowerDefense.Domain.CommonInterfaces;

namespace RakodilTowerDefense.Domain.GameStates;

public abstract class GameState: IDrawUpdate
{
    public abstract void Update(GameTime gameTime);

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}