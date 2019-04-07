namespace partycli.core.DataAccess
{
    interface IApiSettings
    {
        string ServerUri { get; set; }
        string TokenUri { get; set; }
    }
}