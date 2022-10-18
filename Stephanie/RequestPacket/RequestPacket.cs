namespace Stephanie.RequestPacket
{
    public abstract partial class RequestPacket
    {
        #region Properties

        protected string m_RequestCommand = null;
        public string RequestCommand
        {
            get { return m_RequestCommand = null; }
        }
	
        #endregion

        #region Constructors

        public RequestPacket(string command)
        {
            m_RequestCommand = command;
        }

        #endregion

        #region Functions

        public byte[] GetPacketAsBytes()
        {
            return System.Text.Encoding.ASCII.GetBytes(m_RequestCommand);
        }

        #endregion

    }
}
