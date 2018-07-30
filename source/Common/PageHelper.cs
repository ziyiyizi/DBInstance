using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Common
{
    /// <summary>
    /// 分页操作工具
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageHelper<T>
    {
        public  int PageSize { get; }
        public  int RecordCount { get;}
        public  int PageCount { get;}
        public int PageIndex { get;}

        //构造函数
        public PageHelper(int recordCount, int pageSize, int pageIndex, IEnumerable<T> data)
        {
            RecordCount = recordCount;
            PageSize = pageSize;
            PageCount = RecordCount / PageSize;
            if (RecordCount % PageSize != 0)
            {
                PageCount += 1;
            }

            DataSource = data;

            if (pageIndex >= PageCount && PageCount >= 1)
            {
                pageIndex = PageCount - 1;
            }
            PageIndex = pageIndex;

        }

        public IEnumerable<T> GetData()
        {
            return DataSource.Skip(PageIndex * PageSize).Take(PageSize);
        }
        //分页数据源
        public IEnumerable<T> DataSource { get; private set; }
        //每页显示记录的数量

    }
}