using System.Net.NetworkInformation;

namespace Bwa.Core.Net
{
    public class NetworkPinger : IPinger
    {
        public IPStatus Status(string hostNameOrAddress)
        {
            try
            {
                return new Ping().Send(hostNameOrAddress).Status;
            }
            catch
            {
                return IPStatus.Unknown;
            }
        }

        public bool IsSuccess(string hostNameOrAddress) =>
            Status(hostNameOrAddress) == IPStatus.Success;
    }
}
