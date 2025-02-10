using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

/* 
 * Textury muzu nacitat jenom v Game.cs. (nenasel jsem jiny zpusob)
 * Nactu je proto sem to textureManageru a pak je muzu brat odsud :thumbsup:
 *
 * Je dobry pouzivat name textury stejny jako jmeno souboru (bez pripony)
 * feel free to udelat to jinak, tohle bylo nejjednodussi c:
 */
public static class TextureManager
{

    private static Dictionary<String, Texture2D> projectTextures = new Dictionary<string, Texture2D>();

    public static Texture2D getTexture(String name)
    {
        Texture2D output;
        if (projectTextures.TryGetValue(name, out output))
        {
            return output;
        }

        throw new Exception("Resource path not found!");
    }

    public static bool addTexture(String name, Texture2D texture)
    {
        if (projectTextures.TryAdd(name, texture))
        {
            return true;
        }
        return false;
    }
}