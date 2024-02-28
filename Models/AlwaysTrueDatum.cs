using System.Formats.Cbor;
using Cardano.Sync.Data.Models.Datums;
using CborSerialization;

[CborSerialize(typeof(AlwaysTrueCborConvert))]
public record AlwaysTrueDatum(ulong Number) : IDatum;

public class AlwaysTrueCborConvert : ICborConvertor<AlwaysTrueDatum>
{
    public AlwaysTrueDatum Read(ref CborReader reader)
    {
        var outerTag = reader.ReadTag();
        if ((int)outerTag != 121)
        {
            throw new Exception("Invalid outer tag for Address");
        }
        reader.ReadStartArray();
        var number = reader.ReadUInt64();
        reader.ReadEndArray();
        return new AlwaysTrueDatum(number);
    }

    public void Write(ref CborWriter writer, AlwaysTrueDatum value)
    {
        writer.WriteTag((CborTag)121); // Outermost tag for Address
        writer.WriteStartArray(null);
        writer.WriteUInt64(value.Number);
        writer.WriteEndArray(); // End of Address
    }
}