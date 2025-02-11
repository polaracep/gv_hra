using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;
internal class RangedEnemy : Enemy, IDraw
{
    protected Texture2D Sprite;
    protected static string SpriteName = "korenovy_vezen";
    public RangedEnemy(Vector2 position)
    {
        Sprite = TextureManager.GetTexture(SpriteName);
        Size = new Vector2(Sprite.Width, Sprite.Height);
        Position = position;
        Hp = 3;
        MovementSpeed = 4;
        AttackSpeed = 300;
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
