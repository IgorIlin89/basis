namespace Utility.Misc.Options;

public class ApiUserOptions : IApiOptions
{
    public string ApiUrl { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
}
