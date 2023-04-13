﻿using Nox.Reference.Abstractions.MacAddresses;

namespace Nox.Reference.MacAddresses
{
    public interface IMacAddressService
    {
        /// <summary>
        /// Get mac address info by mac address.
        /// </summary>
        /// <param name="id">Mac Address identifier</param>
        /// <returns>Max address info</returns>
        IMacAddressInfo? GetMacAddressInfo(string id);
    }
}