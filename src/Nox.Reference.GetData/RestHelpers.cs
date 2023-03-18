﻿using RestSharp;

internal class RestHelper
{
    internal static RestResponse? GetInternetContent(string uri, string accept = "application/json")
    {
        var client = new RestClient(uri);

        var request = new RestRequest() { Method = Method.Get };

        request.AddHeader("Accept", accept);

        var data = client.Execute(request);

        if (string.IsNullOrEmpty(data.Content))
        {
            throw new Exception($"Error retreiving data from {uri}. No data was returned.");
        }

        if (data.ResponseStatus == ResponseStatus.Error)
        {
            throw new Exception($"Error retreiving data from {uri} ({data.ErrorException?.Message})");
        }

        if (data.ResponseStatus == ResponseStatus.Error)
        {
            throw new Exception($"Error retreiving data from {uri} ({data.ErrorException?.Message})");
        }

        return data;
    }
}
