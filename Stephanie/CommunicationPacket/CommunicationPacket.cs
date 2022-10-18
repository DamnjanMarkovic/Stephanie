using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    internal partial class CommunicationPacket
    {
        #region Communication Constants
        
        protected const byte STX = 0x2;
        protected const byte ETX = 0x3;
        
        #endregion

        // Disabling the option to create instances of this type
        private CommunicationPacket()
        { }

        #region Static Functions

        // StripCommunicationBytes
        //  Description:    Strips the start and end signals used for communication sync from the received byte array
        //
        //  Input:          byData - the byte array without the communication signals
        //  Output:         a new byte array which includes the start and end signals
        public static byte[] StripCommunicationBytes(byte[] byData)
        {
            byte[] byDest = new byte[byData.Length-2];

            Array.Copy(byData, 1, byDest, 0, byData.Length - 2);

            return byDest;
        }

        // AddCommunicationBytes
        //  Description:    Strips the start and end signals used for communication sync from the received byte array
        //
        //  Input:          byData - the byte array without the communication signals
        //  Output:         a new byte array which includes the start and end signals
        public static byte[] AddCommunicationBytes(byte[] byData)
        {
            byte[] byDest = new byte[byData.Length + 2];
            Array.Copy(byData, 0, byDest, 1, byData.Length);
            byDest[0] = STX;
            byDest[byDest.Length - 1] = ETX;

            return byDest;
        }

        #endregion
    }
}
