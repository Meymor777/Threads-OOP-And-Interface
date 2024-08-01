namespace KitchenObject.BL
{
    public class ConsoleControler
    {
        public object LockObject { get; set; }


        public ConsoleControler(object lockObject)
        {
            LockObject = lockObject;
        }


        public void WriteInConsole(string message)
        {
            lock (LockObject)
            {
                ClearCurrentConsoleLine();
                Task.WaitAll(Task.Delay(100));
                Console.WriteLine(message);
                Console.CursorTop--;
            }
        }
        public void ClearCurrentConsoleLine()
        {
            lock (LockObject)
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }
    }
}
