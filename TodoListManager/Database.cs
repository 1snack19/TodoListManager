using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoListManager
{
    class Database
    {
        public static Database Instance { get; private set; }

        public static void Init()
        {
            if (Instance != null)
                throw new Exception("Database is already initialized!");
            
            Instance = new Database();
        }

        List<Reminder> _reminders = new List<Reminder>();

        Database() { }


        public List<Reminder> GetReminders() 
        { 
            return _reminders; 
        }

        public int Count() 
        { 
            return _reminders.Count; 
        }

        public void RemoveAt(int index)
        {
            _reminders.RemoveAt(index);
        }

        public void InsertAt(int index, Reminder item)
        {
            _reminders.Insert(index, item);
        }

        public void Add(Reminder item)
        {
            _reminders.Add(item);
        }

        public void ReplaceAt(int index, Reminder item) {
            _reminders[index] = item;
        }


        public void Load()
        {
            Console.Clear();
            Console.WriteLine("Reading file");
            if (!File.Exists(Misc.SAVE_FILE_NAME)) {
                Console.WriteLine("Database file does not exist. Creating a new one.");
                File.WriteAllText(Misc.SAVE_FILE_NAME, "");
                return;
            }
            string readSerial = File.ReadAllText(Misc.SAVE_FILE_NAME);
            Console.WriteLine("Deserializing");
            List<Reminder> serialized = JsonConvert.DeserializeObject<List<Reminder>>(readSerial);
            if (serialized != null) {
                _reminders = serialized;
            }

        }

        public void Save() 
        {
            Console.Clear();
            Console.WriteLine("Saving...");
            string serial = JsonConvert.SerializeObject(_reminders);
            File.WriteAllText(Misc.SAVE_FILE_NAME, serial);
        }


    }
}
