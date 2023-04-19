using System.Net.NetworkInformation;

namespace Bwa.Core.Net
{
    public interface IPinger
    {
        IPStatus Status(string hostNameOrAddress);
        bool IsSuccess(string hostNameOrAddress);
    }
}
