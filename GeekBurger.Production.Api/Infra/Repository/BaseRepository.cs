using System.Threading.Tasks;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;

using GeekBurger.Production.Models;

namespace GeekBurger.Production.Infra.Repository
{
    /// <summary>
    /// Base Repository
    /// </summary>
    public abstract class BaseRepository
    {
        #region| Fields |

        private readonly IOptions<NoSql> _nosql;
        private readonly DocumentClient _document;

        #endregion

        #region| Constructor |

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="nosql">NoSql</param>
        /// <param name="document">DocumentClient</param>
        public BaseRepository(IOptions<NoSql> nosql, DocumentClient document)
        {
            _nosql = nosql;
            _document = document;
        }

        #endregion

        #region| Methods |

        /// <summary>
        /// Validate the database
        /// </summary>
        /// <returns></returns>
        public async Task ValidateDatabase()
        {
            await _document.CreateDatabaseIfNotExistsAsync(new Database { Id = _nosql.Value.Database });
        }

        /// <summary>
        /// Validatye the collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public async Task ValidateCollection(string collection)
        {
            await _document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_nosql.Value.Database), new DocumentCollection { Id = collection });
        } 

        #endregion
    }
}