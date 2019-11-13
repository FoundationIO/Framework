/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Framework.Infrastructure.Exceptions;

namespace Framework.Infrastructure.Models.Result
{
    public class ReturnListWithSearchModel<TModel, TSearch> : IReturnModel
        where TSearch : class
    {
        public ReturnListWithSearchModel(TSearch search, List<TModel> items, long totalItems)
        {
            Model = items;
            TotalRecords = totalItems;
            IsSuccess = true;
            Search = search;
        }

        public ReturnListWithSearchModel(TSearch search, List<TModel> items)
        {
            Model = items;
            TotalRecords = items.Count;
            IsSuccess = true;
            Search = search;
        }

        public ReturnListWithSearchModel(List<TModel> items, long totalItems)
            : this(null, items, totalItems)
        {
        }

        public ReturnListWithSearchModel(List<TModel> items)
            : this(null, items)
        {
        }

        public ReturnListWithSearchModel(TSearch search, Exception ex)
        {
            Search = search;
            IsSuccess = false;
            ErrorHolder = new ReturnError(ex);
        }

        public ReturnListWithSearchModel(TSearch search, string errorMsg, Exception ex = null)
        {
            Search = search;
            IsSuccess = false;
            ErrorHolder = new ReturnError(errorMsg, ex);
        }

        public ReturnListWithSearchModel(TSearch search, string errorMsg, List<ReturnErrorItem> errorList)
        {
            Search = search;
            IsSuccess = false;
            ErrorHolder = new ReturnError(errorMsg, errorList, errorMsg);
        }

        public ReturnListWithSearchModel(Exception ex)
            : this((TSearch)null, ex)
        {
        }

        public ReturnListWithSearchModel(string errorMsg, Exception ex = null)
            : this(null, errorMsg, ex)
        {
        }

        public ReturnListWithSearchModel(string errorMsg, List<ReturnErrorItem> errorList)
            : this(null, errorMsg, errorList)
        {
        }

        public List<TModel> Model { get; private set; }

        public TSearch Search { get; private set; }

        [JsonIgnore]
        public int ActiveTab { get; set; }

        public long TotalRecords { get; private set; }

        public bool IsSuccess { get; set; }

        public string SuccessMessage { get; set; }

        public ReturnError ErrorHolder { get; set; }

        [JsonIgnore]
        public int HttpCode { get; set; }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(TSearch search, List<TModel> objList, string sucessMessage = null)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(search, objList) { IsSuccess = true , SuccessMessage = sucessMessage };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(TSearch search, List<TModel> objList, long totalItems, string sucessMessage = null)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(search, objList, totalItems) { IsSuccess = true, SuccessMessage = sucessMessage };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(List<TModel> objList, long totalItems, string sucessMessage = null)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(objList, totalItems) { IsSuccess = true, SuccessMessage = sucessMessage };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(List<TModel> objList, string sucessMessage = null)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(objList, objList.Count) { IsSuccess = true, SuccessMessage = sucessMessage };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(Exception ex, int httpCode = 200)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(ex) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, int httpCode = 200)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, Exception ex = null, int httpCode = 200)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, ex) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, errorList) { IsSuccess = false, HttpCode = httpCode };
        }
    }
}
