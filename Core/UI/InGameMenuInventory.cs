using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System;

namespace TBoGV;

internal class InGameMenuInventory : InGameMenu, IDraw
{
	static Viewport Viewport;
	List<ItemContainer> ItemContainers;
	static SpriteFont Font;
	static SpriteFont MiddleFont;
	static SpriteFont LargerFont;
	static Texture2D SpriteForeground;
	ItemContainer? hoveredItem; // Store hovered item

	public InGameMenuInventory(Viewport viewport)
	{
		Viewport = viewport;
		SpriteBackground = TextureManager.GetTexture("blackSquare");
		SpriteForeground = TextureManager.GetTexture("whiteSquare");
		Font = FontManager.GetFont("Arial8");
		MiddleFont = FontManager.GetFont("Arial12");
		LargerFont = FontManager.GetFont("Arial24");
		Active = false;
	}

	public override void Update(Viewport viewport, Player player, MouseState mouseState)
	{
		base.Update(viewport, player, mouseState);
		ItemContainers = player.ItemContainers;
		hoveredItem = null; // Reset hovered item

		Vector2 menuCenter = new Vector2(viewport.Width / 2, viewport.Height / 2);
		for (int i = 0; i < ItemContainers.Count; i++)
		{
			Vector2 containerPosition = menuCenter + new Vector2((i - ItemContainers.Count / 2) * 60, 0);
			ItemContainers[i].SetPosition(containerPosition);

			// Check if mouse is over an item
			if (ItemContainers[i].GetRectangle().Contains(mouseState.Position) && !ItemContainers[i].IsEmpty())
			{
				hoveredItem = ItemContainers[i];
			}
		}
	}

	public override void Draw(SpriteBatch spriteBatch)
	{
		base.Draw(spriteBatch);
		Vector2 menuCenter = new Vector2(Viewport.Width / 2, Viewport.Height / 2);

		foreach (var container in ItemContainers)
		{
			container.Draw(spriteBatch);
		}

		// Draw tooltip if an item is hovered
		if (hoveredItem != null)
		{
			DrawTooltip(spriteBatch, hoveredItem);
		}
	}

	private void DrawTooltip(SpriteBatch spriteBatch, ItemContainer item)
	{
		Vector2 tooltipPosition = new Vector2(Mouse.GetState().X + 10, Mouse.GetState().Y + 10);

		// Get formatted text
		string name = item.Item.Name;
		string description = item.Item.Description;
		string stats = FormatStats(item.Item.Stats);

		// Measure text sizes
		Vector2 nameSize = LargerFont.MeasureString(name);
		Vector2 descriptionSize = MiddleFont.MeasureString(description);
		Vector2 statsSize = Font.MeasureString(stats);

		// Determine tooltip width & height
		float tooltipWidth = Math.Max(Math.Max(nameSize.X, descriptionSize.X), statsSize.X) + 20;
		float tooltipHeight = nameSize.Y + descriptionSize.Y + 10 + statsSize.Y + 20; // Extra 10px space after description

		Rectangle backgroundRect = new Rectangle(tooltipPosition.ToPoint(), new Point((int)tooltipWidth, (int)tooltipHeight));

		// Draw background
		spriteBatch.Draw(SpriteBackground, backgroundRect, Color.Black * 0.8f);

		// Draw text
		Vector2 textPosition = tooltipPosition + new Vector2(10, 5);
		spriteBatch.DrawString(LargerFont, name, textPosition, Color.White);
		textPosition.Y += nameSize.Y;

		spriteBatch.DrawString(MiddleFont, description, textPosition, Color.LightGray);
		textPosition.Y += descriptionSize.Y + 10; // Add extra space below description

		// Center stats within the tooltip
		Vector2 statsPosition = new Vector2(
			tooltipPosition.X + (tooltipWidth - statsSize.X) / 2, // Center horizontally
			textPosition.Y
		);
		spriteBatch.DrawString(Font, stats, statsPosition, Color.White);
	}

	private string FormatStats(Dictionary<StatTypes, int> stats)
	{
		if (stats == null || stats.Count == 0) return "No stats available";

		StringBuilder sb = new StringBuilder();
		foreach (var stat in stats)
		{
			sb.AppendLine($"{stat.Key}: {stat.Value}");
		}
		return sb.ToString();
	}
}
