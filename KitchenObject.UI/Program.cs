using System.Drawing;
using KitchenObject.BL;

namespace KitchenObject.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lockObject = new Object();
            var consoleControler = new ConsoleControler(lockObject);


            var cup = new Cup(Color.Black);
            var kettle = new Kettle(Color.White);
            var electricKettle = new ElectricKettle(Color.Red);
            var multicooker = new Multicooker(Color.Blue);
            var allKitchenObjects = CreateAllKitchenObjectList(cup, kettle, electricKettle, multicooker);
            foreach (var kitchenObject in allKitchenObjects)
            {
                kitchenObject.ConsoleControler = consoleControler;
            }


            WriteCommand(allKitchenObjects);
            var objectsWithVolumes = allKitchenObjects.Where(x => x is IObjectWithVolume).Select(x => (IObjectWithVolume)x).ToList();
            var temperatureControler = new TemperatureControler(lockObject, objectsWithVolumes);
            var statusControler = new StatusControler(lockObject, objectsWithVolumes);
            var tasks = new Task[]
            {
                new Task(() => { temperatureControler.ControlTemperature(); }),
                new Task(() => { statusControler.WriteStatus(); })
            };
            foreach (var task in tasks)
            {
                task.Start();
            }


            var endCycle = false;
            while (!endCycle)
            {
                var key = Console.ReadKey();
                lock (allKitchenObjects)
                {
                    consoleControler.ClearCurrentConsoleLine();
                    if (Int32.TryParse(key.KeyChar.ToString(), out int command))
                    {
                        if (command == 1 || command == 2 || command == 3 || command == 4)
                        {
                            allKitchenObjects[command - 1].Use();
                        }
                        else if (command == 5)
                        {
                            cup.PourFromTheKettle(kettle);
                        }
                        else if (command == 6)
                        {
                            cup.PourFromTheKettle(electricKettle);
                        }
                        else if (command == 7)
                        {
                            multicooker.Clean();
                        }
                        else if (command == 9)
                        {
                            endCycle = true;
                        }
                    }
                }
            }


            temperatureControler.EndControling = true;
            statusControler.EndControling = true;
            Task.WaitAll(tasks);
        }


        public static void WriteCommand(List<BL.KitchenObject> allKitchenObjects)
        {
            Console.WriteLine($"{new string("Status kitchen object").PadRight(20)}:  {new string("V").PadRight(4)}  {new string("t").PadRight(3)}  Vmax  Color");
            foreach (var kitchenObject in allKitchenObjects)
            {
                var objectWithVolume = (IObjectWithVolume)kitchenObject;
                Console.WriteLine($"{new string("").PadRight(33)}  {objectWithVolume.Volume.ToString().PadRight(4)}  {kitchenObject.Color.Name}");
            }
            Console.WriteLine();
            Console.WriteLine("Command list:");
            Console.WriteLine("1 - use cup          2 - boil kettle          3 - boil electric kettle");
            Console.WriteLine("4 - use multicook    5 - pour from kettle     6 - pour from electric kettle");
            Console.WriteLine("7 - clean multicook  9 - exit");
            Console.WriteLine();
            Console.WriteLine("Command status:");
        }
        public static List<BL.KitchenObject> CreateAllKitchenObjectList(params BL.KitchenObject[] kitchenObjects)
        {
            var allKitchenObjects = new List<BL.KitchenObject>();
            foreach (var kitchenObject in kitchenObjects)
            {
                allKitchenObjects.Add(kitchenObject);
            }
            return allKitchenObjects;
        }
    }
}
