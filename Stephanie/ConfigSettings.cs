using Imdsoft.SettingsSupport;

using MVBSLib;

namespace Stephanie
{
    [SettingSource(SettingSourceAttribute.DriverSettingSources.ConfigurationSettings)]
    public class ConfigSettings : CoreSettings
    {
        public ConfigSettings(IHelperObject helper)
            : base(helper)
        {
        }

        [BedId]
        public int BedID { get; set; }
    }
}
