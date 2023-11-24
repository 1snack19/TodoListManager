using System;

namespace TodoListManager
{

    class TextPrompt
    {

        public enum ResultType {
            Confirmed,
            Canceled,
        }

        ResultType _resultType = ResultType.Canceled;

        bool _singleline = false;
        string _text;
        public TextPrompt() { }
        

        public TextPrompt(bool singleline) { 
            _singleline = singleline;
        }

        public void Run()
        {
            Console.Clear();
            if (_singleline)
            {
                Console.WriteLine("-----Enter a text(Single line only. Enter nothing to cancel)-----");
                _text = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("-----Enter a text(Enter nothing to cancel)-----");
                string line;
                while (!String.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    _text += line + "\n";
                }
            }
            if (!String.IsNullOrEmpty(_text)) {
                _resultType = ResultType.Confirmed;
            }
        }

        public string GetResult() { 
            return _text; 
        }

        public ResultType GetResultType() {
            return _resultType;
        }

    }

    
}