using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    // class ResponsePacket
    //  Description: An abstract class used to define the sturcture and required validations for each type of response packets.
    internal abstract partial class ResponsePacket
    {

        #region Constants

        public const byte MESSAGE_MIN_SIZE = 4;
        public const ushort CHKSUM_POLYNOM = 0x8810;

        #endregion

        #region Properties
        protected byte m_MessageLength;
        public byte MessageLength
        {
            get { return m_MessageLength; }
        }

        protected byte m_MessageID;
        public byte MessageID
        {
            get { return m_MessageID; }
        }

        protected UInt16 m_CheckSum;
        public UInt16 CheckSum
        {
            get { return m_CheckSum; }
        }

        protected byte[] m_Data = null;
        public byte[] Data
        {
            get { return m_Data; }
        }
	
        #endregion

        #region Constructors

        public ResponsePacket(byte[] byData)
        {
            // Making sure the data arrived was not empty and was bigger or equal to the smaller message possible
            if (byData == null || byData.Length <= MESSAGE_MIN_SIZE)
                throw new InvalidDataReceivedException();
            
            // Validating
            AssignAndValidateLength(byData);
            AssignAndValidateID(byData);
            AssignData(byData);
            AssignAndValidateCheckSum(byData);
        }

        #endregion

        #region Functions 
        
        protected void AssignAndValidateLength(byte[] byData)
        {
            if (byData[0] != byData.Length - 3)
            {
                throw new UnexpectedMessageLengthException(byData.Length, byData[0]);
            }

            m_MessageLength = byData[0];
        }

        protected abstract void AssignAndValidateID(byte[] byData);

        protected void AssignData(byte[] byData)
        {
            m_Data = new byte[MessageLength - 1];
            Array.Copy(byData, 2, m_Data, 0, MessageLength - 1);
        }

        // AssignAndValidateCheckSum
        //  Description:    This function is reponsible to calculate the checksum for the packet and to validate the
        //                  checksum which arrived with the packet.
        //
        //  Input:          byData - the packet bytes
        protected void AssignAndValidateCheckSum(byte[] byData)
        {
            ushort chkSumCalc;
            m_CheckSum = BitConverter.ToUInt16(byData,byData.Length-2);


            chkSumCalc = CalculateCheckSum(byData, 0, byData.Length - 2);

            if (chkSumCalc != CheckSum)
            {
                throw new CheckSumFailedException();
            }
        }

        protected ushort CalculateCheckSum(byte[] byData,int startIndex, int count)
        {
            ushort sum;
            int i,j;

            sum = 0;

            for (i=startIndex; i < startIndex + count; i++)
            {
                sum ^= (ushort)((byData[i] & 0xff) << 8);

                for (j=0; j <= 7; j++)
                {
                    if ((sum & 0x8000) != 0)
                    {
                        sum ^= CHKSUM_POLYNOM;
                        sum = (ushort)((sum << 1) + 1);
                    }
                    else
                    {
                        sum = (ushort)(sum << 1);
                    }
                }
            }

            return (ushort)(sum & 0xffff);
        }

        public abstract List<Parameter> GetParsedData();

        #endregion



    }
}
