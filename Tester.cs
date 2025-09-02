using Godot;
using System.Collections.Generic;
using Array = Godot.Collections.Array;

public partial class Tester : Node3D {
	private WorldController world;

	public override void _Ready() {
        world = new(this);
		world.UdpateChunks();
	}
}