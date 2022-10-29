using System;

namespace Stephanie
{
    internal class BreathingGasMeasuredValuesParametar : Parameter
    {

        #region Constructors

        public BreathingGasMeasuredValuesParametar(byte[] byData, int startIndex)
            : base(byData, startIndex)
        { }

        public BreathingGasMeasuredValuesParametar(byte id, Int16 value)
            : base(id, value)
        { }

        #endregion

        protected override bool AssignAndValidateValue(short value)
        {
            //added for testing on the machine
#if DEBUG
            Console.WriteLine($"BreathingGasMeasuredValuesParametar; Value: {value}");
#endif
            // Calculating the real value for this parameter
            m_Value = ((float)value / m_Divider).ToString();
            return true;
        }
    }
}


