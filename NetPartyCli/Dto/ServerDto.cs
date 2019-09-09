
namespace NetPartyCli.Dto
{
    public class ServerDto
    {
        public ServerDto(string name, int distance)
        {
            Name = name;
            Distance = distance;
        }

        public string Name { get; set; }
        public int  Distance { get; set; }
    }
}
