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
            Console.WriteLine("login/register");
            Console.WriteLine("Name");
            string name = "test1";
            Console.WriteLine("pass");
            string pass = "test1p";
            loginViewModel.Register(name, pass);
            Console.WriteLine("registered");
            
            Thread.Sleep(5000);
            string id = loginViewModel.Login(name, pass).Result;
            string Id  = id;
            Console.WriteLine(Id);
            Console.WriteLine("Start");
            Thread.Sleep(5000);
            XLSXConverter xLSX = new XLSXConverter(Id);
            Thread.Sleep(5000);
            await xLSX.AddData();
            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}


