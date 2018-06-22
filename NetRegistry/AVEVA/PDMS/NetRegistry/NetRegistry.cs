namespace AVEVA.PDMS.NetRegistry
{
    using Aveva.PDMS.PMLNet;
    using Microsoft.Win32;
    using System;

    [PMLNetCallable]
    public class NetRegistry
    {
        [PMLNetCallable]
        public void Assign(AVEVA.PDMS.NetRegistry.NetRegistry that)
        {
        }

        [PMLNetCallable]
        public string GetValue(string keyName, string valueName)
        {
            return (string) Registry.GetValue(keyName, valueName, "");
        }
    }
}

