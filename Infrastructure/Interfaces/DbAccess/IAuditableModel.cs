/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;

namespace Framework.Infrastructure.Interfaces.DbAccess
{
    public interface IAuditableModel
    {
        DateTime CreatedDate { get; set; }

        string CreatedBy { get; set; }

        DateTime ModifiedDate { get; set; }

        string ModifiedBy { get; set; }
    }
}
