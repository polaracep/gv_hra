using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV.Core
{
    internal class UI : IDraw
    {
        List<Heart> hearths;
        public UI()
        {
            hearths = new List<Heart>();
        }
        public void Update(Player player)
        {
            if (player.MaxHp != hearths.Count)
            {
                hearths.Clear();
                for (int i = 0; i < player.MaxHp; i++)
                    hearths.Add(new Heart());
            }

            // Top-left corner of the screen with a small padding
            Vector2 screenOffset = new Vector2(20, 20);

            for (int i = 0; i < hearths.Count; i++)
            {
                hearths[i].Broken = i >= player.Hp;

                // Screen-space positioning (ignoring camera transform)
                hearths[i].Position = screenOffset + new Vector2((hearths[i].Size.X + 10) * i, 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < hearths.Count; i++)
                hearths[i].Draw(spriteBatch);
        }
    }
}
