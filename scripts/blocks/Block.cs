using Godot;

public class Block {
	[System.Flags]
	public enum Faces {
		UP = 1,
		DOWN = 2,
		LEFT = 4,
		RIGHT = 8,
		FRONT = 16,
		BACK = 32
	}


	Faces FacesEnabled = 0;

	public Vector3I Position;
	public string Name;

	public Block(Vector3I position) {
		SetFace(Faces.UP | Faces.DOWN | Faces.LEFT | Faces.RIGHT | Faces.FRONT | Faces.BACK);
		
		Position = position;

	}
	public bool HasAnyFace(Faces flags) {
	    return (FacesEnabled & flags) != 0;
	}

	public bool HasAllFaces(Faces flags) {
	    return (FacesEnabled & flags) == flags;
	}

	public void SetFace(Faces value) {
		FacesEnabled |= value;
	}

	public void RemoveFace(Faces value) {
		FacesEnabled &= ~value;
	}
}
