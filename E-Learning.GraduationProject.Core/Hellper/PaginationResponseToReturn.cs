using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Hellper
{
    public class PaginationResponseToReturn<TEntity>
    {

        public PaginationResponseToReturn(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; } // no. of all data 
        public IEnumerable<TEntity> Data { get; set; }

        
    }
}
