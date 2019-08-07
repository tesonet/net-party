namespace NetParty.Contracts.Results
{
    public class Error
    {
        public string Reason { get; set; }
        public string Message { get; set; }

        public static readonly Error Default = new Error
        {
            Reason = "UnexpectedError",
            Message = "Unexpected error has occured, please try again later or contact client support."
        };
    }
}