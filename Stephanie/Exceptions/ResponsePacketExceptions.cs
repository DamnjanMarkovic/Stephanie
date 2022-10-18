using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    internal partial class ResponsePacket
    {
        internal class InvalidDataReceivedException : Exception
        {
            public InvalidDataReceivedException()
                : base(Properties.Resources.ERR_INVALID_DATA)
            { }
        }

        internal class UnexpectedMessageLengthException : Exception
        {
            public UnexpectedMessageLengthException(int receivedLength, int expectedLength)
                : base(string.Format(Properties.Resources.ERR_MESSAGE_LENGTH, expectedLength, receivedLength))
            { }
        }

        internal class InvalidMessageIDException : Exception
        {
            public InvalidMessageIDException(byte receivedID)
                : base(string.Format(Properties.Resources.ERR_MESSAGE_ID, receivedID))
            { }
        }

        internal class CheckSumFailedException : Exception
        {
            public CheckSumFailedException()
                : base(Properties.Resources.ERR_CHKSUM)
            { }
        }
        
    }
}
