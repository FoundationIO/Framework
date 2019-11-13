/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Models.Search
{
    public class BaseSearchCriteria : IBaseSearchCriteria
    {
        public BaseSearchCriteria()
        {
            PageSize = 100;
            NumericPageCount = 10;
            Page = 1;
            TotalRowCount = -1;
            CurrentRows = -1;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string Keyword { get; set; }

        public string SortBy { get; set; }

        public bool SortAscending { get; set; }

        public int CurrentRows { get; set; }

        public long TotalRowCount { get; set; }

        public int NumericPageCount { get; set; }

        public int CurrentPageStartRow()
        {
            ValidateMandatoryParams();
            if (TotalRowCount == 0)
            {
                return 0;
            }

            int result = (Page - 1) * PageSize;
            return result + 1;
        }

        public int CurrentPageEndRow()
        {
            ValidateMandatoryParams();
            if (TotalRowCount == 0)
            {
                return 0;
            }

            int result = ((Page - 1) * PageSize) + CurrentRows;
            return result;
        }

        public long PageCount()
        {
            ValidateMandatoryParams();
            long pageCount = TotalRowCount % this.PageSize == 0 ? TotalRowCount / this.PageSize : (TotalRowCount / this.PageSize) + 1;
            return Math.Max(pageCount, 1);
        }

        public int GetSkipValue()
        {
            if (PageSize == 0)
            {
                PageSize = 100;
            }

            if (Page == 0)
            {
                return 0;
            }

            Page -= 1;
            return PageSize * Page;
        }

        private void ValidateMandatoryParams()
        {
            if (TotalRowCount == -1)
            {
                throw new Exception("TotalRowCount should be set to the criteria object before sending to the View");
            }

            if (CurrentRows == -1)
            {
                throw new Exception("CurrentRows should be set to the criteria object before sending to the View");
            }
        }
    }
}
