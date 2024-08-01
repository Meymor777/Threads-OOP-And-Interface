using System.Drawing;

namespace KitchenObject.BL
{

    public abstract class KitchenObject
    {
        public readonly Color Color;
        public ConsoleControler ConsoleControler { get; set; }


        protected KitchenObject(Color color)
        {
            Color = color;
        }


        public abstract void Use();
        public void WriteToConsoleControler(string message)
        {
            ConsoleControler.WriteInConsole(message);
        }
    }
}