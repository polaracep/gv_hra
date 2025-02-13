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
    static SpriteFont Font;
    static Texture2D SpriteForeground;
    public InGameMenu(Viewport viewport)
    {
        Viewport = viewport;
        SpriteBackground = TextureManager.GetTexture("blackSquare");
        SpriteForeground = TextureManager.GetTexture("whiteSquare");
        Font = FontManager.GetFont("font");
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
        spriteBatch.Draw(SpriteBackground,
            new Rectangle(0, 0, Viewport.Width, Viewport.Height),
            new Color(0, 0, 0, (int)(255 * 0.25))); // Poloprůhledné pozadí

        Vector2 menuCenter = new Vector2(Viewport.Width / 2, Viewport.Height / 2);

        for (int i = 0; i < ItemContainers.Count; i++)
        {
            Vector2 containerPosition = menuCenter + new Vector2((i - ItemContainers.Count / 2) * 60, 0);
            ItemContainers[i].SetPosition(containerPosition);

            // Kreslení pozadí slotu
            spriteBatch.Draw(SpriteForeground,
                new Rectangle((int)containerPosition.X - 25, (int)containerPosition.Y - 25, 50, 50),
                Color.White);

            // Kreslení samotného předmětu
            ItemContainers[i].Draw(spriteBatch);
        }
    }

}
