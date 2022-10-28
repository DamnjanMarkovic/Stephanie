using System.Collections.Generic;

namespace Stephanie
{
    // class SpO2ControllerValuesResponsePacket
    //  Description:    this class is used to represent and parse a setting packet which has been received from the device
    internal class SpO2ControllerValuesResponsePacket : ResponsePacket
    {
        #region Constants

        protected const byte SETTING_MESSAGE_ID = 0x43;

        #endregion

        #region Constructors

        public SpO2ControllerValuesResponsePacket(byte[] byData)
            : base(byData)
        { }

        #endregion

        #region Functions

        public override List<Parameter> GetParsedData()
        {
            List<Parameter> oParamList = new List<Parameter>();

            for (int i = 0; i < Data.Length; i += 2)
            {
                oParamList.Add(new SpO2ControllerValuesParametar(Data[i], (short)(Data[i + 1])));
            }

            return oParamList;
        }

        protected override void AssignAndValidateID(byte[] byData)
        {
            if (byData[1] != SETTING_MESSAGE_ID)
            {
                throw new InvalidMessageIDException(byData[0]);
            }

            m_MessageID = byData[1];
        }

        #endregion

    }
}


