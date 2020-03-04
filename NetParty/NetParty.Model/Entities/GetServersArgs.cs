using NetParty.Model.Entities.Enums;

namespace NetParty.Model.Entities
{
    public class GetServersArgs
    {
        public bool UseLocal { get; set; }
        public Order Order { get; set; }
    }
}