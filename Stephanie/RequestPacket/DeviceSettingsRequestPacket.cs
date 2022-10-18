namespace Stephanie.RequestPacket
{
    internal class DeviceSettingsRequestPacket : RequestPacket
    {
        public DeviceSettingsRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(DeviceSettingsRequestPacket)))
        { }
    }
}