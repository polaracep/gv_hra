using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TBoGV.Core
{
	internal class RangedEnemy : Enemy
	{
        static Texture2D Sprite;
        static string SpriteName = "vitek-nobg";
        public RangedEnemy(Vector2 position)
		{
            Position = position;
            Size = new Vector2(50, 50);
            Hp = 3;
            MovementSpeed = 4;
            AttackSpeed = 20;
            AttackDmg = 1;
        }
        public override void Update(Vector2 playerPosition)
        {
            Direction = new Vector2(playerPosition.X, playerPosition.Y) - Position - Size / 2;
            Direction.Normalize(); 
        }
        public override Projectile Attack()
        {
            LastAttackTime = DateTime.UtcNow;
            return new Projectile(Position + Size / 2, Direction, AttackDmg);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch, Sprite);
        }

        public override bool ReadyToAttack()
        {
            return (DateTime.UtcNow - LastAttackTime).TotalMilliseconds >= AttackSpeed;
        }

        public override void RecieveDmg()
        {
            throw new NotImplementedException();
        }

        public override void SetPosition(Vector2 postition)
        {
            throw new NotImplementedException();
        }

        public override void Load(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(SpriteName);
        }
    }
}
