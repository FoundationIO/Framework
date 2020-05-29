/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Models
{
    [Serializable]
    public class TextWithValue
    {
        public TextWithValue()
        {
        }

        public TextWithValue(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }

        public string Value { get; set; }
    }
}
