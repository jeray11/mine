using mine.core.Data;
using mine.core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data
{
    public class EfRepository<T> : IRepository<T> where T: BaseEntity
    {

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        public IQueryable<T> TableNoTracking
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
