using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
namespace TBoGV;

internal class InGameMenu : IDraw
{
    static Texture2D SpriteBackground;
    static Viewport Viewport;
    public bool Active;
    List<ItemContainer> ItemContainers;
    public InGameMenu(Viewport viewport)
    {
        Viewport = viewport;
        SpriteBackground = TextureManager.GetTexture("blackSquare");
        Active = false;
    }
    public void Update(Viewport viewport, Player player, MouseState mouseState)
    {
        Viewport = viewport;
        player.UpdateContainers(mouseState);
        ItemContainers = player.ItemContainers;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(SpriteBackground, new Rectangle(0, 0, Viewport.Width, Viewport.Height), new Color(0, 0, 0, (int)(255 * 0.25)));
        // Draw item containers in the middle of the menu
        Vector2 menuCenter = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 2, GraphicsDeviceManager.DefaultBackBufferHeight / 2);
        for (int i = 0; i < ItemContainers.Count; i++)
        {
            Vector2 containerPosition = menuCenter + new Vector2((i - ItemContainers.Count / 2) * 50, 0);
            ItemContainers[i].Position = containerPosition;
            ItemContainers[i].Draw(spriteBatch);
        }
    }
}
