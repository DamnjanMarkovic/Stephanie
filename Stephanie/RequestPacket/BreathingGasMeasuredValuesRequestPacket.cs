
namespace Stephanie.RequestPacket
{
    internal class BreathingGasMeasuredValuesRequestPacket : RequestPacket
    {
        public BreathingGasMeasuredValuesRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(BreathingGasMeasuredValuesRequestPacket)))
        { }
    }
}

