using Godot;

public class Ball : Block {

	public Ball(Vector3I position) : base(position) {
		// ¯\_(ツ)_/¯
		Name = "ball";
	}
	public static BlockBuilder Register() {
		return new BlockBuilder()
			.Name("ball")
			.Sprite("ball.png")
			.Type<Ball>();
	}
}