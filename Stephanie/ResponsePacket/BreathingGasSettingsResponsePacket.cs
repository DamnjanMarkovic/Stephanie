using Stephanie.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Stephanie
{
    // class BreathingGasSettingsResponsePacket
    //  Description:    this class is used to represent and parse a setting packet which has been received from the device
    internal class BreathingGasSettingsResponsePacket : ResponsePacket
    {
        #region Constants

        protected const byte SETTING_MESSAGE_ID = 0x35;

        #endregion

        #region Constructors

        public BreathingGasSettingsResponsePacket(byte[] byData)
            : base(byData)
        { }

        #endregion

        #region Functions

        public override List<Parameter> GetParsedData()
        {
            //added for testing on the machine
#if DEBUG
            Debug.WriteLine($"BreathingGasSettingsResponsePacket; Data as byte array: {TestHelper.PrintByteArray(Data)}");
            Debug.WriteLine($"BreathingGasSettingsResponsePacket; Data as string: {Encoding.UTF8.GetString(Data)}");
#endif
            List<Parameter> oParamList = new List<Parameter>();

            for (int i = 0; i < Data.Length; i += 2)
            {
                oParamList.Add(new BreathingGasSettingsParametar(Data[i], (short)(Data[i + 1])));
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


