using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    // class AlarmResponsePacket
    //  Description:    this class is used to represent and parse an alarm packet which has been received from the device
    internal class AlarmResponsePacket : ResponsePacket
    {
        #region Constants

        protected const byte ALARM_MESSAGE_ID = 0x32;

        #endregion

        #region Constructors

        public AlarmResponsePacket(byte[] byData)
            : base(byData)
        { }

        #endregion

        #region Functions

        public override List<Parameter> GetParsedData()
        {
            List<Parameter> oParamList = new List<Parameter>();

            for (int i = 0; i < Data.Length; i += 3)
            {
                oParamList.Add(new AlarmParameter(Data, i));
            }

            return oParamList;
        }

        protected override void AssignAndValidateID(byte[] byData)
        {
            if (byData[1] != ALARM_MESSAGE_ID)
            {
                throw new InvalidMessageIDException(byData[0]);
            }

            m_MessageID = byData[1];
        }

        #endregion

    }
}
