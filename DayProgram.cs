using System.Collections.Generic;

namespace TimeManagement.Models
{
    public class DayProgram : List<Activity>
    {
        public string Name{ get; set; }
    }
}