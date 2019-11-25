/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using Framework.Infrastructure.Exceptions;
using Framework.Infrastructure.Logging;

namespace Framework.Infrastructure.Models.Result
{
    public static class ReturnModelExtensions
    {
        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, int httpCode)
        {
            log.Error(errorMsg);
            return ReturnModel<T>.Error(errorMsg, httpCode: httpCode);
        }

        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, Exception ex = null, int httpCode = 200)
        {
            log.Error(ex, errorMsg);
            return ReturnModel<T>.Error(errorMsg, ex,httpCode: httpCode);
        }

        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            log.Error(errorMsg);
            return ReturnModel<T>.Error(errorMsg, errorList, httpCode: httpCode);
        }

        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, string logMsg, Exception ex = null, int httpCode = 200)
        {
            log.Error(logMsg);
            return ReturnModel<T>.Error(errorMsg, ex, httpCode: httpCode);
        }

        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, ReturnError returnError, int httpCode = 200)
        {
            log.Error(returnError);
            return ReturnModel<T>.Error(returnError, httpCode: httpCode);
        }

        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, string logMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            log.Error(errorMsg + logMsg);
            return ReturnModel<T>.Error(errorMsg, errorList, httpCode: httpCode);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, int httpCode)
        {
            log.Warn(warnMsg);
            return new ReturnModel<T>(warnMsg, httpCode: httpCode);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, Exception ex = null, int httpCode = 200)
        {
            log.Warn(warnMsg);
            return new ReturnModel<T>(warnMsg, ex, httpCode: httpCode);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            log.Warn(warnMsg);
            return new ReturnModel<T>(warnMsg, errorList, httpCode: httpCode);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, string logMsg, Exception ex = null, int httpCode = 200)
        {
            log.Warn(logMsg);
            return new ReturnModel<T>(warnMsg, ex, httpCode: httpCode);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, string logMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            log.Warn(logMsg);
            return new ReturnModel<T>(warnMsg, errorList, httpCode: httpCode);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, int httpCode)
        {
            log.Error(errorMsg);
            return ReturnListModel<T>.Error(errorMsg, httpCode: httpCode);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, Exception ex = null, int httpCode = 200)
        {
            log.Error(ex,errorMsg);
            return ReturnListModel<T>.Error(errorMsg, ex, httpCode: httpCode);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            log.Error(errorMsg);
            return ReturnListModel<T>.Error(errorMsg, errorList, httpCode: httpCode);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, string logMsg, Exception ex = null, int httpCode = 200)
        {
            log.Error(logMsg);
            return ReturnListModel<T>.Error(errorMsg, ex, httpCode: httpCode);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, string logMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            log.Error(logMsg);
            return ReturnListModel<T>.Error(errorMsg, errorList, httpCode: httpCode);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string warnMsg, int httpCode)
        {
            log.Warn(warnMsg);
            return new ReturnListModel<T>(warnMsg, httpCode: httpCode);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string warnMsg, Exception ex = null, int httpCode = 200)
        {
            log.Warn(warnMsg);
            return new ReturnListModel<T>(warnMsg, ex, httpCode: httpCode);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string warnMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            log.Warn(warnMsg);
            return new ReturnListModel<T>(warnMsg, errorList, httpCode: httpCode);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string errorMsg, string logMsg, Exception ex = null, int httpCode = 200)
        {
            log.Warn(logMsg);
            return new ReturnListModel<T>(errorMsg, ex, httpCode: httpCode);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string errorMsg, string logMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
        {
            log.Warn(logMsg);
            return new ReturnListModel<T>(errorMsg, errorList, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> ErrorAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string errorMsg, int httpCode)
            where TSearch : class
        {
            log.Error(errorMsg);
            return ReturnListWithSearchModel<TModel, TSearch>.Error(errorMsg, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> ErrorAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string errorMsg, Exception ex = null, int httpCode = 200)
            where TSearch : class
        {
            log.Error(ex, errorMsg);
            return ReturnListWithSearchModel<TModel, TSearch>.Error(errorMsg, ex, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> ErrorAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string errorMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
            where TSearch : class
        {
            log.Error(errorMsg);
            return ReturnListWithSearchModel<TModel, TSearch>.Error(errorMsg, errorList, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> ErrorAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string errorMsg, string logMsg, Exception ex = null, int httpCode = 200)
            where TSearch : class
        {
            log.Error(logMsg);
            return ReturnListWithSearchModel<TModel, TSearch>.Error(errorMsg, ex, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> ErrorAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string errorMsg, string logMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
            where TSearch : class
        {
            log.Error(logMsg);
            return ReturnListWithSearchModel<TModel, TSearch>.Error(errorMsg, errorList, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, int httpCode)
            where TSearch : class
        {
            log.Warn(warnMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, Exception ex = null, int httpCode = 200)
            where TSearch : class
        {
            log.Warn(ex,warnMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, ex, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
            where TSearch : class
        {
            log.Warn(warnMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, errorList, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, string logMsg, Exception ex = null, int httpCode = 200)
            where TSearch : class
        {
            log.Warn(ex,logMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, ex, httpCode: httpCode);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, string logMsg, List<ReturnErrorItem> errorList, int httpCode = 200)
            where TSearch : class
        {
            log.Warn(logMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, errorList, httpCode: httpCode);
        }
    }
}
