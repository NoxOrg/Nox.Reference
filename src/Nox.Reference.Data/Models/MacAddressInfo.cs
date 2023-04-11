using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.Data.Models
{
    internal class MacAddressInfo : IMacAddressInfo, INoxReferenceInfo
    {
        public string IEEERegistry { get; private set; }
        public string Id { get; private set; }
        public string MacPrefix { get; private set; }
        public string OrganizationName { get; private set; }
        public string OrganizationAddress { get; private set; }
    }
}