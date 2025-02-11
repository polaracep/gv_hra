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
    List<Enemy> enemies;
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
        projectiles = new List<Projectile>();
        enemies = new List<Enemy>();
        Enemy enemy;
        for (int i = 1; i < 19; i++)
        {
            enemy = new RangedEnemy(new Vector2(50*i, 50));
            enemies.Add(enemy);
        }
        
        player = new Player(new Vector2(50, 350));

        r = new RoomEmpty();
        r.AddTile(new TileHeal(), new Vector2(5, 5));
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

        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update();

            if (ObjectCollision.CircleCircleCollision(projectiles[i], player))
            {
                player.RecieveDmg(projectiles[i].Damage);
                projectiles.RemoveAt(i);
                continue;
            }
            if (r.GetTile(projectiles[i].GetCircleCenter()).DoCollision == true)
            {
                projectiles.RemoveAt(i);
            }
        }
        for (int i = player.Projectiles.Count - 1; i >= 0; i--)
        {
            player.Projectiles[i].Update();
            if (r.GetTile(player.Projectiles[i].GetCircleCenter()).DoCollision == true)
            {
                player.Projectiles.RemoveAt(i);
                continue;
            }
            for (int j = 0; j < enemies.Count; j++)
                if (ObjectCollision.CircleCircleCollision(player.Projectiles[i], enemies[j]))
                {
                    // HOnim HOdne HOdin - SANTA REFERENCE
                    enemies[j].RecieveDmg(player.Projectiles[i].Damage);
                    player.Projectiles.RemoveAt(i); 
                    if (enemies[j].IsDead())
                    {
                        player.Kill(enemies[j].XpValue);
                        enemies.RemoveAt(j);
                        // TODO player.Kill() dostani xp, dropy atd`    
                    }
                    break;
                }

        }
        foreach (Enemy enemy in enemies)
        {
            enemy.Update(player.Position + player.Size / 2);

            if (enemy.ReadyToAttack())
                projectiles.Add(enemy.Attack());
        }

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
        foreach (Enemy enemy in enemies)
            enemy.Draw(_spriteBatch);
        foreach (Projectile projectile in player.Projectiles)
            projectile.Draw(_spriteBatch);
        foreach (Projectile projectile in projectiles)
            projectile.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin();
        UI.Draw(_spriteBatch);
        inGameMenu.Draw(_spriteBatch);
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
