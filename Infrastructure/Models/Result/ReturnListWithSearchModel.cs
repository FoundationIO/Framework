/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Framework.Infrastructure.Constants;
using Framework.Infrastructure.Exceptions;

namespace Framework.Infrastructure.Models.Result
{
    public class ReturnListWithSearchModel<TModel, TSearch> : IReturnModel
        where TSearch : class
    {
        public ReturnListWithSearchModel()
        {
            IsSuccess = false;
        }

        public ReturnListWithSearchModel(TSearch search, List<TModel> items, long totalItems, int httpCode = HttpCodeContants.Success)
        {
            Model = items;
            TotalRecords = totalItems;
            IsSuccess = true;
            Search = search;
            HttpCode = httpCode;
        }

        public ReturnListWithSearchModel(TSearch search, List<TModel> items, int httpCode = HttpCodeContants.Success)
        {
            Model = items;
            TotalRecords = items.Count;
            IsSuccess = true;
            Search = search;
            HttpCode = httpCode;
        }

        public ReturnListWithSearchModel(List<TModel> items, long totalItems, int httpCode = HttpCodeContants.Success)
            : this(null, items, totalItems, httpCode)
        {
        }

        public ReturnListWithSearchModel(List<TModel> items, int httpCode = HttpCodeContants.Success)
            : this(null, items, httpCode)
        {
        }

        public ReturnListWithSearchModel(TSearch search, Exception ex, int httpCode = HttpCodeContants.ErrorOccured)
        {
            Search = search;
            IsSuccess = false;
            HttpCode = httpCode;
            if (ex != null)
            {
                ErrorHolder = new ReturnError(ex);
            }
        }

        public ReturnListWithSearchModel(TSearch search, string errorMsg, Exception ex = null, string internalMsg = null, int httpCode = HttpCodeContants.ErrorOccured)
        {
            Search = search;
            IsSuccess = false;
            HttpCode = httpCode;
            ErrorHolder = new ReturnError(errorMsg, ex, internalMsg);
        }

        public ReturnListWithSearchModel(TSearch search, string errorMsg, List<ReturnErrorItem> errorList, int httpCode = HttpCodeContants.ErrorOccured)
        {
            Search = search;
            IsSuccess = false;
            HttpCode = httpCode;
            ErrorHolder = new ReturnError(errorMsg, errorList, errorMsg);
        }

        public ReturnListWithSearchModel(Exception ex, int httpCode = HttpCodeContants.Success)
            : this((TSearch)null, ex, httpCode)
        {
        }

        public ReturnListWithSearchModel(string errorMsg, Exception ex = null, string internalMsg = null, int httpCode = HttpCodeContants.ErrorOccured)
            : this(null, errorMsg, ex, internalMsg, httpCode)
        {
        }

        public ReturnListWithSearchModel(string errorMsg, List<ReturnErrorItem> errorList, int httpCode = HttpCodeContants.ErrorOccured)
            : this(null, errorMsg, errorList, httpCode)
        {
        }

        public List<TModel> Model { get; set; }

        public TSearch Search { get; set; }

        [JsonIgnore]
        public int ActiveTab { get; set; }

        public long TotalRecords { get; set; }

        public bool IsSuccess { get; set; }

        public string SuccessMessage { get; set; }

        public ReturnError ErrorHolder { get; set; }

        [JsonIgnore]
        public int HttpCode { get; set; }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(TSearch search, List<TModel> objList, string sucessMessage = null, int httpCode = HttpCodeContants.Success)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(search, objList) { IsSuccess = true, SuccessMessage = sucessMessage, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(TSearch search, List<TModel> objList, long totalItems, string sucessMessage = null, int httpCode = HttpCodeContants.Success)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(search, objList, totalItems) { IsSuccess = true, SuccessMessage = sucessMessage, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(List<TModel> objList, long totalItems, string sucessMessage = null, int httpCode = HttpCodeContants.Success)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(objList, totalItems) { IsSuccess = true, SuccessMessage = sucessMessage, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(List<TModel> objList, string sucessMessage = null, int httpCode = HttpCodeContants.Success)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(objList, objList.Count) { IsSuccess = true, SuccessMessage = sucessMessage, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(IReturnModel model)
        {
            return new ReturnListWithSearchModel<TModel, TSearch> { IsSuccess = model.IsSuccess, ErrorHolder = model.ErrorHolder, SuccessMessage = model.SuccessMessage, HttpCode = model.HttpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(Exception ex, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(ex) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, Exception ex, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, ex) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, Exception ex, string internalMsg, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, ex, internalMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, List<ReturnErrorItem> errorList, int httpCode = HttpCodeContants.ErrorOccured)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, errorList) { IsSuccess = false, HttpCode = httpCode };
        }
    }
}
