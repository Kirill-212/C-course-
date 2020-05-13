using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.ViewModels.timetable
{
   public class TimeTableR
    {
        public string day_of_the_week { get; set; }
        public int week { get; set; }
        public string time_and_sub8 { get; set; }
        public string time_and_sub9 { get; set; }
        public string time_and_sub11 { get; set; }
        public string time_and_sub13 { get; set; }
        public string time_and_sub15 { get; set; }
        public string time_and_sub17 { get; set; }
        public string time_and_sub19 { get; set; }
        public TimeTableR()
        {
            this.time_and_sub8 = "8.00-9.35  ->";
            this.time_and_sub9 = "9.50-11.25  ->";
            this.time_and_sub11 = "11.40-13.15  ->";
            this.time_and_sub13 = "13.50-15.25  ->";
            this.time_and_sub15 = "15.40-17.15  ->";
            this.time_and_sub17 = "17.30-19.05  ->";
            this.time_and_sub19 = "19.20-20.55  ->";
        }
    }
}
