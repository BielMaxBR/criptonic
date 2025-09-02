using System.Collections.Generic;
using Godot;

public class BlockDatabase {
	private Dictionary<string, BlockBuilder> database;
	private int idCounter = 0;

	public BlockDatabase() {

	}

	public void RegisterAll() {
		CreateBlock(Stone.Register());
	}

	public void CreateBlock(BlockBuilder b) {
		b.Id(idCounter++);

		database.Add(b.name, b);
	}

	public Texture2D GenerateAtlas() {
		Texture2D texture = new Texture2D();

		return texture;
	} 
}