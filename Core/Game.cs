using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TBoGV.Core;

namespace TBoGV;

public class TBoGVGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
	Player player;
	Enemy enemy;
	MouseState mouseState;
	KeyboardState keyboardState;
    public TBoGVGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content/Textures";
        IsMouseVisible = true;
		player = new Player(new Vector2(0, 0));
        //enemy = new RangedEnemy(new Vector2(0, 100));
	}

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

		player.Load(Content);

    }

    protected override void Update(GameTime gameTime)
    {
        // exit code
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
		mouseState = Mouse.GetState();
		keyboardState = Keyboard.GetState();
		player.Update(keyboardState, mouseState);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //TileWall wallTile = new TileWall();
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        //_spriteBatch.Draw(wallTile.getTexture(), new Vector2(0, 0), Color.White);
		player.Draw(_spriteBatch);
		//enemy.Draw(_spriteBatch);

		_spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
