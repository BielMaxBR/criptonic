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
		CreateBlock(Grass.Register());
		CreateBlock(Ball.Register());
		// CreateBlock(Wood.Register());
		// CreateBlock(Door.Register());

		// GenerateAtlas();
	}

	public void CreateBlock(BlockBuilder b) {
		b.Id(idCounter++);

		database.Add($"{b.name}", b);
	}

	public Texture2D GenerateAtlas() {
		Image bigImage = Image.CreateEmpty(1,1,false,Image.Format.Rgba8);

		// faz a magica aqui
		foreach(var (name, block) in database) {
			string sprite_name = block.sprite;
			Image blockImage = ResourceLoader.Load<Texture2D>($"res://assets/sprites/blocks/{sprite_name}").GetImage();
			Vector2I newSize = new Vector2I(blockImage.GetSize().X + bigImage.GetSize().X, blockImage.GetSize().Y);
			bigImage.Crop(newSize.X, newSize.Y);
			Rect2I rect = new Rect2I(new Vector2I(0,0),newSize);
			// GD.Print("-----");
			// GD.Print(blockImage.GetSize().X*block.id);
			// GD.Print(bigImage.GetFormat());
			// GD.Print(blockImage.GetFormat());
			// GD.Print("-----");
			blockImage.Convert(bigImage.GetFormat());
			bigImage.BlitRect(blockImage, rect, new Vector2I(block.id*blockImage.GetSize().X, 0));

		}
		// bigImage.SavePng("res://test.png");
		Vector2 bigSize = bigImage.GetSize();
		Vector2 UVBlockSize = new Vector2(32,32) / bigSize; // 32px de tile
		Vector2[] faceDefaultUVs =[
		    new Vector2(0,1),
		    new Vector2(1,1),
			new Vector2(0,0),
		    new Vector2(1,0),
		];
		foreach(var (name, block) in database) {
			Vector2 idOffset = new Vector2(block.id * 3, 0);
			for (int i = 0; i < 6; i++) {
				Vector2 faceOffset = new Vector2(i%3,i/3) + idOffset; // assumindo q o sheet de bloco seja 3x2
				block.faceUvList[i] = new Vector2[4];


				for (int v = 0; v < 4; v++) {
					block.faceUvList[i][v] = (faceOffset + faceDefaultUVs[v]) * UVBlockSize;
				}
			}

		}
		foreach(var (name, block) in database) {
			for (int i = 0; i < 6; i++) {
				for (int v = 0; v < 4; v++) {
					GD.Print($"{i} : {v} : {block.faceUvList[i][v]}");
				}
			}
		}
		ImageTexture texture = new();
		texture.SetImage(bigImage);
		return texture;
	}

	public BlockBuilder GetBlockData(string name) {
		return database[name];
	} 
}