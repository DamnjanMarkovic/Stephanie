
namespace Stephanie.RequestPacket
{
    internal class VentilationModeAsTextRequestPacket : RequestPacket
    {
        public VentilationModeAsTextRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(VentilationModeAsTextRequestPacket)))
        { }
    }
}