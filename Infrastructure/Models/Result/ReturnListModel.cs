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
            : base(null, items, totalItems)
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

        public ReturnListModel(string errorMsg, List<ReturnErrorItem> errorList)
            : base(errorMsg, errorList)
        {
        }

        public static new ReturnListModel<TModel> Success(List<TModel> objList, string sucessMessage = null)
        {
            return new ReturnListModel<TModel>(objList) { IsSuccess = true , SuccessMessage = sucessMessage };
        }

        public static new ReturnListModel<TModel> Success(List<TModel> objList, long totalItems, string sucessMessage = null)
        {
            return new ReturnListModel<TModel>(objList, totalItems) { IsSuccess = true, SuccessMessage = sucessMessage };
        }

        public static new ReturnListModel<TModel> Error(Exception ex, int httpCode = 200)
        {
            return new ReturnListModel<TModel>(ex) { IsSuccess = false, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, int httpCode = 200)
        {
            return new ReturnListModel<TModel>(errorMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, Exception ex = null, int httpCode = 200)
        {
            return new ReturnListModel<TModel>(errorMsg, ex) { IsSuccess = false, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            return new ReturnListModel<TModel>(errorMsg, errorList) { IsSuccess = false, HttpCode = httpCode };
        }
    }
}
