using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Infra.Repository
{
    public abstract class BaseRepository
    {
        private readonly DocumentClient _document;

        public BaseRepository(
            DocumentClient document)
        {
            _document = document;
        }

        public async Task ValidateDatabase()
        {
            await _document.CreateDatabaseIfNotExistsAsync(new Microsoft.Azure.Documents.Database { Id = "Production" });
        }
    }
}
