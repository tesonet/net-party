using Dapper.Contrib.Extensions;

namespace net_party.Entities.Interfaces
{
    public interface IEntity
    {
        [Key]
        long Id { get; set; }
    }
}