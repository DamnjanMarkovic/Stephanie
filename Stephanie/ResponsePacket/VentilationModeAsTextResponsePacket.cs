
using Stephanie.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Stephanie
{
    // class VentilationModeAsTextResponsePacket
    //  Description:    this class is used to represent and parse a setting packet which has been received from the device
    internal class VentilationModeAsTextResponsePacket : ResponsePacket
    {
        #region Constants

        protected const byte SETTING_MESSAGE_ID = 0x44;

        #endregion

        #region Constructors

        public VentilationModeAsTextResponsePacket(byte[] byData)
            : base(byData)
        { }

        #endregion

        #region Functions

        public override List<Parameter> GetParsedData()
        {
            //added for testing on the machine
#if DEBUG
            Debug.WriteLine($"VentilationModeAsTextResponsePacket; Data as byte array: {TestHelper.PrintByteArray(Data)}");
#endif
            List<Parameter> oParamList = new List<Parameter>();
            oParamList.Add(new VentilationModeAsTextParametar(255, Data));
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



