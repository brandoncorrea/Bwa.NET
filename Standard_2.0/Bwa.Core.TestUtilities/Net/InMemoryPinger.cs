using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Bwa.Core.TestUtilities.Net
{
    public class InMemoryPinger
    {
        public List<string> Invocations = new List<string>();
        public Dictionary<string, IPStatus> MachineStatuses { get; set; } = new Dictionary<string, IPStatus>();
        public Action<string> SimulatePing { get; set; }

        public bool IsSuccess(string hostNameOrAddress) =>
            Status(hostNameOrAddress) == IPStatus.Success;

        public IPStatus Status(string hostNameOrAddress)
        {
            Invocations.Add(hostNameOrAddress);
            SimulatePing?.Invoke(hostNameOrAddress);
            return MachineStatuses.ContainsKey(hostNameOrAddress)
                ? MachineStatuses[hostNameOrAddress]
                : IPStatus.Unknown;
        }
    }
}
