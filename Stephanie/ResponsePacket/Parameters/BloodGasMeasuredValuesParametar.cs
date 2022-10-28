using System;

namespace Stephanie
{
    internal class BloodGasMeasuredValuesParametar : Parameter
    {

        #region Constructors

        public BloodGasMeasuredValuesParametar(byte[] byData, int startIndex)
            : base(byData, startIndex)
        { }

        public BloodGasMeasuredValuesParametar(byte id, Int16 value)
            : base(id, value)
        { }

        #endregion

        protected override bool AssignAndValidateValue(short value)
        {
            // Calculating the real value for this parameter
            m_Value = ((float)value / m_Divider).ToString();
            return true;
        }
    }
}


