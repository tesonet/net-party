using partycli.Contracts.Exceptions;

namespace partycli.Contracts.DTOs
{
    public class ServerDTO
    {
        public ServerDTO(string name, int distance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new PartyException("Server name is required.");

            Name = name;
            Distance = distance;
        }

        public string Name { get; set; }
        public int  Distance { get; set; }
    }
}
