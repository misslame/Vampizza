using System.IO;

public abstract class SerializableData {
    public abstract void Serialize(BinaryWriter bw);
    public abstract void Deserialize(BinaryReader br);
}
