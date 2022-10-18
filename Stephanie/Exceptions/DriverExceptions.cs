using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    public partial class Driver
    {
        internal class DriverException : Exception
        {
            public DriverException(string message)
                : base("Driver Exception : " + message)
            { }
        }

        internal class DriverConnectionFailedException : DriverException
        {
            public DriverConnectionFailedException()
                : base(Properties.Resources.ERR_CONN_FAILED)
            { }
        }

        internal class DriverCommErrorException : DriverException
        {
            public DriverCommErrorException()
                : base(Properties.Resources.ERR_COMM_ERROR)
            { }
        }

        internal class DriverTimeoutException : DriverException
        {
            public DriverTimeoutException()
                : base(Properties.Resources.ERR_TIMEOUT)
            { }
        }

        internal class InvalidBedIDException : DriverException
        {
            public InvalidBedIDException()
                : base(Properties.Resources.ERR_BEDID)
            { }
        }
    }
}
