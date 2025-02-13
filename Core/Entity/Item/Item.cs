using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public abstract class Item : Entity, IDraw
{
    public bool Small; 
    public abstract void Draw(SpriteBatch spriteBatch);
}

