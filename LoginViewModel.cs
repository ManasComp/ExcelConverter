﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
 using ExcelConverter;
 using FoodOrderApp.Model;
using FoodOrderApp.Services;
 namespace TimeManagement.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        private bool Result;

        public async Task Register(string Username1, string Password1)
        {
            var userService = new UserService();
            Result = await userService.RegisterUser(Username1, Password1);
            if (Result)
                Console.WriteLine("OK");
            else
                Console.WriteLine("exists");
        }

        public async Task<string> Login(string Username1, string Password1)
        {
            var userService = new UserService();
            Result = await userService.Login(Username1, Password1);
            if (Result)
            {
                Console.WriteLine("logged");
                Console.WriteLine(userService.user.Id);
               return userService.user.Id;

            }
            else
            {
               Console.WriteLine("error");
               return "";
            }
        }
    }
}
