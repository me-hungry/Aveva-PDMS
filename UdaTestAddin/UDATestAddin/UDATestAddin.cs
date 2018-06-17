using Aveva.ApplicationFramework;

namespace UdaTestAddin
{
    public class UdaTestAddin : IAddin
    {
        #region Implementation of IAddin

        public void Start(ServiceManager serviceManager)
        {
            UdaTestCode.Run();
        }

        public void Stop()
        {
            
        }

        public string Name
        {
            get { return "UDA Code"; }
        }

        public string Description
        {
            get { return "This is a test implementation of UDA Pseudo Attribute Code"; }
        }

        #endregion
    }
}
