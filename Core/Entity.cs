using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBoGV.Core.Interface;
using Microsoft.Xna.Framework.Input;

namespace TBoGV.Core
{
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
}
