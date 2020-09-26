using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeManagement.ViewModels;


namespace ExcelConverter
{
    class Program
    {
        public static void Main(string[] args)
        {
            mrdk();
        }

        public static async void mrdk()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            Console.WriteLine("Name");
            string name = Console.ReadLine();
            Console.WriteLine("pass");
            string pass = Console.ReadLine();
            
            string id = loginViewModel.LogOrReg(name, pass).Result;
            
            Console.WriteLine(id);
            Console.WriteLine("Start");
            Thread.Sleep(5000);
            XLSXConverter xLSX = new XLSXConverter(id);
            Thread.Sleep(5000);
            await xLSX.AddData();
            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}


