using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace TBoGV.Core
{
    internal class UI : IDraw
    {
        List<Heart> hearths;
		static SpriteFont Font;
		static Texture2D SpriteCoin;
		int Coins;

		public UI()
        {
            hearths = new List<Heart>();
			Font = FontManager.GetFont("font");
			SpriteCoin = TextureManager.GetTexture("coin");
			Coins = 0;
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
			Coins = player.Coins;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < hearths.Count; i++)
                hearths[i].Draw(spriteBatch);
			string coinText = $"{Coins}";
			Vector2 coinPosition = new Vector2(20, 60); // Position under the hearts
			spriteBatch.Draw(SpriteCoin, new Rectangle((int)coinPosition.X, (int)coinPosition.Y,30,30), Color.White);
			spriteBatch.DrawString(Font, coinText, new Vector2((int)coinPosition.X +30, (int)coinPosition.Y), Color.Yellow);
		}
    }
}
