using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;
abstract class EnemyRanged : Enemy, IDraw
{
    public override void Update(Vector2 playerPosition)
    {
        Direction = new Vector2(playerPosition.X, playerPosition.Y) - Position - Size / 2;
        Direction.Normalize();
    }
    public override Projectile Attack()
    {
        LastAttackTime = DateTime.UtcNow;
        return new ProjectilePee(Position + Size / 2, Direction, AttackDmg);
    }
    public override bool ReadyToAttack()
    {
        return (DateTime.UtcNow - LastAttackTime).TotalMilliseconds >= AttackSpeed;
    }
	public override bool IsDead()
	{
		return Hp <= 0;
	}
}

