using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    // class SettingResponsePacket
    //  Description:    this class is used to represent and parse a setting packet which has been received from the device
    internal class SettingResponsePacket : ResponsePacket
    {
        #region Constants

        protected const byte SETTING_MESSAGE_ID = 0x33;

        #endregion

        #region Constructors

        public SettingResponsePacket(byte[] byData)
            : base(byData)
        { }

        #endregion

        #region Functions

        public override List<Parameter> GetParsedData()
        {
            List<Parameter> oParamList = new List<Parameter>();

            for (int i = 0; i < Data.Length; i += 3)
            {
                oParamList.Add(new SettingsParameter(Data, i));
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
