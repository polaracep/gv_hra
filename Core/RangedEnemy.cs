using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;
internal class RangedEnemy : Enemy, IDraw
{
    static Texture2D Sprite;
    static string SpriteName = "vitek-nobg";
    public RangedEnemy(Vector2 position)
    {
        Position = position;
        Size = new Vector2(50, 50);
        Hp = 3;
        MovementSpeed = 4;
        AttackSpeed = 300;
        AttackDmg = 1;
        Sprite = TextureManager.GetTexture(SpriteName);
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
        spriteBatch.Draw(Sprite, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
    }
    public override bool ReadyToAttack()
    {
        return (DateTime.UtcNow - LastAttackTime).TotalMilliseconds >= AttackSpeed;
    }
    public override void RecieveDmg()
    {
        throw new NotImplementedException();
    }

    public override bool IsDead()
    {
        return Hp <= 0;
    }
}
