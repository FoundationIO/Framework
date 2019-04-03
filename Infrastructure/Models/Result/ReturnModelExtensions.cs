using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure.Exceptions;
using Framework.Infrastructure.Logging;

namespace Framework.Infrastructure.Models.Result
{
    public static class ReturnModelExtensions
    {
        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, Exception ex = null)
        {
            log.Error(errorMsg);
            return new ReturnModel<T>(errorMsg, ex);
        }

        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, List<ErrorItem> errorList)
        {
            log.Error(errorMsg);
            return new ReturnModel<T>(errorMsg, errorList);
        }

        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, string logMsg, Exception ex = null)
        {
            log.Error(logMsg);
            return new ReturnModel<T>(errorMsg, ex);
        }

        public static ReturnModel<T> ErrorAndSendReturnModel<T>(this ILog log, string errorMsg, string logMsg, List<ErrorItem> errorList)
        {
            log.Error(logMsg);
            return new ReturnModel<T>(errorMsg, errorList);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, Exception ex = null)
        {
            log.Warn(warnMsg);
            return new ReturnModel<T>(warnMsg, ex);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, List<ErrorItem> errorList)
        {
            log.Warn(warnMsg);
            return new ReturnModel<T>(warnMsg, errorList);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, string logMsg, Exception ex = null)
        {
            log.Warn(logMsg);
            return new ReturnModel<T>(warnMsg, ex);
        }

        public static ReturnModel<T> WarnAndSendReturnModel<T>(this ILog log, string warnMsg, string logMsg, List<ErrorItem> errorList)
        {
            log.Warn(logMsg);
            return new ReturnModel<T>(warnMsg, errorList);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, Exception ex = null)
        {
            log.Error(errorMsg);
            return new ReturnListModel<T>(errorMsg, ex);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, List<ErrorItem> errorList)
        {
            log.Error(errorMsg);
            return new ReturnListModel<T>(errorMsg, errorList);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, string logMsg, Exception ex = null)
        {
            log.Error(logMsg);
            return new ReturnListModel<T>(errorMsg, ex);
        }

        public static ReturnListModel<T> ErrorAndSendReturnListModel<T>(this ILog log, string errorMsg, string logMsg, List<ErrorItem> errorList)
        {
            log.Error(logMsg);
            return new ReturnListModel<T>(errorMsg, errorList);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string warnMsg, Exception ex = null)
        {
            log.Warn(warnMsg);
            return new ReturnListModel<T>(warnMsg, ex);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string warnMsg, List<ErrorItem> errorList)
        {
            log.Warn(warnMsg);
            return new ReturnListModel<T>(warnMsg, errorList);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string errorMsg, string logMsg, Exception ex = null)
        {
            log.Warn(logMsg);
            return new ReturnListModel<T>(errorMsg, ex);
        }

        public static ReturnListModel<T> WarnAndSendReturnListModel<T>(this ILog log, string errorMsg, string logMsg, List<ErrorItem> errorList)
        {
            log.Warn(logMsg);
            return new ReturnListModel<T>(errorMsg, errorList);
        }

        public static ReturnListWithSearchModel<TModel,TSearch> ErrorAndSendReturnListWithSearchModel<TModel,TSearch>(this ILog log, string errorMsg, Exception ex = null)
            where TSearch : class
        {
            log.Error(errorMsg);
            return new ReturnListWithSearchModel<TModel,TSearch>(errorMsg, ex);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> ErrorAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string errorMsg, List<ErrorItem> errorList)
            where TSearch : class
        {
            log.Error(errorMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, errorList);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> ErrorAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string errorMsg, string logMsg, Exception ex = null)
            where TSearch : class
        {
            log.Error(logMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, ex);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> ErrorAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string errorMsg, string logMsg, List<ErrorItem> errorList)
            where TSearch : class
        {
            log.Error(logMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(errorMsg, errorList);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, Exception ex = null)
            where TSearch : class
        {
            log.Warn(warnMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, ex);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, List<ErrorItem> errorList)
            where TSearch : class
        {
            log.Warn(warnMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, errorList);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, string logMsg, Exception ex = null)
            where TSearch : class
        {
            log.Warn(logMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, ex);
        }

        public static ReturnListWithSearchModel<TModel, TSearch> WarnAndSendReturnListWithSearchModel<TModel, TSearch>(this ILog log, string warnMsg, string logMsg, List<ErrorItem> errorList)
            where TSearch : class
        {
            log.Warn(logMsg);
            return new ReturnListWithSearchModel<TModel, TSearch>(warnMsg, errorList);
        }
    }
}
