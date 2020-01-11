using Dapper;
using net_party.Entities.Database;
using net_party.Repositories.Contracts;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace net_party.Repositories
{
    public class AuthTokenRepository : BaseRepository, IAuthTokenRepository
    {
        public AuthTokenRepository(IServiceProvider services) : base(services)
        {
        }

        public async Task<AuthToken> Get()
        {
            if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Closed)
            {
                await _connection.OpenAsync();
            }

            var sql = "SELECT TOP 1 * FROM AuthToken";
            //public static IEnumerable<T> Query<T>(this IDbConnection cnn, string sql, object param = null, SqlTransaction transaction = null, bool buffered = true)
            var result = SqlMapper.Query<AuthToken>(_connection, sql);

            //$"SELECT {queryConfiguration.FieldMapper.BuildSelectProjection(true)} " +
            //         $"FROM (SELECT {queryConfiguration.FieldMapper.BuildSelectProjection(true)}, ROW_NUMBER() OVER ({BuildSort(queryConfiguration.SortParameters)}) AS {ROW_NUMBER} " +
            //         $"FROM (SELECT {queryConfiguration.FieldMapper.BuildSelectProjection()} " +
            //         $"FROM {queryConfiguration.GetTableName()} {BuildFilters(filters)}) AS {originalAs}) AS {orderedAs} " +
            //         $"WHERE {BuildPaging(queryConfiguration.Paging.PageSize, queryConfiguration.Paging.PageNumber)}";

            //await _services.GetService<IDataStore>().Execute(
            //        $@" DELETE FROM [{nameof(BenefitSegment)}]
            //            WHERE {nameof(BenefitSegment)}.{nameof(BenefitSegment.SegmentId)} = @segmentId", parameter);
            return result.FirstOrDefault();
        }
    }
}