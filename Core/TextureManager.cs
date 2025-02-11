using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            "blackSquare"
        };

        foreach (string name in names)
        {
            textures.Add(name, content.Load<Texture2D>(name));
        }
    }
    public static Texture2D GetTexture(string name)
    {
        return textures.GetValueOrDefault(name);
    }
}