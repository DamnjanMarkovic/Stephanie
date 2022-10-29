
using Stephanie.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Stephanie
{
    // class DeviceInformationResponsePacket
    //  Description:    this class is used to represent and parse a setting packet which has been received from the device
    internal class DeviceInformationResponsePacket : ResponsePacket
    {
        #region Constants

        protected const byte SETTING_MESSAGE_ID = 0x38;

        #endregion

        #region Constructors

        public DeviceInformationResponsePacket(byte[] byData)
            : base(byData)
        { }

        #endregion

        #region Functions

        public override List<Parameter> GetParsedData()
        {
            //added for testing on the machine
#if DEBUG
            Debug.WriteLine($"DeviceInformationResponsePacket; Data as byte array: {TestHelper.PrintByteArray(Data)}");
            Debug.WriteLine($"DeviceInformationResponsePacket; Data as string: {Encoding.UTF8.GetString(Data)}");
#endif
            List<Parameter> oParamList = new List<Parameter>();

            for (int i = 0; i < Data.Length; i += 2)
            {
                switch ((int)Data[i])
                {
                    case 128:
                        oParamList.Add(new DeviceInformationParametar(Data[i], (short)(Data[i + 1])));
                        break;
                    case 129:
                    case 130:
                    case 131:
                    case 132:
                    case 133:
                        var stringByteArraylength = (int)Data[i + 1];
                        oParamList.Add(new DeviceInformationParametar(Data[i], CreateByteArrayOfString(stringByteArraylength, i + 1)));
                        i += stringByteArraylength;
                        break;

                    case 134:
                        oParamList.Add(new DeviceInformationParametar(Data[i], CreateByteArrayOfString(2, i)));
                        i++;
                        break;
                    case 135:
                        oParamList.Add(new DeviceInformationParametar(Data, i));
                        i++;
                        break;
                    default:
                        break;
                }
            }

            return oParamList;
        }

        private byte[] CreateByteArrayOfString(int stringByteArraylength, int i)
        {
            var stringByteArray = new byte[stringByteArraylength];
            Array.Copy(Data, i + 1, stringByteArray, 0, stringByteArraylength);
            return stringByteArray;
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


