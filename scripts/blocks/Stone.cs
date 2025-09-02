using Godot;

public class Stone : Block {

	public Stone(Vector3I position) : base(position) {
		// ¯\_(ツ)_/¯
	}
	public static BlockBuilder Register() {
		return new BlockBuilder()
			.Name("stone")
			.Sprite("stone.png")
			.Type<Stone>();
	}
}