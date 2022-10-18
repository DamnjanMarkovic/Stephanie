using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    // class DataResponsePacket
    //  Description:    this class is used to represent and parse a data packet which has been received from the device
    internal partial class DataResponsePacket : ResponsePacket
    {
        #region Constants

        protected const byte DATA_MESSAGE_ID = 0x31;
        
        #endregion

        #region Constructors

        public DataResponsePacket(byte[] byData)
            : base(byData)
        { }

        #endregion

        #region Functions

        public override List<Parameter> GetParsedData()
        {
            List<Parameter> oParamList = new List<Parameter>();

            for (int i = 0; i < Data.Length; i += 3)
            {
                oParamList.Add(new DataParameter(Data, i));
            }

            return oParamList;
        }

        protected override void AssignAndValidateID(byte[] byData)
        {
            if (byData[1] != DATA_MESSAGE_ID)
            {
                throw new InvalidMessageIDException(byData[0]);
            }

            m_MessageID = byData[1];
        }

        #endregion

    }
}
