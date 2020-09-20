using System.Collections.Generic;

namespace TimeManagement.Models
{
    public class DayProgram : List<Activity>
    {
        public string Name{ get; set; }
        public int Id { get; set; }
    }
}