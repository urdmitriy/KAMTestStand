using System;

namespace KAMTestStand;

public struct DataTestT
{
    public ResultE result;
    public UInt32 value;
}
    
public struct DiscoveryDtoT
{
    public UInt32 command_id;
    public DataTestT data;
    public UInt16 crc;

    public override string ToString()
    {
        string result =
            $"{command_id.ToString("0000")}" +
            $"{((int)data.result).ToString("0000")}" +
            $"{data.value.ToString("0000")}" +
            $"{crc.ToString("0000")}";
        
        return result;
    }
}