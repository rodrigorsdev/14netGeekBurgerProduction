using GeekBurger.Production.Contract;
using GeekBurger.Production.Interface;
using GeekBurger.Production.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeekBurger.Production.Infra.Repository
{
    public class AreaRepository : BaseRepository, IAreaRepository
    {
        private readonly IOptions<NoSql> _nosql;
        private readonly DocumentClient _document;

        public AreaRepository(
            IOptions<NoSql> nosql,
            DocumentClient document) : base(nosql, document)
        {
            _nosql = nosql;
            _document = document;
        }

        public async Task Add(Area model)
        {
            await ValidateDatabase();
            await ValidateCollection("AreaCollection");
            await CreateDocumentIfNotExists(_nosql.Value.Database, "AreaCollection", model);
        }

        private async Task CreateDocumentIfNotExists(string databaseName, string collectionName, Area area)
        {
            try
            {
                await _document.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, area.AreaId.ToString()));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _document.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), area);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ICollection<Area>> List()
        {
            await ValidateDatabase();
            await ValidateCollection("AreaCollection");
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Area> query = _document.CreateDocumentQuery<Area>(
                    UriFactory.CreateDocumentCollectionUri(
                        _nosql.Value.Database,
                        "AreaCollection"), queryOptions);

            return query.ToList();
        }
    }
}
