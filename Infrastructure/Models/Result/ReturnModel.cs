﻿/**
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
    public class ReturnModel<T> : IReturnModel
    {
        public ReturnModel(T result, int httpCode = 200)
        {
            Model = result;
            IsSuccess = true;
            HttpCode = httpCode;
        }

        public ReturnModel(Exception ex, int httpCode = 200)
        {
            IsSuccess = false;
            HttpCode = httpCode;
            ErrorHolder = new ReturnError(ex);
        }

        public ReturnModel(ReturnError error, int httpCode = 200)
        {
            IsSuccess = false;
            HttpCode = httpCode;
            ErrorHolder = error;
        }

        public ReturnModel(string errorMsg, Exception ex = null, string internalErrorMsg = null, int httpCode = 200)
        {
            IsSuccess = false;
            HttpCode = httpCode;
            ErrorHolder = new ReturnError(errorMsg, ex , internalErrorMsg);
        }

        public ReturnModel(string errorMsg, List<ReturnErrorItem> errorList, string internalErrorMsg = null, int httpCode = 200)
        {
            IsSuccess = false;
            HttpCode = httpCode;
            ErrorHolder = new ReturnError(errorMsg, errorList, internalErrorMsg);
        }

        public T Model { get; set; }

        public bool IsSuccess { get; set; }

        public string SuccessMessage { get; set; }

        public ReturnError ErrorHolder { get; set; }

        [JsonIgnore]
        public int ActiveTab { get; set; }

        [JsonIgnore]
        public int HttpCode { get; set; }

        public static ReturnModel<TReturn> SimpleResult<TReturn>(TReturn result, string errorMsg, string internalErrorMsg = null, int httpCode = 200)
            where TReturn : class
        {
            if (result == null)
            {
                return ReturnModel<TReturn>.Error(errorMsg, internalErrorMsg, httpCode);
            }
            else
            {
                return ReturnModel<TReturn>.Success(result);
            }
        }

        public static ReturnModel<bool> SimpleResult(bool result, string errorMsg, string internalErrorMsg = null, int httpCode = 200)
        {
            if (!result)
            {
                return ReturnModel<bool>.Error(errorMsg, internalErrorMsg , httpCode);
            }
            else
            {
                return ReturnModel<bool>.Success(true);
            }
        }

        public static ReturnModel<T> Success(T obj, int httpCode = 200)
        {
            return new ReturnModel<T>(obj) { IsSuccess = true , HttpCode = httpCode };
        }

        public static ReturnModel<T> Error(ReturnError error, int httpCode = 200)
        {
            return new ReturnModel<T>("") { IsSuccess = false, HttpCode = httpCode , ErrorHolder = error };
        }

        public static ReturnModel<T> Error(Exception ex, string internalErrorMsg = null, int httpCode = 200)
        {
            return new ReturnModel<T>(null, ex, internalErrorMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnModel<T> Error(string errorMsg, Exception ex, string internalErroMsg = null, int httpCode = 200)
        {
            return new ReturnModel<T>(errorMsg, ex, internalErroMsg) { IsSuccess = false , HttpCode = httpCode };
        }

        public static ReturnModel<T> Error(string errorMsg, string internalErrorMsg = null, int httpCode = 200)
        {
            return new ReturnModel<T>(errorMsg, (Exception)null, internalErrorMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnModel<T> Error(string errorMsg, List<ReturnErrorItem> errorList, string internalErrorMsg = null, int httpCode = 200)
        {
            return new ReturnModel<T>(errorMsg, errorList, internalErrorMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnModel<T> Error(string errorMsg, List<string> errorMsgList, string internalErrorMsg = null, int httpCode = 200)
        {
            var errorList = new List<ReturnErrorItem>();
            foreach (var msg in errorMsgList)
            {
                errorList.Add(new ReturnErrorItem() { Value = msg });
            }

            return new ReturnModel<T>(errorMsg, errorList, internalErrorMsg) { IsSuccess = false, HttpCode = httpCode };
        }

        public static ReturnModel<T> Error(List<string> errorMsgList, int httpCode = 200)
        {
            return Error("", errorMsgList,"", httpCode);
        }

        public static ReturnModel<T> Error(List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            return Error("", errorList, "", httpCode);
        }
    }
}
