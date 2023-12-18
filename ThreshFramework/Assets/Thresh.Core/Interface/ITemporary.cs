namespace Thresh.Core.Interface
{
    public interface ITemporary
    {
        string Name { get; }

        bool      GetBool();
        byte      GetByte();
        int       GetInt();
        long      GetLong();
        float     GetFloat();
        string    GetString();
    }
}