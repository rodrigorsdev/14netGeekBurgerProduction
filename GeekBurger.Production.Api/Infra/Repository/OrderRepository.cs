using GeekBurger.Production.Interface;
using GeekBurger.Production.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeekBurger.Production.Infra.Repository
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        private const string COLLECTION = "OrderCollection";

        private readonly IOptions<NoSql> _nosql;
        private readonly DocumentClient _document;

        public OrderRepository(
            IOptions<NoSql> nosql,
            DocumentClient document) : base(nosql, document)
        {
            _nosql = nosql;
            _document = document;
        }

        public async Task Add(Order model)
        {
            await ValidateDatabase();
            await ValidateCollection(COLLECTION);
            await CreateDocumentIfNotExists(_nosql.Value.Database, COLLECTION, model);
        }

        public async Task<Order> GetById(Guid id)
        {
            await ValidateDatabase();
            await ValidateCollection(COLLECTION);
            var query = _document.CreateDocumentQuery(UriFactory.CreateDocumentUri(_nosql.Value.Database, COLLECTION,id.ToString())).ToList();
            var result = query.FirstOrDefault();
            return null;
        }

        public async Task Update(Order model)
        {
            await ValidateDatabase();
            await ValidateCollection(COLLECTION);
            var query = _document.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(_nosql.Value.Database, COLLECTION), "select * from c").ToList();
            var result = query.FirstOrDefault();
            await _document.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_nosql.Value.Database, COLLECTION, result.id), model);
        }

        private async Task CreateDocumentIfNotExists(string databaseName, string collectionName, Order model)
        {
            try
            {
                await _document.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, model.OrderId.ToString()));
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
    }
}
