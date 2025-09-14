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
		// CreateBlock(Wood.Register());
		// CreateBlock(Door.Register());

		GenerateAtlas();
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
			GD.Print("-----");
			GD.Print(newSize.X*block.id);
			// GD.Print(bigImage.GetFormat());
			// GD.Print(blockImage.GetFormat());
			// GD.Print("-----");
			blockImage.Convert(bigImage.GetFormat());
			bigImage.BlitRect(blockImage, rect, new Vector2I(block.id*blockImage.GetSize().X, 0));

		}
		// bigImage.SavePng("res://test.png");
		Vector2I bigSize = bigImage.GetSize();
		Vector2I UVBlockSize = new Vector2I(32,32) / bigSize;
		Vector2I[] faceDefaultUVs =[
			new Vector2I(0,0)*UVBlockSize,
		    new Vector2I(1,0)*UVBlockSize,
		    new Vector2I(0,1)*UVBlockSize,
		    new Vector2I(1,1)*UVBlockSize
		];
		foreach(var (name, block) in database) {
			Vector2I idOffset = new Vector2I(1,0) * block.id;
			for (int i = 0; i < 6; i++) {
				Vector2I faceOffset = new Vector2I(i%6,i/3) * idOffset;
				block.faceUvList[i] = [
						faceDefaultUVs[0] * faceOffset,
						faceDefaultUVs[1] * faceOffset,
						faceDefaultUVs[2] * faceOffset,
						faceDefaultUVs[3] * faceOffset
					];
				}

		}
		return ImageTexture.CreateFromImage(bigImage);
	}

	public BlockBuilder GetBlockData(string name) {
		return database[name];
	} 
}