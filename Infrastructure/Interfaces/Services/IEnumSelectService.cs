/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using Framework.Infrastructure.Models;

namespace Framework.Infrastructure.Interfaces.Services
{
    public interface IEnumSelectService
    {
        List<TextWithValue> GetSelectList<T>()
            where T : Enum;

        List<TextWithValue> GetSelectListWithAll<T>()
            where T : Enum;
    }
}
