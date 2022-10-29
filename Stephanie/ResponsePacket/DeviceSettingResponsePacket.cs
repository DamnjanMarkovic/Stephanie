using Stephanie.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Stephanie
{
    // class SettingResponsePacket
    //  Description:    this class is used to represent and parse a setting packet which has been received from the device
    internal class DeviceSettingResponsePacket : ResponsePacket
    {
        #region Constants

        protected const byte SETTING_MESSAGE_ID = 0x34;

        #endregion

        #region Constructors

        public DeviceSettingResponsePacket(byte[] byData)
            : base(byData)
        { }

        #endregion

        #region Functions

        public override List<Parameter> GetParsedData()
        {
            //added for testing on the machine
#if DEBUG
            Debug.WriteLine($"DeviceSettingResponsePacket; Data as byte array: {TestHelper.PrintByteArray(Data)}");
#endif
            List<Parameter> oParamList = new List<Parameter>();

            for (int i = 0; i < Data.Length; i += 2)
            {
                if ((int)Data[i] == 33)
                {
                    oParamList.Add(new DeviceSettingsParameter(Data, i));
                    i++;
                }
                else
                {
                    oParamList.Add(new DeviceSettingsParameter(Data[i], (short)(Data[i + 1])));
                }
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
