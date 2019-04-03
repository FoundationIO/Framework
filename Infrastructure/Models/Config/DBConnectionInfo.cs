using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Models.Config
{
    public class DbConnectionInfo
    {
        public string Name { get; set; }

        public string DatabaseType { get; set; }

        public string DatabaseName { get; set; }

        public string DatabaseServer { get; set; }

        public string DatabaseUserName { get; set; }

        public string DatabasePassword { get; set; }

        public int DatabaseCommandTimeout { get; set; }

        public int MaxPoolSize { get; set; }

        public bool DatabaseUseIntegratedLogin { get; set; }

        public string AdditionalParameters { get; set; }

        public string MigrationProfile { get; set; }

        public string MigrationNamespace { get; set; }
    }
}
