using System;
using Godot;
using System.Collections.Generic;

public class ChunkRenderer {
	public Dictionary<Vector3I, Mesh> ChunkMeshs;	
	public ChunkRenderer() {

	}

	public void Render(Chunk chunk) {
		foreach(Block block in chunk.BlockList) {

		}
	}
}