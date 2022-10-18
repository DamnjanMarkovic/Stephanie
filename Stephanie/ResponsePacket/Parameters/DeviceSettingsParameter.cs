using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    internal class DeviceSettingsParameter : Parameter
    {

        #region Constructors

        public DeviceSettingsParameter(byte[] byData, int startIndex)
            : base(byData, startIndex)
        { }

        public DeviceSettingsParameter(byte id, Int16 value)
            : base(id, value)
        { }

        #endregion

        protected override bool AssignAndValidateValue(short value)
        {
            //  Set Description using Name and PDF values:
            //  This could be in some XML
            //  I don't know if this should be set here or is something else reading the values and 'converting' them to strings

            switch (this.Name)
            {
                case "Trigger Source":
                    this.m_Description = SetTriggerSourceDescription(value);
                    break;
                case "Respiratory Supplement":
                    this.m_Description = SetRespiratorySupplementDescription(value);
                    break;
                case "Closed Loop Ventilation":
                    this.m_Description = SetClosedLoopVentilationDescription(value);
                    break;
                case "Rebreathing Mode":
                    this.m_Description = SetRebreathingModeDescription(value);
                    break;
                default:
                    break;
            }

            m_Value = value.ToString();
            return true;
        }


        #region Set description based on values 
        private string SetTriggerSourceDescription(short value)
        {
            var desc = string.Empty;

            switch (value)
            {
                case 0:
                    desc = "Inactive / No Trigger";
                    break;
                case 1:
                    desc = "Pressure-";
                    break;
                case 2:
                    desc = "Flow-";
                    break;
                case 3:
                    desc = "External Trigger";
                    break;
                default:
                    break;
            }
            return desc;
        }
        private string SetRespiratorySupplementDescription(short value)
        {
            var desc = string.Empty;

            switch (value)
            {
                case 0:
                    desc = "NONE";
                    break;
                case 1:
                    desc = "HFO";
                    break;
                case 2:
                    desc = "PAV (NVI) ";
                    break;
                case 4:
                    desc = "PVI";
                    break;
                case 8:
                    desc = "APNCTRL (Apnoe control)";
                    break;
                case 16:
                    desc = "VLIM (Volume limit)";
                    break;
                case 32:
                    desc = "VG (PRVC)";
                    break;
                case 64:
                    desc = "Backup (STDBU)";
                    break;
                case 128:
                    desc = "PSV";
                    break;
                case 256:
                    desc = "BBRK (Break)";
                    break;
                case 512:
                    desc = "NIV (non invasive)";
                    break;
                case 1024:
                    desc = "TC";
                    break;
                case 2048:
                    desc = "Leakage Compensation ";
                    break;
                case 4096:
                    desc = "Measure";
                    break;
                default:
                    break;
            }
            return desc;
        }
        private string SetClosedLoopVentilationDescription(short value)
        {
            var desc = string.Empty;

            switch (value)
            {
                case 0:
                    desc = "Deactivated";
                    break;
                case 1:
                    desc = "Activated";
                    break;
                default:
                    break;
            }
            return desc;
        }
        private string SetRebreathingModeDescription(short value)
        {
            var desc = string.Empty;

            switch (value)
            {
                case 0:
                    desc = "Closed";
                    break;
                case 1:
                    desc = "Open";
                    break;
                case 2:
                    desc = "Semi Open";
                    break;
                case 3:
                    desc = "Semi Closed";
                    break;
                default:
                    break;
            }
            return desc;
        }

        #endregion
    }
}
