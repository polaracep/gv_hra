using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using TBoGV.Core;
namespace TBoGV;

internal class ScreenGame : Screen
{
    private GraphicsDeviceManager Graphics;
    private Player player;
    private RoomEmpty r;
    private Camera _camera;
    private InGameMenu inGameMenu;
    private UI UI;
    private MouseState mouseState;
    private KeyboardState keyboardState;
    public ScreenGame(GraphicsDeviceManager graphics)
    {
        Graphics = graphics;
    }

    public override void BeginRun(GraphicsDevice graphicsDevice)
    {
        player = new Player();
        r = new RoomEmpty(player);
        UI = new UI();
        _camera = new Camera(graphicsDevice.Viewport, (int)(r.Dimensions.X * Tile.GetSize().X), (int)(r.Dimensions.Y * Tile.GetSize().Y));
        inGameMenu = new InGameMenu(graphicsDevice.Viewport);
    }

    public override void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Begin(transformMatrix: _camera.Transform);
        r.Draw(_spriteBatch);
        player.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin();
        UI.Draw(_spriteBatch);
        inGameMenu.Draw(_spriteBatch);
        _spriteBatch.End();
    }

    public override void Load(ContentManager content)
    {
        TextureManager.Load(content);
    }

    public override void LoadContent()
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        mouseState = Mouse.GetState();
        keyboardState = Keyboard.GetState();
        player.Update(keyboardState, mouseState, _camera.Transform, r);
        r.Update();
        UI.Update(player);
        _camera.Update(player.Position + player.Size / 2);
    }
}
