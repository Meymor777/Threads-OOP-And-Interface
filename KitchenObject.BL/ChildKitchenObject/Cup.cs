using System.Drawing;

namespace KitchenObject.BL
{
    public class Cup : KitchenObject, IObjectWithVolume
    {
        public int Volume { get; set; } = 500;
        public int CurrentTemperature { get; set; }
        public int CurrentFilledVolume { get; set; }


        private readonly IObjectWithVolume CupVolume;


        public Cup(Color color) : base(color)
        {
            CupVolume = this;
        }


        public override void Use()
        {
            if (CurrentFilledVolume == 0)
            {
                WriteToConsoleControler("Cup is empty, need water");
            }
            else if (CurrentTemperature < 40)
            {
                DrainWithPause(Volume);
                WriteToConsoleControler("Water in cup is cold, need new hot water");
            }
            else
            {
                DrinkTea();
            }
        }
        private void DrinkTea()
        {
            if (CurrentTemperature > 80)
            {
                WriteToConsoleControler("Waiting to normally temperature of tea");
            }
            while (CurrentTemperature > 80)
            {
                Wait(1000);
            }
            while (CurrentFilledVolume != 0)
            {
                Wait(1000);
                TakeASip();
            }
            WriteToConsoleControler("Wonderful tea!");
        }
        private void TakeASip()
        {
            CupVolume.DrainTheVolume(50);
            WriteToConsoleControler("Take a sip");
        }


        public void PourFromTheKettle(IObjectWithVolume kettle)
        {
            if (kettle.CurrentTemperature < 90)
            {
                WriteToConsoleControler("The temperature of the water in the kettle is less than 90, it must be boiled");
                return;
            }
            DrainWithPause(Volume);
            PourFromKettleWithPause(Volume, kettle);
        }


        protected void FillWithPause(int needVolumeToFill, int temperature)
        {
            var filledVolume = 0;
            while (filledVolume != needVolumeToFill)
            {
                filledVolume += 50;
                CupVolume.FillTheVolume(50, temperature);
                Wait(100);
            }
        }
        protected void DrainWithPause(int needVolumeToDrain)
        {
            var drainedVolume = 0;
            while (drainedVolume != needVolumeToDrain && CurrentFilledVolume != 0)
            {
                drainedVolume += 50;
                CupVolume.DrainTheVolume(50);
                Wait(100);
            }
        }
        protected void PourFromKettleWithPause(int needVolumeToDrain, IObjectWithVolume kettle)
        {
            var filledVolume = 0;
            while (filledVolume != Volume)
            {
                filledVolume += 50;
                CupVolume.FillTheVolume(kettle, 50);
                Wait(100);
            }
        }
        protected void Wait(int minutes)
        {
            Task.WaitAll(Task.Delay(minutes));
        }
    }
}
