using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
namespace TBoGV;

internal class ScreenGame : Screen
{
    private GraphicsDeviceManager Graphics;
    public ScreenGame(GraphicsDeviceManager graphics)
    {
        Graphics = graphics;
    }

    public override void BeginRun()
    {

    }

    public override void Draw()
    {
        throw new NotImplementedException();
    }

    public override void Load(ContentManager content)
    {
        TextureManager.Load(content);
    }

    public override void LoadContent()
    {
        throw new NotImplementedException();
    }
}
