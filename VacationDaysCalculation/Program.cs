using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationDaysCalculation
{

    public enum Days
    {
        Monday = 1,  
        Tuesday = 2,
        Wednesday =3, 
        Thursday =4,
        Friday =5,
        Saturday =6,
        Sunday =7,
        Weekend =8,
    }

    public enum Months
    {
        January = 1       ,
        February = 2      ,
        March = 3         ,
        April = 4         ,
        May = 5           ,
        June = 6          ,
        July = 7          ,
        August = 8        ,
        September = 9     ,
        October = 10      ,
        November = 11     ,
        December = 12     ,
    }

    class Program
    {
        static void Main(string[] args)
        {
            solution(2014, "January", "January", "Wednesday");
            solution(2014, "January", "February", "Tuesday");
            solution(2014, "April","May", "Wednesday");  // expect 7
            //solution(2150, "April", "May", "Friday");  // exception
            //solution(2050, "April", "January", "Tuesday");  // exception


            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        public static bool IsLeapYear(int y)
        {
            return y % 4 == 0;
        }

        public static int solution(int Y, string A, string B, string W)
        {
            var firstDayOfYear = (int) Enum.Parse(typeof(Days), W);
            var firstMonth = (int)Enum.Parse(typeof(Months), A);
            var lastMonth = (int)Enum.Parse(typeof(Months), B);

            ValidateInputValues(Y, firstMonth, lastMonth);

            var firstSundayN = getFirstSunday(Y, firstMonth, firstDayOfYear);
            var lastSundayN = getLastSunday(Y, lastMonth, firstDayOfYear);

            var vacationLengthDays = lastSundayN - firstSundayN;
            int vacationLengthWeeks = (vacationLengthDays) / 7;

            Console.WriteLine(Y + " " + A + " " + B + " " + W + "; vacation length in weeks: " + vacationLengthWeeks);

            return vacationLengthWeeks;
        }

        private static void ValidateInputValues(int y, int firstMonth, int lastMonth)
        {
            if (y<2001 || y>2099)
                throw new ArgumentOutOfRangeException("year", "should be 2001-2099"); // actually it means we can use 2 digit year..

            if (lastMonth < firstMonth)
                throw new Exception("first month should be less than last");  // hmm... why can't be December - January over New Year???
        }

        private static int getFirstSunday(int y, int a, int w) 
        {
            var isLeapYear = IsLeapYear(y);

            var days = 0;

            for (int i = 1; i < a; i++)
            {
                if (i == 2)
                {
                    if (isLeapYear)
                        days += 29;
                    else
                        days += 28;
                }
                else if (i % 2 == 0)
                    days += 30;
                else days += 31;
            }


            for (int i = 1; i <= 7; i++)
            {
                days += 1;
                if ((days + w) % 7 == 0)
                    break;
            }

            return days;
        }

        private static int getLastSunday(int y, int b, int w)
        {
            // we may add logic for vacation over new year (Dec-Jan) later..

            var isLeapYear = IsLeapYear(y);

            var days = 0;

            for (int i = 1; i <= b; i++)
            {
                if (i == 2)
                {
                    if (isLeapYear)
                        days += 29;
                    else
                        days += 28;
                }
                else if (i % 2 == 0)
                    days += 30;
                else days += 31;
            }


            for (int i = 0; i <= 6; i++)
            {
                days -= 1;
                if ((days + w) % 7 == 0)
                    break;
            }

            return days;
        }

    }
}
