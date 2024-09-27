using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ASF.ERP.Classes
{
    public static class ASFExtensions
    {
        public static DataTable CopyToDataTable(this IEnumerable<object> data)
        {
            var parseDT = new DataTable();
            data.Select((r, i) => new { Key = i, Value = r }).ToList().ForEach(r =>
            {
                if (r.Key == 0)
                {
                    r.Value.GetType().GetProperties().ToList().ForEach(p =>
                    {
                        parseDT.Columns.Add(p.Name, p.PropertyType);
                    });
                }
                var row = parseDT.NewRow();
                r.Value.GetType().GetProperties().ToList().ForEach(p =>
                {
                    row[p.Name] = p.GetValue(r.Value, null);
                });
                parseDT.Rows.Add(row);

            });
            return parseDT;
        }
    }
}