using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TBoGV.Core
{
    internal class Hearth : IDraw
    {
        public bool Broken;
        static Texture2D Sprite;
        static string SpriteName = "vitek-nobg";
        public Vector2 Position;
        public Vector2 Size;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
        }
    }
}
