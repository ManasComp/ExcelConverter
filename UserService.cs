using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using TimeManagement.Models;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ExcelConverter;
using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Model;

namespace FoodOrderApp.Services
{
    class UserService
    {
        private FirebaseService _firebaseService;
        public User user;
        public async Task<bool> RegisterUser(string uname, string passwd)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = uname,
                Password = passwd
            };
            string _url = "https://timemanegment-74160.firebaseio.com/";
            FirebaseClient firebaseClient = new FirebaseClient(_url);
            //firebaseClient.Child("Users1").DeleteAsync();
            firebaseClient.Child("Users1").PostAsync(user);
            Console.WriteLine("ready");
            return true;
        }
        public async Task<bool> Login(string uname, string passwd)
        {
            string _url = "https://timemanegment-74160.firebaseio.com/";
            FirebaseClient _firebaseClient = new FirebaseClient(_url);
            var mrdka = await _firebaseClient.Child("Users1").OnceAsync<User>();
            user = mrdka.Select(e => e.Object as User).ToList()
                .Where(u => u.Username == uname)
                .FirstOrDefault(u => u.Password == passwd);
            return (user != null);
        }
    }
}
