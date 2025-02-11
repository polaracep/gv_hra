using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

abstract class Screen 
{
    public abstract void LoadContent();
    public abstract void Draw(SpriteBatch _spriteBatch);
    public abstract void BeginRun();
    public abstract void Update();

}

