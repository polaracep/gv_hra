using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


namespace TBoGV;

public abstract class Projectile : Entity, IDraw
{
	public bool ShotByPlayer;
	public int Damage { get; set; }
	public virtual void Update()
	{
		Position += Direction * MovementSpeed;
	}
	public abstract void Draw(SpriteBatch spriteBatch);

}

