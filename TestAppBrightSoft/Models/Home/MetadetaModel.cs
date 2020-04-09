using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppBrightSoft.Models.Home
{
    public class MetadetaModel
    {
        public List<TableMetadata> tableMetadatas { get; set; }
    }
    public class TableMetadata
    {
        public string TableName { get; set; }
        public List<ColumnData> ColumnDatas { get; set; }
    }
    public class ColumnData
    {
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}
