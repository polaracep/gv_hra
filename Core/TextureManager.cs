using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public static class TextureManager
{
    private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

    public static void Load(ContentManager content)
    {
        textures.Add("tile", content.Load<Texture2D>("tile"));
        textures.Add("wall", content.Load<Texture2D>("wall"));
        textures.Add("door", content.Load<Texture2D>("door"));

        textures.Add("vitek-nobg", content.Load<Texture2D>("vitek-nobg"));

        textures.Add("projectile", content.Load<Texture2D>("projectile"));
    }
    public static Texture2D GetTexture(string name)
    {
        return textures.GetValueOrDefault(name);
    }
}