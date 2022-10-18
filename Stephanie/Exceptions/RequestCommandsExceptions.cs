using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    internal partial class RequestCommands
    {
        internal class UnrecognizedPacketTypeException : Exception
        {
            public UnrecognizedPacketTypeException(string typeName)
                : base(string.Format(Properties.Resources.ERR_UNRECOGNIZED_PACKETTYPE, typeName))
            { }
        }

        internal class InvalidXMLResourceForRequestCommandsException : Exception
        {
            public InvalidXMLResourceForRequestCommandsException()
                : base(Properties.Resources.ERR_REQUESTCOMMANDS_XML)
            { }
        }

        internal class RequestPacketTypeNotFoundException : Exception
        {
            public RequestPacketTypeNotFoundException(string sPacketType)
                : base(string.Format(Properties.Resources.ERR_REQUESTPACKET_TYPE_NOT_FOUND, sPacketType))
            { }
        }
    }
}
