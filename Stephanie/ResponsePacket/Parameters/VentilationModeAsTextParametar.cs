using System;

namespace Stephanie
{
    internal class VentilationModeAsTextParametar : Parameter
    {

        #region Constructors

        //public VentilationModeAsTextParametar(byte[] byData, int startIndex)
        //    : base(byData, startIndex)
        //{ }

        //public VentilationModeAsTextParametar(byte id, Int16 value)
        //    : base(id, value)
        //{ }

        public VentilationModeAsTextParametar(byte id, byte[] byData)
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
            // Calculating the real value for this parameter
            m_Value = ((float)value / m_Divider).ToString();
            return true;
        }
    }
}



