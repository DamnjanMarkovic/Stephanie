using MVBSLib;

using Stephanie;
using Stephanie.Helpers;

namespace StephanieUnitTests
{
    class TestableDriver : Driver
    {
        private string ProtocolVersion { get; }
        private new IHelperObject HelperObject { get; }

        public TestableDriver(string protocolVersion, IHelperObject mOHelperObject=null)
        {
            ProtocolVersion = protocolVersion;
            HelperObject = mOHelperObject;
        }
        public override void InitPoolSettings()
        {
            //PoolSettings poolSettings = new PoolSettings(HelperObject);
            //poolSettings.LoadSettings();
         XmlHelper.Init(ProtocolVersion);
        }

    }
}
