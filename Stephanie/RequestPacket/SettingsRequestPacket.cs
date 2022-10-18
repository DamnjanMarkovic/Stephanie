namespace Stephanie.RequestPacket
{
    internal class SettingsRequestPacket : RequestPacket
    {
        public SettingsRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(SettingsRequestPacket)))
        { }
    }
}
