using System;

namespace Stephanie
{
    internal class SpO2ControllerValuesParametar : Parameter
    {

        #region Constructors

        //public SpO2ControllerValuesParametar(byte[] byData, int startIndex)
        //    : base(byData, startIndex)
        //{ }

        public SpO2ControllerValuesParametar(byte id, Int16 value)
            : base(id, value)
        { }

        #endregion

        protected override bool AssignAndValidateValue(short value)
        {
            //added for testing on the machine
#if DEBUG
            Console.WriteLine($"SpO2ControllerValuesParametar; Value: {value}");
#endif
            //  Set Description, using Name and PDF values:

            switch (this.Name)
            {
                case "Controller Status Setpoint":
                    this.m_Description = SetControllerStatusSetpoint(value);
                    break;
                case "Controller Activity Setpoint":
                    this.m_Description = SetControllerActivitySetpoint(value);
                    break;
                case "Intervention Status":
                    this.m_Description = SetInterventionStatus(value);
                    break;
                case "Limitation Type":
                    this.m_Description = SetLimitationType(value);
                    break;
                case "Protocol":
                    this.m_Description = SetProtocol(value);
                    break;
                default:
                    break;
            }
            // Calculating the real value for this parameter
            m_Value = ((float)value / m_Divider).ToString();
            return true;
        }

        private string SetProtocol(short value)
        {
            switch (value)
            {
                case 0: return "Protocol: Intern";
                case 1: return "Protocol: Dash";
                case 2: return "Protocol: Masimo";
                case 3: return "Protocol: Draeger";
                case 4: return "Protocol: Philips";
                case 5: return "Protocol: Carescape";
                default: return string.Empty;
            }
        }

        private string SetLimitationType(short value)
        {
            switch (value)
            {
                case 0: return "Limitation Type: None";
                case 1: return "Limitation Type: Optical";
                case 2: return "Limitation Type: Optical & Acoustic";
                default: return string.Empty;
            }
        }

        private string SetInterventionStatus(short value)
        {
            switch (value)
            {
                case 0: return "Intervention Status: none / no intervention";
                case 1: return "Intervention Status: active intervention (e.g. 1min)";
                default: return string.Empty;
            }
        }

        private string SetControllerActivitySetpoint(short value)
        {
            switch (value)
            {
                case 0: return "Activity: Suspended";
                case 1: return "Activity: Active";
                default: return string.Empty;
            }
        }

        private string SetControllerStatusSetpoint(short value)
        {
            switch (value)
            {
                case 0: return "Status: Off";
                case 1: return "Status: On";
                default: return string.Empty;
            }
        }
    }
}


