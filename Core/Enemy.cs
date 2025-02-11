using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace TBoGV;
abstract internal class Enemy : Entity, IRecieveDmg, IDealDmg
{
    public DateTime LastAttackTime { get; set; }
    public int AttackSpeed { get; set; }
    public int AttackDmg { get; set; }
	public int Hp { get; set; }
    public int MaxHp { get; set; }

    public abstract void SetPosition(Vector2 postition);
    public abstract void Draw(SpriteBatch spriteBatch);
    public abstract void RecieveDmg();
    public abstract bool ReadyToAttack();
    public abstract void Update(Vector2 playerPosition);
    public abstract Projectile Attack();

	public void RecieveDmg(int damage)
	{
		Hp -= damage;
	}
}
