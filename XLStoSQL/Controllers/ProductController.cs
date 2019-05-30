using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using XLStoSQL.Models;
using System.Data.SqlClient;

namespace XLStoSQL.Controllers
{
    public class ProductController : Controller
    {
        private static FinanceInfoDBEntities db = new FinanceInfoDBEntities();
        protected static int id = 1;
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

 
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase postedFile)
        {

            if (postedFile == null || postedFile.ContentLength == 0)
            {
                ViewBag.Error = "Please, choose excel file";
                return View("Index");
            }
            else
            {
                if (postedFile.FileName.EndsWith("xls") || postedFile.FileName.EndsWith("csv") || postedFile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                    postedFile.InputStream.Close();
                    // Read all data from excel file

                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path + Path.GetFileName(postedFile.FileName));
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;

                    int row = 2;

                    //db.Database.ExecuteSqlCommand("DELETE FROM Finance_DB");

                    var sql = "SELECT COUNT(*) FROM Finance_DB";
                    var total = db.Database.SqlQuery<int>(sql).First();

                    // Return last value of a column 
                    if (total > 0)
                    {
                        var v = db.Finance_DB.OrderByDescending(t => t.ID).First();
                        if (v != null)
                        {
                            id = v.ID + 1;
                        }

                    }
                     
                 
                    string _trans;
                    string _type;
                    string _date;
                    string _name;
                    string _memo;
                    string _account;
                    string _debit;
                    string _credit;

                    while (row < range.Rows.Count)
                    {
                         _trans = ((Excel.Range)range.Cells[row, 2]).Text;
                         _type = ((Excel.Range)range.Cells[row, 4]).Text;
                         _date = ((Excel.Range)range.Cells[row, 6]).Text;
                         _name = ((Excel.Range)range.Cells[row, 10]).Text;
                         _memo = ((Excel.Range)range.Cells[row, 12]).Text;
                         _account = ((Excel.Range)range.Cells[row, 14]).Text;
                         _debit = ((Excel.Range)range.Cells[row, 16]).Text;
                         _credit = ((Excel.Range)range.Cells[row, 18]).Text;
                        if (_debit == _credit && _name == "")
                        {
                            // It means we`ve got a Total 
                            // Last row in the record 
                            row++;
                            continue;
                        }
                       // _trans = _trans.Replace(",", ".");
                        Finance_DB finance_DB = new Finance_DB() {
                            Trans = Convert.ToDouble(_trans),
                            Type = _type,
                            Date = _date,
                            Name = _name,
                            Memo = _memo,
                            Account = _account,
                            Debit = Convert.ToDouble(_debit),
                            Credit = Convert.ToDouble(_credit),
                            ID = ++id
                        };
                        db.Finance_DB.Add(finance_DB);
                        //db.SaveChanges();                                
                        // Move to the next row
                        row++;
                        while (row < range.Rows.Count && ((Excel.Range)range.Cells[row, 2]).Text == "")
                        {
                             _type = ((Excel.Range)range.Cells[row, 4]).Text;
                             _date = ((Excel.Range)range.Cells[row, 6]).Text;
                             _name = ((Excel.Range)range.Cells[row, 10]).Text;
                             _memo = ((Excel.Range)range.Cells[row, 12]).Text;
                             _account = ((Excel.Range)range.Cells[row, 14]).Text;
                             _debit = ((Excel.Range)range.Cells[row, 16]).Text;
                             _credit = ((Excel.Range)range.Cells[row, 18]).Text;
                            if (_debit.Equals(_credit) && _name == "")
                            {
                                // It means we`ve got a Total 
                                // Last row in the record 
                                row++;
                                break;
                            }
                            finance_DB = new Finance_DB()
                            {
                                Trans = Convert.ToDouble(_trans),
                                Type = _type,
                                Date = _date,
                                Name = _name,
                                Memo = _memo,
                                Account = _account,
                                Debit = Convert.ToDouble(_debit),
                                Credit = Convert.ToDouble(_credit),
                                ID = ++id
                            };
                            db.Finance_DB.Add(finance_DB);
                            db.SaveChanges();
                            // Move to the next row
                            row++;
                        }
                    }
                    workbook.Close();
                    ViewBag.Message = "File uploaded successfully.";
                    return View("Success");
                }
                else
                {
                    ViewBag.Error = "Please, choose excel file";
                    return View("Index");
                }

            }


        }
    }
}