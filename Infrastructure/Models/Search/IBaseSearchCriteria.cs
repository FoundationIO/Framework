/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
namespace Framework.Infrastructure.Models.Search
{
    public interface IBaseSearchCriteria
    {
        int Page { get; set; }

        int PageSize { get; set; }

        string Keyword { get; set; }

        string SortBy { get; set; }

        bool SortAscending { get; set; }

        int CurrentRows { get; set; }

        long TotalRowCount { get; set; }

        int NumericPageCount { get; set; }
    }
}