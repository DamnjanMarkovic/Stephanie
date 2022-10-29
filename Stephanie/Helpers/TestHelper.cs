using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie.Helpers
{
    //Added for testing
    public class TestHelper
    {
        public static string PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("new byte[] { ");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
