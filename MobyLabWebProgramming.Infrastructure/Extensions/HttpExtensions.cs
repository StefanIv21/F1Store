using System.Text.Json;
using Microsoft.AspNetCore.Http;
using MobyLabWebProgramming.Core.Requests;

namespace MobyLabWebProgramming.Infrastructure.Extensions;

public static class HttpExtensions
{
    public static void AddPagination(this HttpResponse response, MetaData metaData)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Add("Pagination",JsonSerializer.Serialize(metaData,options));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

    }
}