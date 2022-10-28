
namespace Stephanie.RequestPacket
{
    internal class DeviceInformationRequestPacket : RequestPacket
    {
        public DeviceInformationRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(DeviceInformationRequestPacket)))
        { }
    }
}


