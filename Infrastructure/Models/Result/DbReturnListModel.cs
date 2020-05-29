/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Collections.Generic;

namespace Framework.Infrastructure.Models.Result
{
    public class DbReturnListModel<T>
    {
        public DbReturnListModel()
        {
        }

        public DbReturnListModel(List<T> result)
        {
            this.Result = result;
            this.TotalRows = result.Count;
        }

        public DbReturnListModel(List<T> result, long totalRows)
        {
            this.Result = result;
            this.TotalRows = totalRows;
        }

        public List<T> Result { get; set; }

        public long TotalRows { get; set; }
    }
}
