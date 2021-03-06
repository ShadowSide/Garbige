﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Global.BusinessCommon.Helpers.DB.DbSetExtensions
{
    public static class TableNameOfDbSetExtension
    {
        public static string GetTableName<DBContext, T>(this DBContext context, Func<DBContext, IDbSet<T>> dbSet) 
            where T : class
            where DBContext : DbContext
        {
            return context.GetTableName<T>();
        }

        public static string GetTableName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetTableName<T>();
        }

        public static string GetTableName<T>(this ObjectContext context) where T : class
        {
            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);

            string table = match.Groups["table"].Value;
            return table;
        }
    }
}
