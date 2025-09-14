using Godot;

public class Grass : Block {

	public Grass(Vector3I position) : base(position) {
		// ¯\_(ツ)_/¯
		Name = "grass";
	}
	public static BlockBuilder Register() {
		return new BlockBuilder()
			.Name("grass")
			.Sprite("grass.png")
			.Type<Grass>();
	}
}