using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

public static class TextureManager
{
    private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

    public static void Load(ContentManager content)
    {
        List<string> names = new List<string>
        {
            "tile",
            "wall",
            "door",
            "vitek-nobg",
            "projectile",
            "taunt",
            "admiration",
            "heal",
            "blackSquare",
            "koren",
            "korenovy_vezen",
            "gymvod",
            "platina",
        };

        foreach (string name in names)
        {
            textures.Add(name, content.Load<Texture2D>("Textures/" + name));
        }
    }
    public static Texture2D GetTexture(string name)
    {
        return textures.GetValueOrDefault(name);
    }
}

public static class SongManager
{
    private static Dictionary<string, Song> songs = new Dictionary<string, Song>();

    public static void Load(ContentManager content)
    {
        List<string> names = new List<string>
        {
            "soundtrack",
        };

        foreach (string name in names)
        {
            songs.Add(name, content.Load<Song>("Sounds/" + name));
        }
    }
    public static Song GetSong(string name)
    {
        return songs.GetValueOrDefault(name);
    }
}

