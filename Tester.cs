using Godot;
using System.Collections.Generic;
using Array = Godot.Collections.Array;
/*
extends MeshInstance3D

func _ready():
	var surface_array = []
	surface_array.resize(Mesh.ARRAY_MAX)

	# PackedVector**Arrays for mesh construction.
	var verts = PackedVector3Array()
	var uvs = PackedVector2Array()
	var normals = PackedVector3Array()
	var indices = PackedInt32Array()

	#######################################
	## Insert code here to generate mesh ##
	#######################################

	# Assign arrays to surface array.
	surface_array[Mesh.ARRAY_VERTEX] = verts
	surface_array[Mesh.ARRAY_TEX_UV] = uvs
	surface_array[Mesh.ARRAY_NORMAL] = normals
	surface_array[Mesh.ARRAY_INDEX] = indices

	# Create mesh surface from mesh array.
	# No blendshapes, lods, or compression used.
	mesh.add_surface_from_arrays(Mesh.PRIMITIVE_TRIANGLES, surface_array)
*/
public partial class Tester : Node3D
{
	private int _rings = 15;
	private int _radialSegments = 15;
	private float _radius = 1;
	public override void _Ready()
	{
		MeshInstance3D instance = GetNode<MeshInstance3D>("Mesh");
		ArrayMesh mesh = (ArrayMesh)instance.Mesh;

		Array array = [];
		array.Resize((int)Mesh.ArrayType.Max);

		List<Vector3> verts = [];
		List<Vector2> uvs = [];
		List<Vector3> normals = [];
		List<int> indices = [];
		List<Color> colors = [];

		int size = 32;
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				Vector3 point = new(x-(size/2), Mathf.Sin(Mathf.Sqrt(x*x+y*y)/4f) * 2f, y-(size/2));
				// if (x%2 == y%4) {
				// 	point.Y -= 1;
				// }
				verts.Add(point);
				uvs.Add(new Vector2(x / (size/2f) + 0.5f, y / (size/2f) + 0.5f));

				Color color = new Color((x+y) / (float)size,(x+y) / (float)size,(x+y) / (float)size);
                colors.Add(color);
				normals.Add(Vector3.Up);

				if (y < size-1 && x < size-1) {
					int topleft = y * size + x;
					int topright = y * size + (x+1);
					int bottomleft = (y+1) * size + x;
					int bottomright = (y+1) * size + (x+1);
					indices.Add(topleft);	// sentido horário
					
					indices.Add(topright);
					indices.Add(bottomleft);
					indices.Add(topright);  
					indices.Add(bottomright);
					indices.Add(bottomleft);
					

				    // indices.Add(topleft);
				    // indices.Add(bottomleft);  // Linha entre topleft e bottomleft
				    // indices.Add(topright);
				    // indices.Add(bottomright); // Linha entre topright e bottomright
				    // indices.Add(topleft);
				    // indices.Add(topright);    // Linha entre topleft e topright
				    // indices.Add(bottomleft);
				    // indices.Add(bottomright); // Linha entre bottomleft e bottomright
				}

			}
		}
		
		GD.Print("Vertices: ", verts.Count);
		GD.Print("Indices: ", indices.Count);

		// Vertex indices.
		// var thisRow = 0;
		// var prevRow = 0;
		// var point = 0;

		// // Loop over rings.
		// for (var i = 0; i < _rings + 1; i++)
		// {
		//     var v = ((float)i) / _rings;
		//     var w = Mathf.Sin(Mathf.Pi * v);
		//     var y = Mathf.Cos(Mathf.Pi * v);

		//     // Loop over segments in ring.
		//     for (var j = 0; j < _radialSegments + 1; j++)
		//     {
		//         var u = ((float)j) / _radialSegments;
		//         var x = Mathf.Sin(u * Mathf.Pi * 2);
		//         var z = Mathf.Cos(u * Mathf.Pi * 2);
		//         var vert = new Vector3(x * _radius * w, y * _radius, z * _radius * w);
		//         verts.Add(vert);
		//         normals.Add(vert.Normalized());
		//         uvs.Add(new Vector2(u, v));
		//         point += 1;

		//         // Create triangles in ring using indices.
		//         if (i > 0 && j > 0)
		//         {
		//             indices.Add(prevRow + j - 1);
		//             indices.Add(prevRow + j);
		//             indices.Add(thisRow + j - 1);

		//             indices.Add(prevRow + j);
		//             indices.Add(thisRow + j);
		//             indices.Add(thisRow + j - 1);
		//         }
		//     }

		//     prevRow = thisRow;
		//     thisRow = point;
		// }

		array[(int)Mesh.ArrayType.Vertex] = verts.ToArray();
		array[(int)Mesh.ArrayType.TexUV] = uvs.ToArray();
		array[(int)Mesh.ArrayType.Normal] = normals.ToArray();
		array[(int)Mesh.ArrayType.Index] = indices.ToArray();
		array[(int)Mesh.ArrayType.Color] = colors.ToArray();

		GD.Print("Count: " + array.Count);
		if (array != null)
			mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, array);
	}
}
