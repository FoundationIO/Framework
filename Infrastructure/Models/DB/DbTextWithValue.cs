/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
namespace Framework.Infrastructure.Models.DB
{
    public class DbTextWithValue : TextWithValue
    {
        public DbTextWithValue()
        {
        }

        public DbTextWithValue(string text, string value)
            : base(text, value)
        {
        }
    }
}
