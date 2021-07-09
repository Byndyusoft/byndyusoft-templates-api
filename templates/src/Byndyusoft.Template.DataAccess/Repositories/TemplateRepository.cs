namespace Byndyusoft.Template.DataAccess.Repositories
{
    using Byndyusoft.Data.Relational;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class TemplateRepository : DbSessionConsumer
    {
        public TemplateRepository(IDbSessionAccessor sessionAccessor) : base(sessionAccessor)
        {
        }

        public async Task GetOneAsync(CancellationToken cancellationToken)
        {
            var queryObject = new QueryObject("SELECT 1");
            var result = await DbSession.QueryAsync<int>(queryObject, cancellationToken: cancellationToken);

            Debug.Assert(result.Single() == 1);
        }
    }
}