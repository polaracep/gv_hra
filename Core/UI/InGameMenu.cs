using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace TBoGV;

internal class InGameMenu : IDraw
{
    private static Texture2D spriteBackground;
    private static Viewport viewport;
    public bool Active { get; set; }
    private List<ItemContainer> itemContainers;
    private const float BackgroundOpacity = 0.25f;
    private const int ContainerSpacing = 60;

    public InGameMenu(Viewport viewport)
    {
        InGameMenu.viewport = viewport;
        spriteBackground = TextureManager.GetTexture("blackSquare");
        Active = false;
        itemContainers = new List<ItemContainer>();
    }

    public void Update(Viewport viewport, Player player, MouseState mouseState)
    {
        InGameMenu.viewport = viewport;
        player.UpdateContainers(mouseState);
        itemContainers = player.ItemContainers;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteBackground, new Rectangle(0, 0, viewport.Width, viewport.Height), new Color(0, 0, 0, (int)(255 * BackgroundOpacity)));
            
        if (itemContainers == null || itemContainers.Count == 0)
            return;
            
        Vector2 menuCenter = new Vector2(viewport.Width / 2, viewport.Height / 2);
        int numRows = (int)Math.Ceiling(Math.Sqrt(itemContainers.Count));
        int numCols = (int)Math.Ceiling((double)itemContainers.Count / numRows);
        Vector2 startPosition = menuCenter - new Vector2((numCols - 1) * ContainerSpacing / 2, (numRows - 1) * ContainerSpacing / 2);

        for (int i = 0; i < itemContainers.Count; i++)
        {
            int row = i / numCols;
            int col = i % numCols;
            Vector2 containerPosition = startPosition + new Vector2(col * ContainerSpacing, row * ContainerSpacing);
            itemContainers[i].Position = containerPosition;
            itemContainers[i].Draw(spriteBatch);
        }
    }
}
