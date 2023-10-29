using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{

    class DateTimeSelector
    {
        static public string dateFormat { get; private set; }
        static public string timeFormat { get; private set; }

        static public void SetDateFormat(string value)
        {
            dateFormat = value;
        }

        static public void SetTimeFormat(string value)
        {
            timeFormat = value;
        }

        bool _madeError = false;

        bool _canceled = false;

        DateTime _result;
        public DateTimeSelector() {
            _result = DateTime.MinValue;
        }

        public void Run()
        {
            
            string[] formats = { timeFormat, dateFormat, dateFormat + " " + timeFormat };

            while (true)//Get datetime
            {
                Console.Clear();
                Console.WriteLine("-------Enter a datetime(In DD/MM/YYYY HH:MM:SS format. Enter nothing to cancel)-------");
                Console.WriteLine("* You can leave out date or time and the current date/time will be assumed as default.");
                if (_madeError)
                {
                    Console.WriteLine("(INVALID INPUT)");
                    _madeError = false;
                }
                string input = Console.ReadLine().Trim();
                if (String.IsNullOrEmpty(input)) { 
                    _canceled = true;
                    break; 
                }

                DateTime date;
                //USE THE METHOD ToLocalTime() WHEN YOU WANT TO DISPLAY INFORMATIONS ONLY. DO NOT USE IT IN ANY OTHER CIRCUMSTANCES. IT MAKES THINGS CONFUSING
                if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    if (date.TimeOfDay == TimeSpan.Zero)
                    {
                        DateTime now = DateTime.Now;
                        DateTime newdate = new DateTime(date.Year, date.Month, date.Day, now.Hour, now.Minute, now.Second);
                        _result = newdate;
                        break;
                    }
                    //The current date is already the default value so it's fine
                    _result = date;
                    break;
                } 
                else
                {
                    _madeError = true;
                }
               
            }

        }

        public bool IsCanceled()
        {
            return _canceled;
        }

        public DateTime GetResult()
        {
            return _result;
        }

    }
}
