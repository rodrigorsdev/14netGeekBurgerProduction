using GeekBurger.Production.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GeekBurger.Production.Infra.Repository
{
    public abstract class BaseRepository
    {
        private readonly IOptions<NoSql> _nosql;
        private readonly DocumentClient _document;

        public BaseRepository(
            IOptions<NoSql> nosql,
            DocumentClient document)
        {
            _nosql = nosql;
            _document = document;
        }

        public async Task ValidateDatabase()
        {
            await _document.CreateDatabaseIfNotExistsAsync(new Database { Id = _nosql.Value.Database });
        }

        public async Task ValidateCollection(string collection)
        {
            await _document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_nosql.Value.Database), new DocumentCollection { Id = collection });
        }
    }
}