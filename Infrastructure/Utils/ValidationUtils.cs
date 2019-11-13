/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.ComponentModel.DataAnnotations;

namespace Framework.Infrastructure.Utils
{
    public static class ValidationUtils
    {
        public static bool IsEmail(string str)
        {
            return new EmailAddressAttribute().IsValid(str);
        }
    }
}
