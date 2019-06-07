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
    public class ProductionRepository : BaseRepository, IProductionRepository
    {
        private const string COLLECTION = "ProductionCollection";

        private readonly IOptions<NoSql> _nosql;
        private readonly DocumentClient _document;

        public ProductionRepository(
            IOptions<NoSql> nosql,
            DocumentClient document) : base(nosql, document)
        {
            _nosql = nosql;
            _document = document;
        }

        public async Task<ICollection<Contract.Production>> List()
        {
            await ValidateDatabase();
            await ValidateCollection(COLLECTION);
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Contract.Production> query = _document.CreateDocumentQuery<Contract.Production>(
                    UriFactory.CreateDocumentCollectionUri(
                        _nosql.Value.Database,
                        COLLECTION), queryOptions);

            return query.ToList();
        }

        public async Task Add(Contract.Production model)
        {
            await ValidateDatabase();
            await ValidateCollection(COLLECTION);
            await CreateDocumentIfNotExists(_nosql.Value.Database, COLLECTION, model);
        }

        public Task Update(Contract.Production model)
        {
            throw new System.NotImplementedException();
        }

        private async Task CreateDocumentIfNotExists(string databaseName, string collectionName, Contract.Production model)
        {
            try
            {
                await _document.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, model.ProductionId.ToString()));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _document.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), model);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task UpdateDocument(string databaseName, string collectionName, Contract.Production model)
        {
            try
            {
                var data = await _document.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, model.ProductionId.ToString()));
            }
            catch (DocumentClientException de)
            {
                //if (de.StatusCode == HttpStatusCode.NotFound)
                //{
                //    await _document.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), model);
                //}
                //else
                //{
                //    throw;
                //}
            }
        }

        
    }
}
