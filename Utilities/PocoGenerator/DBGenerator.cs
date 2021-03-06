﻿/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using Framework.Utilities.PocoGenerator.Utilities;
using RazorLight;

namespace Framework.Utilities.PocoGenerator
{
    public class DbGenerator
    {
        private readonly Config config;

        public DbGenerator(Config config)
        {
            this.config = config;
        }

        public DatabaseSchema GetSchema()
        {
            DatabaseSchema schema;

#if NET461
            using (var dbReader = new DatabaseReader(config.ConnectionString, config.ServerType))
#else
            using (var dbReader = new DatabaseReader(config.GetConnection()))
#endif
            {
                schema = dbReader.ReadAll();
            }

            // Fix all the names of the Views and Tables then proceed to check for duplicates
            foreach (var tbl in schema.Tables)
            {
                tbl.NetName = GeneratorUtils.MakeClassName(tbl.Name);
                FixColumns(tbl.Columns);
            }

            foreach (var view in schema.Views)
            {
                view.NetName = GeneratorUtils.MakeClassName(view.Name);
                FixColumns(view.Columns);
            }

            // Now look for duplicates and correct the variable names
            foreach (var tbl in schema.Tables)
            {
                FixTableAndViewName(tbl.NetName, schema.Tables, schema.Views);
            }

            foreach (var vw in schema.Views)
            {
                FixTableAndViewName(vw.NetName, schema.Tables, schema.Views);
            }

            foreach (var tbl in schema.Tables)
            {
                if (!config.IgnoredTableNames.Contains(tbl.Name.Trim().ToUpper()))
                    continue;

                tbl.Name = "";
                tbl.NetName = "";
            }

            foreach (var vw in schema.Views)
            {
                if (!config.IgnoredTableNames.Contains(vw.Name.Trim().ToUpper()))
                    continue;

                vw.Name = "";
                vw.NetName = "";
            }

            return schema;
        }

        public void FixTableAndViewName(string tableName, List<DatabaseTable> existingTables, List<DatabaseView> existingViews)
        {
            var tables = existingTables.Where(x => x.NetName == tableName).ToList();
            var views = existingViews.Where(x => x.NetName == tableName).ToList();

            if ((tables.Count + views.Count) > 1)
            {
                int idx = 0;
                foreach (var tbl in tables)
                {
                    if (tbl.NetName == tableName)
                    {
                        if (idx == 0)
                        {
                            idx++;
                            continue;
                        }
                    }

                    idx++;
                    tbl.NetName = $"{tbl.NetName}_{idx}";
                }

                foreach (var view in views)
                {
                    if (view.NetName == tableName)
                    {
                        if (idx == 0)
                        {
                            idx++;
                            continue;
                        }
                    }

                    idx++;
                    view.NetName = $"{view.NetName}_{idx}";
                }
            }
        }

        public void FixColumns(List<DatabaseColumn> columns)
        {
            foreach (var col in columns)
            {
                col.NetName = GeneratorUtils.MakePropertyName(col.Name);
                if (col.Table.NetName == col.NetName)
                {
                    col.NetName = $"{col.NetName}2";
                }
            }

            foreach (var col in columns)
            {
                var cols = columns.Where(x => x.NetName == col.NetName).ToList();

                if (cols.Count > 1)
                {
                    int idx = 0;
                    foreach (var newCol in cols)
                    {
                        if (idx == 0)
                        {
                            idx++;
                            continue;
                        }

                        idx++;
                        newCol.NetName = "{newCol.NetName}_{idx}";
                    }
                }
            }
        }

        public void LoadAndGenerate()
        {
            var schema = GetSchema();
            var model = new TemplateData() { DatabaseSchema = schema, Config = config };

            foreach (var item in config.InputOutputFiles)
            {
                var engine = new RazorLightEngineBuilder()
                                .UseFileSystemProject(Path.GetDirectoryName(item.TemplateFile),Path.GetExtension(item.TemplateFile))
                                .UseMemoryCachingProvider()
                                .Build();
                string result = engine.CompileRenderAsync(Path.GetFileName(item.TemplateFile), model).Result;
                File.WriteAllText(item.CodeFile, result);
            }
        }
    }
}
