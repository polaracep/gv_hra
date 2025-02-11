using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
    List<Projectile> projectiles;
    Enemy enemy;
    MouseState mouseState;
    KeyboardState keyboardState;
    public TBoGVGame()
    {
        _graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = "Content/Textures";
        IsMouseVisible = true;

    }

    protected override void Initialize()
    {
        base.Initialize();
        _camera = new Camera(GraphicsDevice.Viewport);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        TextureManager.Load(Content);
    }

    // Run after LoadContent
    protected override void BeginRun()
    {
        player = new Player(new Vector2(50, 50));
        enemy = new RangedEnemy(new Vector2(0, 100));
        projectiles = new List<Projectile>();
        r = new RoomEmpty();
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
        foreach (Projectile projectile in player.Projectiles)
            projectile.Update();
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update();

            if (ObjectCollision.CircleCircleCollision(projectiles[i], player))
            {
                player.RecieveDmg(projectiles[i].Damage);
                projectiles.RemoveAt(i); // ✅ Safe removal
            }
        }
        enemy.Update(player.Position + player.Size / 2);
        if (enemy.ReadyToAttack())
            projectiles.Add(enemy.Attack());

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
        enemy.Draw(_spriteBatch);
        foreach (Projectile projectile in player.Projectiles)
            projectile.Draw(_spriteBatch);
        foreach (Projectile projectile in projectiles)
            projectile.Draw(_spriteBatch);
        //enemy.Draw(_spriteBatch);

        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
