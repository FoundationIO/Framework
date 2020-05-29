/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Exceptions;
using Framework.Infrastructure.Models.Search;

namespace Framework.Infrastructure.Models.Result
{
    public class ReturnListModel<TModel> : ReturnListWithSearchModel
        <TModel, BaseSearchCriteria>
    {
        public ReturnListModel()
            : base()
        {
        }

        public ReturnListModel(List<TModel> items, long totalItems, int httpCode = HttpCodeContants.Success)
            : base(null, items, totalItems, httpCode)
        {
        }

        public ReturnListModel(List<TModel> items, int httpCode = HttpCodeContants.Success)
            : base(items, httpCode)
        {
        }

        public ReturnListModel(Exception ex, int httpCode = HttpCodeContants.ErrorOccured)
            : base(ex, httpCode)
        {
        }

        public ReturnListModel(string errorMsg, Exception ex = null, string internalMsg = null, int httpCode = HttpCodeContants.ErrorOccured)
            : base(errorMsg, ex, internalMsg, httpCode)
        {
        }

        public ReturnListModel(string errorMsg, List<ReturnErrorItem> errorList, int httpCode = HttpCodeContants.ErrorOccured)
            : base(errorMsg, errorList, httpCode)
        {
        }

        public static new ReturnListModel<TModel> Success(List<TModel> objList, string sucessMessage = null, int httpCode = HttpCodeContants.Success)
        {
            return new ReturnListModel<TModel>(objList) { IsSuccess = true, SuccessMessage = sucessMessage, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Success(List<TModel> objList, long totalItems, string sucessMessage = null, int httpCode = HttpCodeContants.Success)
        {
            return new ReturnListModel<TModel>(objList, totalItems) { IsSuccess = true, SuccessMessage = sucessMessage, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Error(IReturnModel model)
        {
            return new ReturnListModel<TModel> { IsSuccess = model.IsSuccess, ErrorHolder = model.ErrorHolder, SuccessMessage = model.SuccessMessage,  HttpCode = model.HttpCode };
        }

        public static new ReturnListModel<TModel> Error(Exception ex, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListModel<TModel>(ex) { IsSuccess = false, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListModel<TModel>(errorMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, Exception ex, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListModel<TModel>(errorMsg, ex) { IsSuccess = false, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, Exception ex, string internalMsg, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListModel<TModel>(errorMsg, ex, internalMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static new ReturnListModel<TModel> Error(string errorMsg, List<ReturnErrorItem> errorList, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListModel<TModel>(errorMsg, errorList) { IsSuccess = false, HttpCode = httpCode };
        }
    }
}
