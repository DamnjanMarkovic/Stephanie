namespace Stephanie.RequestPacket
{
    internal class AlarmRequestPacket : RequestPacket
    {
        public AlarmRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(AlarmRequestPacket)))
        { }
    }
}
