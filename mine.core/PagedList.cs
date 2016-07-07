using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly int _totalCount;
        private readonly int _totalPages;
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0)
                throw new ArgumentOutOfRangeException("pageIndex必须大于0");
            if (PageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize必须大于0");
            this._totalCount = source.Count();
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;
            this._totalPages = (_totalCount - 1) / _pageSize + 1;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total count</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0)
                throw new ArgumentOutOfRangeException("pageIndex必须大于0");
            if (PageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize必须大于0");
            this._totalCount = source.Count();
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;
            this._totalPages = (_totalCount - 1) / _pageSize + 1;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
        public int PageIndex
        {
            get { return _pageIndex; }
        }

        public int PageSize
        {
            get { return _pageSize; }
        }

        public int TotalCount
        {
            get { return _totalCount; }
        }

        public int TotalPages
        {
            get { return _totalPages; }
        }

        public bool HasPreviousPage
        {
            get { return _pageIndex > 0; }
        }

        public bool HasNextPage
        {
            get { return _pageIndex+1 < _totalPages; }
        }
    }
}
