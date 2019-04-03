/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using Framework.Infrastructure.Exceptions;
using Framework.Infrastructure.Models.Search;

namespace Framework.Infrastructure.Models.Result
{
    public class ReturnListModel<TModel> : ReturnListWithSearchModel
        <TModel, BaseSearchCriteria>
    {
        public ReturnListModel(List<TModel> items, long totalItems)
            : base(null,items, totalItems)
        {
        }

        public ReturnListModel(List<TModel> items)
            : base(items)
        {
        }

        public ReturnListModel(Exception ex)
            : base(ex)
        {
        }

        public ReturnListModel(string errorMsg, Exception ex = null)
            : base(errorMsg, ex)
        {
        }

        public ReturnListModel(string errorMsg, List<ErrorItem> errorList)
            : base(errorMsg, errorList)
        {
        }

        public static new ReturnListModel<TModel> Success(List<TModel> objList)
        {
            return new ReturnListModel<TModel>(objList) { IsSuccess = true };
        }

        public static new ReturnListModel<TModel> Success(List<TModel> objList, long totalItems)
        {
            return new ReturnListModel<TModel>(objList, totalItems) { IsSuccess = true };
        }

        public static new ReturnListModel<TModel> Error(Exception ex)
        {
            return new ReturnListModel<TModel>(ex) { IsSuccess = false };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, Exception ex = null)
        {
            return new ReturnListModel<TModel>(errorMsg, ex) { IsSuccess = false };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, List<ErrorItem> errorList)
        {
            return new ReturnListModel<TModel>(errorMsg, errorList) { IsSuccess = false };
        }
    }
}
