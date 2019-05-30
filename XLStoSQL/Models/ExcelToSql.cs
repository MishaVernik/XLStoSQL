using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System.Data.Entity;

namespace XLStoSQL.Models
{
    public class FinanceData
    {
        public int _Trans { get; set; }
        public string _Type { get; set; }
        public DateTime _Date { get; set; }
        public string _Name { get; set; }
        public string _Memo { get; set; }
        public string _Account { get; set; }
        public double _Debit { get; set; }
        public double  _Credit { get; set; }
    }
    public class ExcelToSql : DbContext
    {
        public ExcelToSql() : base("DefaultConnection")
        { }
        public DbSet<FinanceData> financeData { get; set; }
    }
}