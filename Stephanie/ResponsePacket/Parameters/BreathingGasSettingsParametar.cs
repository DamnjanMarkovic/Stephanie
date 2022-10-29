using System;

namespace Stephanie
{
    internal class BreathingGasSettingsParametar : Parameter
    {

        #region Constructors

        public BreathingGasSettingsParametar(byte[] byData, int startIndex)
            : base(byData, startIndex)
        { }

        public BreathingGasSettingsParametar(byte id, Int16 value)
            : base(id, value)
        { }

        #endregion

        protected override bool AssignAndValidateValue(short value)
        {
            //added for testing on the machine
#if DEBUG
            Console.WriteLine($"BreathingGasSettingsParametar; Value: {value}");
#endif
            //  Set Description, using Name and PDF values:
            //  I don't know if this should be set here or is something else reading the values and 'converting' them to strings

            #region Set description based on values
            switch (value)
            {
                case 1:
                    this.m_Description = "Vol%";
                    break;
                case 2:
                    this.m_Description = "mmHg";
                    break;
                case 3:
                    this.m_Description = "kPa";
                    break;
                default:
                    break;
            }
            #endregion

            // Calculating the real value for this parameter
            m_Value = ((float)value / m_Divider).ToString();
            return true;
        }
    }
}

