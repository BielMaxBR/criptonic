using Godot;
using System;
using System.Collections.Generic;

public class ChunkManager {
	public Dictionary<Vector3I, Chunk> LoadedChunks; // não pode ser uma lista estatica

	public ChunkManager() {
		LoadedChunks = new ();
	}

	public Chunk GetChunk(Vector3I pos) {
		return LoadedChunks[pos];
	}

	public void RequestChunk(Vector3I pos) {
		Chunk chunk = new Chunk(pos);
		for (int x = 0; x < 5; x++) {
			for (int y = 0; y < 5; y++) {
				Vector3I blockPos = new Vector3I(x,0,y);
				chunk.SetBlock(blockPos, new Ball(blockPos));
			}
		}
		LoadedChunks.Add(pos,chunk);
	}

	public void DeleteChunk(Vector3I pos) {
		LoadedChunks.Remove(pos);
	}

    internal void Render(Chunk chunk)
    {
        throw new NotImplementedException();
    }
}