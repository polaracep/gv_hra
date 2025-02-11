using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TBoGV.Core;

namespace TBoGV;

public class TBoGVGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Screen screenGame, screenCurrent;
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

        screenCurrent.BeginRun(GraphicsDevice);
        base.BeginRun();
    }

    protected override void Update(GameTime gameTime)
    {
        // exit coded
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        screenCurrent.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(blendState: BlendState.Opaque);
        // _spriteBatch.Draw(TextureManager.GetTexture("gymvod"), Vector2.Zero, Color.White);
        _spriteBatch.Draw(TextureManager.GetTexture("gymvod"),
            new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
        _spriteBatch.End();

        _spriteBatch.Begin(transformMatrix: _camera.Transform);
        r.Draw(_spriteBatch);
        player.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin();
        UI.Draw(_spriteBatch);
        _spriteBatch.End();


        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
