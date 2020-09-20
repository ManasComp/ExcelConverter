using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using TimeManagement.Models;
using System.Diagnostics;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;

namespace ExcelConverter
{
    class XLSXConverter
    {
        Excel.Application xlApp;
        Excel.Workbook xlWorkbook;
        Excel._Worksheet xlWorksheet;
        Excel.Range xlRange;
        List<DayProgram> activities;
        FireBaseService fireBaseService;

        private void CellParse(int x, int y, int activityId)
        {
            Console.WriteLine("CellParse");
            if (xlRange.Cells[x, y].Value != null && xlRange.Cells[x, y + 2].Value != null && xlRange.Cells[x, y + 3].Value != null)
            {
                double StartHelper;
                double EndHelper;
                activities[activityId].Add(new Activity
                {
                    Start = TimeSpan.FromDays(double.Parse(xlRange.Cells[x, y].Value.ToString())),
                    End = TimeSpan.FromDays(double.Parse(xlRange.Cells[x, y+2].Value.ToString())),
                    Name = xlRange.Cells[x, y+3].Value.ToString()
                });
                Console.WriteLine(activities[activityId][activities[activityId].Count - 1].Name);
            }
        }
        private void Table(int i)
        {
            Console.WriteLine("Table");
            for (int j = 0; j < 7; j++)
            {
                CellParse(i, 1 + j * 6, j);
            }
        }

        private void Settings()
        {
            Console.WriteLine("Settings");
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\l20170133\Desktop\TimeTable.xlsx");
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;
            activities = new List<DayProgram>();

            activities.Add(new DayProgram { Name = "Monday", Id=0 });
            activities.Add(new DayProgram { Name = "Tuesday", Id=1 });
            activities.Add(new DayProgram { Name = "Wednesday", Id=2 });
            activities.Add(new DayProgram { Name = "Thursday", Id=3 });
            activities.Add(new DayProgram { Name = "Friday", Id=4 });
            activities.Add(new DayProgram { Name = "Saturday", Id=5 });
            activities.Add(new DayProgram { Name = "Sunday", Id=6 });
        }

        private void Cleaning()
        {
            Console.WriteLine("Cleaning");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private void Loading()
        {
            Console.WriteLine("Loading");
            Settings();
            for (int i = 2; i < xlRange.Rows.Count; i++)
            {
                Table(i);
            }
            Cleaning();
        }

        private void Write()
        {
            Console.WriteLine("Write");
            foreach (var activity in activities)
            {
                Console.WriteLine(activity.Name);
                foreach (var items in activity)
                {
                    Console.WriteLine(items.Start + "-" + items.End + "-" + items.Name);
                }
            }
        }

        public async Task AddData()
        {
            Console.WriteLine("AddData");
            Loading();
            fireBaseService = new FireBaseService();
            //Write();

            // await fireBaseService.PostAsync<int>("program", 1);
            //await fireBaseService.PostAsyncList<int>("program", new List<int>(){1,2});
            // for (int i=0;i<7; i++)
            // {
            //     //if (activities[i]!=null)
            //         //await fireBaseService.PostAsyncList<Activity>(activities[i].Name, activities[i]);
            // }
            string _url = "https://timemanegment-74160.firebaseio.com/";
            FirebaseClient _firebaseClient = new FirebaseClient(_url);


             _firebaseClient.Child("DayPrograms").PostAsync(activities);


            // foreach (var day in activities)
            // {
            //     _firebaseClient.Child(day.Name).PostAsync(day);
            // }

            Console.WriteLine("**************************************************************************************************End");
            Console.ReadKey();
            Console.ReadLine();
        }
    }
}
