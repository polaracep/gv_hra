using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TBoGV.Core;

namespace TBoGV;

public class TBoGVGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
	Player player;

    public TBoGVGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content/Textures";
        IsMouseVisible = true;
		player = new Player("vitek-nobg", new Vector2(0, 0));
	}

    protected override void Initialize()
    {
        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
		player.Load(Content);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        // exit code
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //TileWall wallTile = new TileWall();
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        //_spriteBatch.Draw(wallTile.getTexture(), new Vector2(0, 0), Color.White);
		player.Draw(_spriteBatch);
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
