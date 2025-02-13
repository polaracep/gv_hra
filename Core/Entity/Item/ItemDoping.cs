using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TBoGV.Core.Entity.Item
{
    internal class ItemDoping : ItemContainerable
    {
        static Texture2D Sprite;
        public ItemDoping (Vector2 position)
        {
            Position = position;
            Size = new Vector2(50, 50);
            Name = "Problem solver";
            Description = "Dont do drugs";
            Stats = new Dictionary<StatTypes, int>() { { StatTypes.DAMAGE, 2 }, { StatTypes.MAX_HP, -3}, { StatTypes.MOVEMENT_SPEED, 1 } };
            Effects = new List<EffectTypes>() { EffectTypes.MAP_REVEAL };
            Sprite = TextureManager.GetTexture("heal");
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
        }

        public override void Interact(TBoGV.Entity e, Room r)
        {
            throw new NotImplementedException();
        }
    }
}
