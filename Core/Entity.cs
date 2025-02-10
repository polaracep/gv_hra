using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace TBoGV;
abstract class Entity
{
	string SpriteName;
	public Vector2 Position;
	public Vector2 Direction;
	public Vector2 Size;
	public int MovementSpeed;
	public int Hp { get; set; }
	public Entity() { }

}