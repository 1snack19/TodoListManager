using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager {
    /*
     * Implemented menu will look like this:
     * 
     * [Your print menu]
     * [Error(s) will be printed here]
     * Enter an option : (This can be customized) [text input]
     * 
     */

    abstract class InteractiveMenu {
        private bool  _madeError = false;
        private string _errorMessage = "";
        private bool _running = true;

        protected string _askForInputString = "Enter an option : ";

        protected virtual void PlanError(string m) { 
            _madeError = true;
            _errorMessage = m;
        }

        protected virtual void PlanExit() {
            _running = false;
        }

        protected virtual void TryPrintError() {
            if (_madeError) {
                Console.WriteLine(_errorMessage);
                _errorMessage = "";
                _madeError = false;
            }
        }

        protected abstract void PrintMenu();
        protected abstract void ProcessInput(string input);

        public virtual void Run() {
            while (_running) {
                Console.Clear();
                PrintMenu();
                TryPrintError();
                Console.Write(_askForInputString);
                string input = Console.ReadLine().Trim();
                ProcessInput(input);
            }
        }

    }
}
