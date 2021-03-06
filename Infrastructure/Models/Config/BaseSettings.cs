﻿/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Linq;
using Framework.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;

namespace Framework.Infrastructure.Models.Config
{
    public class BaseSettings
    {
        public BaseSettings(IConfiguration configuration, Func<string, string> configUpdator = null)
        {
            if (configuration == null)
            {
                return;
            }

            var propList = this.GetType().GetProperties();

            var valueList = configuration.GetChildren().AsEnumerable().ToList();

            foreach (var prop in propList)
            {
                var value = valueList.FirstOrDefault(x => x.Key.ToLower().Trim() == prop.Name.ToLower().Trim());
                if (value == null)
                {
                    continue;
                }

                if (value.Key.IsTrimmedStringNullOrEmpty() || value.Value.IsTrimmedStringNullOrEmpty())
                {
                    continue;
                }

                if (configUpdator != null)
                {
                    value.Value = configUpdator(value.Value);
                }

                ReflectionUtils.SetPropertyValueFromString(this, prop, value.Value, null);
            }
        }
    }
}
