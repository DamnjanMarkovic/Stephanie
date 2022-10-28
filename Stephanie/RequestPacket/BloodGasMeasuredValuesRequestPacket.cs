
namespace Stephanie.RequestPacket
{
    internal class BloodGasMeasuredValuesRequestPacket : RequestPacket
    {
        public BloodGasMeasuredValuesRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(BloodGasMeasuredValuesRequestPacket)))
        { }
    }
}

