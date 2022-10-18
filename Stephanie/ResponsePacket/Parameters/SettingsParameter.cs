using System;
using System.Collections.Generic;
using System.Text;

namespace Stephanie
{
    internal class SettingsParameter : Parameter
    {

        #region Constructors

        public SettingsParameter(byte[] byData, int startIndex)
            : base(byData, startIndex)
        { }

        public SettingsParameter(byte id, Int16 value)
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
