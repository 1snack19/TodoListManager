using System;
using System.Globalization;

namespace TodoListManager
{

    class DateTimeSelector : InteractiveMenu
    {

        bool _canceled = false;

        string[] _formats = { Misc.TimeFormat, Misc.DateFormat, Misc.DateFormat + " " + Misc.TimeFormat, Misc.TimeFormat + " " + Misc.DateFormat };

        DateTime _result;
        public DateTimeSelector() {
            _askForInputString = ">>";
            _result = DateTime.MinValue;
        }

        protected override void PrintMenu() {
            Misc.BigHeaderPrint("Enter a datetime(In DD/MM/YYYY (Gregorian Calendar) HH:MM:SS (24 Hours) format. Enter nothing to cancel)");
            Console.WriteLine("* You can leave out date or time and the current date/time will be assumed as default.");
            Console.WriteLine("\nExamples:");
            Console.WriteLine(" 30/10/2023");
            Console.WriteLine(" 14/1/2005 07:21:23");
            Console.WriteLine(" 05:21:03");
        }

        protected override void ProcessInput(string input) {
            if (String.IsNullOrEmpty(input)) {
                _canceled = true;
                PlanExit();
                return;
            }

            DateTime date;

            //Try parsing datetime
            if (DateTime.TryParseExact(input, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
                if (date.TimeOfDay == TimeSpan.Zero) {
                    DateTime now = DateTime.Now;
                    DateTime newdate = new DateTime(date.Year, date.Month, date.Day, now.Hour, now.Minute, now.Second);
                    _result = newdate;
                    PlanExit();
                    return;
                }
                //The current date is already the default value so it's fine
                PlanExit();
                _result = date;
                return;
            } else {
                PlanError("(INVALID DATE)");
                return;
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
