using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager {
    class TestMenu : InteractiveMenu {

        double _a = .0;
        double _b = .0;

        protected override void PrintMenu() {
            Misc.HeaderPrint("Test Header");
            Console.WriteLine("-c = Do something");
            Console.WriteLine("-a N = Set A to N");
            Console.WriteLine("-b N = Set B to N");
            Console.WriteLine("-b = Exit");
        }

        protected override void ProcessInput(string m) {
            if (String.IsNullOrEmpty(m)) return;

            string[] args = m.Split(' ');
            switch (args.Length) {
                case 1://-[Option]
                    switch (args[0]) {
                        case "-c":
                            Console.Clear();
                            Console.WriteLine("The result is " + (_a + _b));
                            Console.Write("Press enter to return\n");
                            Console.ReadLine();
                            break;
                        case "-b":
                            PlanExit();
                            return;
                        default:
                            PlanError("(INVALID OPTION)");
                            return;
                    }
                    break;
                case 2://-[Option] [Argument 1]
                    double numberIn;
                    bool parseNumber = Double.TryParse(args[1], out numberIn);

                    if (!parseNumber) {
                        PlanError("(INVALID VALUE)");
                        return;
                    }

                    switch (args[0]) {
                        case "-a":
                            _a = numberIn;
                            break;
                        case "-b":
                            _b = numberIn;
                            break;
                        default:
                            PlanError("(INVALID OPTION)");
                            return;
                    }
                    break;
                default:
                    PlanError("(INVALID OPTION)");
                    break;
            }
            
        }
    }
}
