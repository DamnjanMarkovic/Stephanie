namespace Stephanie.RequestPacket
{
    internal class DataRequestPacket : RequestPacket
    {
        public DataRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(DataRequestPacket)))
        { }
    }
}
