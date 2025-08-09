/*
/Game
 ├── /Blocks
 │    ├── Block.cs                # Classe base para blocos (ID, textura, propriedades)
 │    ├── DirtBlock.cs             # Exemplo de bloco concreto
 │    ├── StoneBlock.cs
 │    ├── BlockDatabase.cs         # Sistema que carrega e registra todos os blocos
 │    ├── BlockAtlasGenerator.cs   # Script que cria o atlas de texturas automaticamente
 │
 ├── /World
 │    ├── Chunk.cs                 # Armazena e gera a malha de um chunk
 │    ├── WorldGenerator.cs        # Controla a geração do terreno e o carregamento de chunks
 │    ├── Room.cs                  # Representa uma sala individual (pos, tamanho, blocos)
 │    ├── RoomGenerator.cs         # Algoritmo que cria salas sem sobreposição (AABB)
 │    ├── DungeonGenerator.cs      # Junta a geração de salas e o preenchimento de terreno
 │
 ├── /Utils
 │    ├── AABBUtils.cs             # Funções para checagem de colisão entre retângulos 3D
 │    ├── RandomUtils.cs           # Funções auxiliares para números aleatórios
 │
 ├── /Scenes
 │    ├── World.tscn               # Cena principal do mundo
 │
 └── Main.cs                       # Script de inicialização do jogo	

*/
using Godot;
// using System;
// using System.Collections.Generic;

public class Chunk {
	public const int SIZE = 16;
	public Vector3I Position;
	private Block[,,] BlockList;

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