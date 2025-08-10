using Godot;

public class WorldController {
	public ChunkManager chunkManager;
	public ChunkRenderer chunkRenderer;

	int renderDistance = 12;

	public WorldController() {
		chunkManager = new ChunkManager();
		chunkRenderer = new ChunkRenderer();
	}

	public void UdpateChunks() {
		Vector3I pos = new(0,0,0);
	
		chunkManager.RequestChunk(pos);

		Chunk chunk = chunkManager.GetChunk(pos);
		chunkRenderer.Render(chunk);

	}
}