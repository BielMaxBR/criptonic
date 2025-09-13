using System;
using Godot;
using System.Collections.Generic;
using Array = Godot.Collections.Array;

public class ChunkRenderer {
	public Dictionary<Vector3I, MeshInstance3D> ChunkMeshs;
	public BlockDatabase blockDatabase; 
	public Node3D Root;	
	public ChunkRenderer(Node3D root, BlockDatabase _blockDatabase) {
		Root = root;
		ChunkMeshs = new();
		blockDatabase = _blockDatabase;
	}

	public void Render(Chunk chunk, Vector3I chunkpos) {
	    MeshInstance3D instance = new MeshInstance3D();
	    ArrayMesh mesh = new();

	    Array array = new();
	    array.Resize((int)Mesh.ArrayType.Max);

	    List<Vector3> verts = new();
	    List<Vector2> uvs = new();
	    List<Vector3> normals = new();
	    List<int> indices = new();
	    List<Color> colors = new();

	    foreach (Block block in chunk.BlockList) {
	    	if (block == null) continue;
	        // Faces visíveis
	        bool u = block.HasAllFaces(Block.Faces.UP);
	        bool d = block.HasAllFaces(Block.Faces.DOWN);
	        bool l = block.HasAllFaces(Block.Faces.LEFT);
	        bool r = block.HasAllFaces(Block.Faces.RIGHT);
	        bool f = block.HasAllFaces(Block.Faces.FRONT);
	        bool b = block.HasAllFaces(Block.Faces.BACK);

	        // Posição base do bloco
	        Vector3 pos = block.Position; // Precisa existir no seu Block

	        // Cantos (coordenadas relativas ao bloco)
	        Vector3 fbl = pos + new Vector3(0, 0, 0); // Front Bottom Left
	        Vector3 fbr = pos + new Vector3(0, 0, 1); // Front Bottom Right
	        Vector3 ftl = pos + new Vector3(0, 1, 0); // Front Top Left
	        Vector3 ftr = pos + new Vector3(0, 1, 1); // Front Top Right
	        Vector3 bbl = pos + new Vector3(1, 0, 0); // Back Bottom Left
	        Vector3 bbr = pos + new Vector3(1, 0, 1); // Back Bottom Right
	        Vector3 btl = pos + new Vector3(1, 1, 0); // Back Top Left
	        Vector3 btr = pos + new Vector3(1, 1, 1); // Back Top Right

	        Vector2[][] faceUvList = blockDatabase.GetBlockData(block.Name).faceUvList;
	        // Função local para adicionar uma face
	        void AddFace(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 normal, Vector2[] uvlist) {
	            int startIndex = verts.Count;

	            verts.Add(v0);
	            verts.Add(v1);
	            verts.Add(v2);
	            verts.Add(v3);

	            normals.Add(normal);
	            normals.Add(normal);
	            normals.Add(normal);
	            normals.Add(normal);

			    uvs.Add(uvlist[0]); // bottom-left
			    uvs.Add(uvlist[1]); // bottom-right
			    uvs.Add(uvlist[2]); // top-left  
			    uvs.Add(uvlist[3]); // top-right

	            // Cor (pode mudar para usar textura)
	            colors.Add(new Color(1, 1, 1));
	            colors.Add(new Color(1, 1, 1));
	            colors.Add(new Color(1, 1, 1));
	            colors.Add(new Color(1, 1, 1));

	            // Índices (duas triangles)
	            indices.Add(startIndex + 0);
	            indices.Add(startIndex + 2);
	            indices.Add(startIndex + 1);
	            indices.Add(startIndex + 1);
	            indices.Add(startIndex + 2);
	            indices.Add(startIndex + 3);
	        }

	        // Adicionar cada face se visível
			if (u) AddFace(ftl, ftr, btl, btr, Vector3.Up, faceUvList[0]);       // era Vector3.Down
			if (d) AddFace(fbl, bbl, fbr, bbr, Vector3.Down, faceUvList[1]);     // era Vector3.Up
			if (l) AddFace(bbl, fbl, btl, ftl, Vector3.Left, faceUvList[2]);     // era Vector3.Back
			if (r) AddFace(fbr, bbr, ftr, btr, Vector3.Right, faceUvList[3]);    // era Vector3.Forward
			if (f) AddFace(fbl, fbr, ftl, ftr, Vector3.Forward, faceUvList[4]);  // era Vector3.Right
			if (b) AddFace(bbr, bbl, btr, btl, Vector3.Back, faceUvList[5]);     // era Vector3.Left  
	    }

	    // Passar para o ArrayMesh
	    array[(int)Mesh.ArrayType.Vertex] = verts.ToArray();
	    array[(int)Mesh.ArrayType.Normal] = normals.ToArray();
	    array[(int)Mesh.ArrayType.TexUV] = uvs.ToArray();
	    array[(int)Mesh.ArrayType.Color] = colors.ToArray();
	    array[(int)Mesh.ArrayType.Index] = indices.ToArray();

	    mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, array);
	    instance.Mesh = mesh;
	    StandardMaterial3D material = new();
	    instance.MaterialOverride = material;
	    // material.AlbedoColor = new Color(0.6f,0.6f, 0.6f);
	    material.AlbedoTexture = GD.Load<Texture2D>("res://icon.svg");
	    // material.ShadingMode = 0;
	    // GD.Print(instance);
	    ChunkMeshs.Add(chunkpos, instance);
	    Root.AddChild(instance);
	}

	public void Delete(Vector3I pos) {
		if (ChunkMeshs.ContainsKey(pos)) {
			ChunkMeshs[pos].QueueFree();
			ChunkMeshs.Remove(pos);
		}
	}
}