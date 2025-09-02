using System;

public class BlockBuilder {
	public int id;
    public string name;
    public string sprite;
    public Type type;

    public BlockBuilder Id(int id) {
        this.id = id;
        return this;
    }

    public BlockBuilder Name(string name) {
        this.name = name;
        return this;
    }

    public BlockBuilder Sprite(string sprite) {
        this.sprite = sprite;
        return this;
    }

    public BlockBuilder Type<T>() where T : Block {
        this.type = typeof(T);
        return this;
    }

}