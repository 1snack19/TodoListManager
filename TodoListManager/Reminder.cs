using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager {
    class Reminder : ICloneable {
        public Reminder() {
            note = "Empty";
            title = "Untitled";
            datetime = DateTime.Now;
        }

        public override string ToString() {
            return title + "   |   " + datetime.ToString(Misc.DateFormat + " " + Misc.TimeFormat);
        }

        public object Clone() {
            Reminder nr = new Reminder();
            nr.note = note;
            nr.title = title;
            nr.datetime = datetime;
            return nr;

        }

        public string note;
        public string title;
        public DateTime datetime;
    }
}
