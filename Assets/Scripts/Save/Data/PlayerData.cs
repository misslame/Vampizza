using System.IO;

public class PlayerData : SerializableData {
  public float exp;
  public ulong level;
  public long currency;
  public string playerName;
  public ulong citizen;
  public ulong blood;

  public override void Serialize(BinaryWriter writer) {
    writer.Write(exp);
    writer.Write(level);
    writer.Write(currency);
    writer.Write(playerName);
    writer.Write(citizen);
    writer.Write(blood);
  }

  public override void Deserialize(BinaryReader reader) {
    exp = reader.ReadSingle();
    level = reader.ReadUInt64();
    currency = reader.ReadInt64();
    playerName = reader.ReadString();
    citizen = reader.ReadUInt64();
    blood = reader.ReadUInt64();
  }
}
