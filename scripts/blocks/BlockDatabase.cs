using System.Collections.Generic;
using Godot;

public class BlockDatabase {
	private Dictionary<string, BlockBuilder> database;
	private int idCounter = 0;

	public BlockDatabase() {
		database = new();
	}

	public void RegisterAll() {
		CreateBlock(Stone.Register());
		// CreateBlock(Grass.Register());
		// CreateBlock(Wood.Register());
		// CreateBlock(Door.Register());
	}

	public void CreateBlock(BlockBuilder b) {
		b.Id(idCounter++);

		database.Add(b.name, b);
	}

	public Texture2D GenerateAtlas() {
		Image bigImage = new Image();

		// faz a magica aqui
		foreach(var (name, block) in database) {
			string sprite_name = block.sprite;
			// block.faceUvList
			Vector2 newSize = new Vector2(blockImage.GetSize().X + bigImage.GetSize().X, blockImage.GetSize().Y);
			Image blockImage = Image.LoadFromFile($"res://assets/sprites/blocks/{sprite_name}");
			bigImage.Crop(newSize.X, newSize.Y);
			Rect2 rect = new Rect2(new Vector2(0,0),newSize);

			bigImage.BlitRect(blockImage, rect, new Vector2(, size.y*i))

		}
		return ImageTexture.CreateFromImage(bigImage);
	}

	public BlockBuilder GetBlockData(string name) {
		return database[name];
	} 
}