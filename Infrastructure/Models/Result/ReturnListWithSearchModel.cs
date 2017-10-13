using System;
using System.Collections.Generic;
using Framework.Infrastructure.Exceptions;
using Framework.Infrastructure.Models.Search;

namespace Framework.Infrastructure.Models.Result
{
    public class ReturnListWithSearchModel<TModel,TSearch> : IReturnModel
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
            : this((TSearch)null, items, totalItems)
        {
        }

        public ReturnListWithSearchModel(List<TModel> items)
            : this((TSearch)null, items)
        {
        }

        public ReturnListWithSearchModel(TSearch search, Exception ex)
        {
            IsSuccess = false;
            ErrorHolder = new Error(ex);
        }

        public ReturnListWithSearchModel(TSearch search, string errorMsg, Exception ex = null)
        {
            IsSuccess = false;
            ErrorHolder = new Error(errorMsg, ex);
        }

        public ReturnListWithSearchModel(TSearch search, string errorMsg, List<ErrorItem> errorList)
        {
            IsSuccess = false;
            ErrorHolder = new Error(errorMsg, errorList);
        }

        public ReturnListWithSearchModel(Exception ex)
            : this((TSearch)null, ex)
        {
        }

        public ReturnListWithSearchModel(string errorMsg, Exception ex = null)
            : this((TSearch)null, errorMsg, ex)
        {
        }

        public ReturnListWithSearchModel(string errorMsg, List<ErrorItem> errorList)
            : this((TSearch)null, errorMsg, errorList)
        {
        }

        public List<TModel> Model { get; private set; }

        public TSearch Search { get; private set; }

        public int ActiveTab { get; set; }

        public long TotalRecords { get; private set; }

        public bool IsSuccess { get; set; }

        public Error ErrorHolder { get; set; }

        public int HttpCode { get; set; }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(TSearch search, List<TModel> objList)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(search, objList) { IsSuccess = true };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(TSearch search, List<TModel> objList, long totalItems)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(search, objList, totalItems) { IsSuccess = true };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(List<TModel> objList, long totalItems)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(objList, totalItems) { IsSuccess = true };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Success(List<TModel> objList)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(objList, objList.Count) { IsSuccess = true };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(Exception ex)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(ex) { IsSuccess = false };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, Exception ex = null)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, ex) { IsSuccess = false };
        }

        public static ReturnListWithSearchModel<TModel, TSearch> Error(string errorMsg, List<ErrorItem> errorList)
        {
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, errorList) { IsSuccess = false };
        }
    }
}
