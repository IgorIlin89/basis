using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OnlineShopWeb.Misc;

public class HttpClientWrapperOptions
{
    public string ApiUrl { get; set; }
    public string ApiKey { get; set; }
    public string ApiHost { get; set; }
    public string ApiPort { get; set; }
    public const string SectionName = "ApiClientOptions";

}
