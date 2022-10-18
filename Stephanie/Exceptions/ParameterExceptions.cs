using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    public partial class Parameter
    {
        internal class InvalidXMLResourceForParamInfoListException : Exception
        {
            public InvalidXMLResourceForParamInfoListException()
                : base(Properties.Resources.ERR_PARAMINFOLIST_XML)
            { }
        }

        internal class InvalidParameterIDException : Exception
        {
            public InvalidParameterIDException(byte id)
                : base(string.Format(Properties.Resources.ERR_PARAMETER_ID, id))
            { }
        }
    }
}
