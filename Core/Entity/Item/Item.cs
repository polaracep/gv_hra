using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public abstract class Item : Entity, IDraw
{
    public bool Small;
    public abstract void Draw(SpriteBatch spriteBatch);
}

