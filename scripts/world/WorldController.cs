using Godot;

public class WorldController {
	public BlockDatabase blockDatabase;
	public ChunkManager chunkManager;
	public ChunkRenderer chunkRenderer;

	int renderDistance = 12;

	public WorldController(Node3D root) {
		blockDatabase = new BlockDatabase();
		chunkManager = new ChunkManager();
		chunkRenderer = new ChunkRenderer(root, blockDatabase);

		blockDatabase.RegisterAll();
		chunkRenderer.UVTexture = blockDatabase.GenerateAtlas();

	}

	public void UdpateChunks() {
		Vector3I pos = new(0,0,0);
	
		chunkManager.RequestChunk(pos);

		Chunk chunk = chunkManager.GetChunk(pos);
		chunkRenderer.Render(chunk, pos);

	}
}