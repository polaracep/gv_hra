using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TBoGV.Core;

namespace TBoGV;
public abstract class Room : IDraw
{
    public Vector2 Dimensions { get; protected set; }
    protected Tile[,] RoomMap;
    protected List<Projectile> projectiles;
    protected List<Enemy> enemies;
    protected Player player;

    public Room(Vector2 dimensions, Player p)
    {
        this.projectiles = new List<Projectile>();
        this.enemies = new List<Enemy>();
        this.player = p;
        this.Dimensions = dimensions;
        this.GenerateRoom();
    }

    protected abstract void GenerateRoom();
    public virtual Tile GetTile(Vector2 coords)
    {
        if (float.IsNaN(coords.X) || float.IsNaN(coords.Y))
            return new TileWall(WallTypes.BASIC);
        if (coords.X >= Dimensions.X * Tile.GetSize().X || coords.Y >= Dimensions.Y * Tile.GetSize().Y || coords.X < 0 || coords.Y < 0)
            return new TileWall(WallTypes.BASIC);
        return RoomMap[(int)(coords.X / Tile.GetSize().X), (int)(coords.Y / Tile.GetSize().Y)];
    }
    public virtual bool AddTile(Tile tile, Vector2 position)
    {
        try
        {
            RoomMap[(int)position.X, (int)position.Y] = tile;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public virtual void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void Update()
    {
        this.UpdateProjectiles();
        this.UpdateEnemies();
    }
    protected void UpdateProjectiles()
    {
        for (int i = player.Projectiles.Count - 1; i >= 0; i--)
        {
            player.Projectiles[i].Update();
            if (this.GetTile(player.Projectiles[i].GetCircleCenter()).DoCollision == true)
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
                        enemies.RemoveAt(j);
                        // TODO player.Kill() dostani xp, dropy atd`    
                    }
                    break;
                }
        }
    }
    protected void UpdateEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Update(player.Position + player.Size / 2);

            if (enemy.ReadyToAttack())
                projectiles.Add(enemy.Attack());
        }

    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Dimensions.X; i++)
            for (var j = 0; j < Dimensions.Y; j++)
            {
                Tile t = RoomMap[i, j];
                spriteBatch.Draw(t.Sprite, new Vector2(i * Tile.GetSize().X, j * Tile.GetSize().Y), Color.White);
            }

        foreach (Enemy enemy in enemies)
            enemy.Draw(spriteBatch);
        foreach (Projectile projectile in player.Projectiles)
            projectile.Draw(spriteBatch);
        foreach (Projectile projectile in projectiles)
            projectile.Draw(spriteBatch);
    }
}