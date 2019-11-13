/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using Framework.Infrastructure.Logging;
using Framework.Infrastructure.Models.Result;

namespace Framework.Infrastructure.BaseClasses
{
    public abstract class BaseService
    {
        protected ILog log;

        protected BaseService(ILog log)
        {
            this.log = log;
        }

        public ReturnListModel<T> ReturnList<T>(string defaultErrorMsg, Func<string,ReturnListModel<T>> func)
        {
            return ReturnList(func, defaultErrorMsg);
        }

        public ReturnListModel<T> ReturnList<T>(Func<string,ReturnListModel<T>> func, string defaultErrorMsg = null)
        {
            try
            {
                return func(defaultErrorMsg);
            }
            catch (Exception ex)
            {
                log.Error(ex, defaultErrorMsg);
                return ReturnListModel<T>.Error(defaultErrorMsg, ex);
            }
        }

        public ReturnModel<T> ReturnItem<T>(string defaultErrorMsg, Func<string,ReturnModel<T>> func)
        {
            return ReturnItem(func, defaultErrorMsg);
        }

        public ReturnModel<T> ReturnItem<T>(Func<string, ReturnModel<T>> func, string defaultErrorMsg = null)
        {
            try
            {
                return func(defaultErrorMsg);
            }
            catch (Exception ex)
            {
                log.Error(ex, defaultErrorMsg);
                return ReturnModel<T>.Error(defaultErrorMsg, ex);
            }
        }
    }
}
