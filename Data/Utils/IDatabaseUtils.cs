/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Threading.Tasks;

namespace Framework.Data.Utils
{
    public interface IDatabaseUtils
    {
        bool IsDatabaseNeedToBeRecreated();

        Task<bool> CreateDatabaseAsync();

        Task<bool> DeleteDatabaseAsync();
    }
}