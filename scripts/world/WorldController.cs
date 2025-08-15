using Godot;

public class WorldController {
	public ChunkManager chunkManager;
	public ChunkRenderer chunkRenderer;

	int renderDistance = 12;

	public WorldController(Node3D root) {
		chunkManager = new ChunkManager();
		chunkRenderer = new ChunkRenderer(root);
	}

	public void UdpateChunks() {
		Vector3I pos = new(0,0,0);
	
		chunkManager.RequestChunk(pos);

		Chunk chunk = chunkManager.GetChunk(pos);
		chunkRenderer.Render(chunk, pos);

	}
}