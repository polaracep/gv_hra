using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TBoGV.Core;

namespace TBoGV;

public class TBoGVGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera _camera;
    Player player;
    RoomEmpty r;
    UI UI;
    Screen screenGame, screenCurrent;
    InGameMenu inGameMenu;
    MouseState mouseState;
    KeyboardState keyboardState;
    public TBoGVGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        
        screenCurrent = screenGame = new ScreenGame(_graphics);

        Content.RootDirectory = "Content/Textures";
        IsMouseVisible = true;

    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        screenGame.Load(Content);
    }

    // Run after LoadContent
    protected override void BeginRun()
    {

        screenCurrent.BeginRun();

        player = new Player();
        r = new RoomEmpty(player);

        UI = new UI();
        _camera = new Camera(GraphicsDevice.Viewport, (int)(r.Dimensions.X * Tile.GetSize().X), (int)(r.Dimensions.Y * Tile.GetSize().Y));
        inGameMenu = new InGameMenu(GraphicsDevice.Viewport);
        base.BeginRun();
    }

    protected override void Update(GameTime gameTime)
    {
        // exit coded
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        mouseState = Mouse.GetState();
        keyboardState = Keyboard.GetState();
        player.Update(keyboardState, mouseState, _camera.Transform, r);

        r.Update();


        UI.Update(player);
        _camera.Update(player.Position + player.Size / 2);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //TileWall wallTile = new TileWall();
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.Transform);
        r.Draw(_spriteBatch);
        player.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin();
        UI.Draw(_spriteBatch);
        inGameMenu.Draw(_spriteBatch);
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
