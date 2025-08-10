/*
/Game
 ├── /Blocks
 │    ├── Block.cs                # Classe base para blocos (ID, textura, propriedades)
 │    ├── DirtBlock.cs            # Exemplo de bloco concreto
 │    ├── StoneBlock.cs
 │    ├── BlockDatabase.cs        # Carrega e registra blocos automaticamente
 │    ├── BlockAtlasGenerator.cs  # Gera o atlas de texturas a partir dos PNGs das faces
 │
 ├── /World
 │    ├── WorldController.cs      # Orquestra chunks, renderização e carregamento
 │    ├── ChunkManager.cs         # Gerencia dados e estado dos chunks
 │    ├── ChunkRenderer.cs        # Gera/atualiza meshes dos chunks
 │    ├── Chunk.cs                # Armazena dados de voxel do chunk
 │    ├── WorldGenerator.cs       # Controla a lógica de geração procedural
 │    ├── DungeonGenerator.cs     # Gera salas e estrutura principal
 │    ├── Room.cs                 # Representa uma sala (pos, tamanho, blocos)
 │    ├── RoomGenerator.cs        # Gera salas sem sobreposição (AABB)
 │
 ├── /Systems
 │    ├── LightingManager.cs      # (Opcional) Gerencia iluminação global
 │    ├── PhysicsManager.cs       # (Opcional) Gera colisões para os chunks
 │
 ├── /Utils
 │    ├── AABBUtils.cs            # Funções para checagem de colisão entre volumes
 │    ├── RandomUtils.cs          # Funções auxiliares para geração aleatória
 │
 ├── /Scenes
 │    ├── World.tscn              # Cena principal do mundo
 │
 └── Main.cs                      # Inicializa o jogo


*/
using Godot;

public partial class Chunk {
	public const int SIZE = 16;
	public Vector3I Position;
	public Block[,,] BlockList;

	public Chunk(Vector3I pos) {

		Position = pos;
		BlockList = new Block[SIZE,SIZE,SIZE];
	}

	public void SetBlock(Vector3I pos, Block block) {
		BlockList[pos.X,pos.Y,pos.Z] = block;
	}

	public Block GetBlock(Vector3I pos) {
		return BlockList[pos.X,pos.Y,pos.Z];
	}

}