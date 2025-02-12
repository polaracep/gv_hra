using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TBoGV;

internal class InGameMenu : IDraw
{
    static Texture2D SpriteBackground;
    static Viewport Viewport;
    public bool Active;
    public InGameMenu(Viewport viewport)
    {
        Viewport = viewport;
        SpriteBackground = TextureManager.GetTexture("blackSquare");
        Active = false;
    }
    public void Update(Viewport viewport)
    {
        Viewport = viewport;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(SpriteBackground, new Rectangle(0, 0, Viewport.Width, Viewport.Height), new Color(0, 0, 0, (int)(255 * 0.25)));
    }
}
