using Microsoft.Xna.Framework.Content;

namespace TBoGV;

abstract class Screen 
{
    public abstract void LoadContent();
    public abstract void Draw();
    public abstract void Load(ContentManager content);
    public abstract void BeginRun();

}

