using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using GeekBurger.Production.Interface;
using GeekBurger.Production.Models;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;

namespace GeekBurger.Production.Infra.Repository
{
    /// <summary>
    /// Production repository
    /// </summary>
    public class ProductionRepository : BaseRepository, IProductionRepository
    {
        #region| Fields |

        private const string COLLECTION = "ProductionCollection";

        private readonly IOptions<NoSql> _nosql;
        private readonly DocumentClient _document;

        #endregion

        #region| Constructor |

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductionRepository(IOptions<NoSql> nosql, DocumentClient document) : base(nosql, document)
        {
            _nosql = nosql;
            _document = document;
        }

        #endregion

        #region| Methods |

        /// <summary>
        /// Get all productions
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Add a production
        /// </summary>
        /// <param name="model">Production model</param>        
        public async Task Add(Contract.Production model)
        {
            await ValidateDatabase();
            await ValidateCollection(COLLECTION);
            await CreateDocumentIfNotExists(_nosql.Value.Database, COLLECTION, model);
        }

        /// <summary>
        /// Update a production
        /// </summary>
        /// <param name="model">Production model</param>
        public Task Update(Contract.Production model)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Create document if not exists
        /// </summary>
        /// <param name="databaseName">database name</param>
        /// <param name="collectionName">collection name AKA table name</param>
        /// <param name="model">model</param>
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

        /// <summary>
        /// Update an existing document
        /// </summary>
        /// <param name="databaseName">database name</param>
        /// <param name="collectionName">collection name AKA table name</param>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task UpdateDocument(string databaseName, string collectionName, Contract.Production model)
        {
            try
            {
                var data = await _document.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, model.ProductionId.ToString()));
            }
            catch (DocumentClientException de)
            {
                Trace.WriteLine(de.Message);
            }
        } 
        #endregion
    }
}
