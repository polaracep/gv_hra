using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TBoGV.Core.Interface;
using System;

namespace TBoGV.Core
{
	abstract internal class Enemy : Entity, IRecieveDmg, IDealDmg
	{
        public DateTime LastAttackTime { get; set; }
        public int AttackSpeed { get; set; }
        public int AttackDmg { get; set; }
        public abstract void SetPosition(Vector2 postition);
		public abstract void Draw(SpriteBatch spriteBatch);
		public abstract void RecieveDmg();
        public abstract bool ReadyToAttack();
        public abstract void Update(Vector2 playerPosition);
        public abstract Projectile Attack();
    }
}
