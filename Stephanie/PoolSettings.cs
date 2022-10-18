using System;
using System.Collections.Generic;
using System.Text;

using Imdsoft.SettingsSupport;

using MVBSLib;

namespace Stephanie
{
    [SettingSource(SettingSourceAttribute.DriverSettingSources.PoolSettings)]
    public class PoolSettings : CoreSettings
    {

       
        public PoolSettings (IHelperObject oHelperObject) : base(oHelperObject)
        {
        }

        public PoolSettings (IHelperObject oHelperObject, Imdsoft.DriverInfra.Logic.DbObject oDbObject) : base(oHelperObject, oDbObject)
        {
        }

        [DefaultValueAttibute("")]
        public string ProtocolVersion { get; set; }

    }
}
