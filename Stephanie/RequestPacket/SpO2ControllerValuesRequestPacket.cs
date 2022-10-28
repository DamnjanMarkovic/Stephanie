namespace Stephanie.RequestPacket
{
    internal class SpO2ControllerValuesRequestPacket : RequestPacket
    {
        public SpO2ControllerValuesRequestPacket()
            : base(RequestCommands.GetRequestCommand(typeof(SpO2ControllerValuesRequestPacket)))
        { }
    }
}