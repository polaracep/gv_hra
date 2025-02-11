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
    private int Frame;
    public ScreenGame(GraphicsDeviceManager graphics)
    {
        Graphics = graphics;
        Frame = 0;
    }

    public override void BeginRun()
    {
        player = new Player();
        r = new RoomEmpty(player);
        UI = new UI();
        _camera = new Camera(Graphics.GraphicsDevice.Viewport, (int)(r.Dimensions.X * Tile.GetSize().X), (int)(r.Dimensions.Y * Tile.GetSize().Y));
        inGameMenu = new InGameMenu(Graphics.GraphicsDevice.Viewport);
    }

    public override void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Begin(blendState: BlendState.Opaque);
        // _spriteBatch.Draw(TextureManager.GetTexture("gymvod"), Vector2.Zero, Color.White);
        _spriteBatch.Draw(TextureManager.GetTexture("gymvod"),
            new Rectangle(0, 0, Graphics.GraphicsDevice.Viewport.Width, Graphics.GraphicsDevice.Viewport.Height), Color.White);
        _spriteBatch.End();

        _spriteBatch.Begin(transformMatrix: _camera.Transform);
        r.Draw(_spriteBatch);
        player.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin();
        UI.Draw(_spriteBatch);
        if (inGameMenu.Active)
            inGameMenu.Draw(_spriteBatch);
        _spriteBatch.End();
    }
    public override void LoadContent()
    {
        throw new NotImplementedException();
    }

    public override void Update(GameTime gameTime)
    {
        previousKeyboardState = keyboardState;
        mouseState = Mouse.GetState();
        keyboardState = Keyboard.GetState();
        if (KeyReleased(Keys.Escape))
            inGameMenu.Active = !inGameMenu.Active;
        if (!inGameMenu.Active || (Frame%15 == 0))
        {
            player.Update(keyboardState, mouseState, _camera.Transform, r);
            r.Update();
            UI.Update(player);
            _camera.Update(player.Position + player.Size / 2);
        }
        Frame++;
    }
    KeyboardState previousKeyboardState;

    public bool KeyReleased(Keys key)
    {
        return previousKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key);
    }
}
