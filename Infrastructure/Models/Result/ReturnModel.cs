/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using Framework.Infrastructure.Exceptions;

namespace Framework.Infrastructure.Models.Result
{
    public class ReturnModel<T> : IReturnModel
    {
        public ReturnModel(T result)
        {
            Model = result;
            IsSuccess = true;
        }

        public ReturnModel(Exception ex)
        {
            IsSuccess = false;
            ErrorHolder = new Error(ex);
        }

        public ReturnModel(string errorMsg, Exception ex = null)
        {
            IsSuccess = false;
            ErrorHolder = new Error(errorMsg, ex);
        }

        public ReturnModel(string errorMsg, List<ErrorItem> errorList)
        {
            IsSuccess = false;
            ErrorHolder = new Error(errorMsg, errorList);
        }

        public T Model { get; set; }

        public bool IsSuccess { get; set; }

        public Error ErrorHolder { get; set; }

        public int ActiveTab { get; set; }

        public int HttpCode { get; set; }

        public static ReturnModel<T> Success(T obj)
        {
            return new ReturnModel<T>(obj) { IsSuccess = true };
        }

        public static ReturnModel<T> Error(Exception ex)
        {
            return new ReturnModel<T>(ex) { IsSuccess = false };
        }

        public static ReturnModel<T> Error(string errorMsg, Exception ex = null)
        {
            return new ReturnModel<T>(errorMsg, ex) { IsSuccess = false };
        }

        public static ReturnModel<T> Error(string errorMsg, List<ErrorItem> errorList)
        {
            return new ReturnModel<T>(errorMsg, errorList) { IsSuccess = false };
        }

        public static ReturnModel<T> Error(string errorMsg, List<string> errorMsgList)
        {
            var errorList = new List<ErrorItem>();
            foreach (var msg in errorMsgList)
            {
                errorList.Add(new ErrorItem() { Value = msg });
            }

            return new ReturnModel<T>(errorMsg, errorList) { IsSuccess = false };
        }

        public static ReturnModel<T> Error(List<string> errorMsgList)
        {
            return Error("", errorMsgList);
        }
    }
}
