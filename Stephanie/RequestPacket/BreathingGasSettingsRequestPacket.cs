
namespace Stephanie.RequestPacket
{
    internal class BreathingGasSettingsRequestPacket : RequestPacket
    {
        public BreathingGasSettingsRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(BreathingGasSettingsRequestPacket)))
        { }
    }
}
