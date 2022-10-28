using System;

namespace Stephanie
{
    internal class DeviceInformationParametar : Parameter
    {

        #region Constructors

        public DeviceInformationParametar(byte[] byData, int startIndex)
            : base(byData, startIndex)
        { }

        public DeviceInformationParametar(byte id, Int16 value)
            : base(id, value)
        { }

        public DeviceInformationParametar(byte id, byte[] byData)
            : base(id, byData)
        { }

        #endregion

        protected override bool AssignAndValidateStringValue(string value)
        {
            //  Set Description, using Name and PDF values:
            this.m_Description = value;

            // Set Value for this parameter
            m_Value = value;
            return true;
        }

        protected override bool AssignAndValidateValue(short value)
        {
            //Set Description

            if (m_Name.Equals("Pressure Unit"))
                m_Description = SetDescription(value);


            // Calculating the real value for this parameter
            m_Value = ((float)value / m_Divider).ToString();
            return true;
        }

        private string SetDescription(short value)
        {
            string description = string.Empty;
            switch (value)
            {
                case 0:
                    description = "mbar";
                    break;
                case 1:
                    description = "cmH2O";
                    break;
                case 2:
                    description = "hPa";
                    break;
                default:
                    break;
            }
            return description;
        }
    }
}


