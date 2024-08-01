using System.Drawing;

namespace KitchenObject.BL
{
    public class Multicooker : KitchenObject, IObjectWithVolume, IElectricalKitchenObject
    {
        public int Volume { get; set; } = 5_000;
        public int CurrentTemperature { get; set; }
        public int CurrentFilledVolume { get; set; }
        public bool IsMaxTemperature { get; protected set; }
        public bool IsOnn { get; set; }


        const int MaxTemperature = 80;
        private readonly IObjectWithVolume MulticookerVolume;
        private readonly IElectricalKitchenObject MulticookerElectrical;


        public Multicooker(Color color) : base(color)
        {
            MulticookerVolume = this;
            MulticookerElectrical = this;
        }


        public override void Use()
        {
            if (CurrentFilledVolume != 0)
            {
                WriteToConsoleControler("Food is cooked in a multicooker, you need to clean it");
            }
            MulticookerElectrical.On();
            if (MulticookerElectrical.IsOnn)
            {
                FillWithPause(Volume, 36);
                while (!IsMaxTemperature)
                {
                    Wait(100);
                    RaiseTemperatue(7_500 / Volume);
                }
                WriteToConsoleControler("Start of cooking");
                for (int i = 0; i < 10; i++)
                {
                    Wait(250);
                    CurrentTemperature = 80;
                    Wait(250);
                    CurrentTemperature = 80;
                    Wait(250);
                    CurrentTemperature = 80;
                    Wait(250);
                    CurrentTemperature = 80;
                    WriteToConsoleControler("Cooking....");
                }
                MulticookerElectrical.Off();
            }
            WriteToConsoleControler("The dish is ready");
        }
        protected void RaiseTemperatue(int temperature)
        {
            if (temperature + CurrentTemperature < MaxTemperature)
            {
                CurrentTemperature += temperature;
            }
            else
            {
                CurrentTemperature = MaxTemperature;
                IsMaxTemperature = true;
            }
        }
        public void Clean()
        {
            DrainWithPause(Volume);
        }


        protected void FillWithPause(int needVolumeToFill, int temperature)
        {
            var filledVolume = 0;
            while (filledVolume != needVolumeToFill)
            {
                filledVolume += 50;
                MulticookerVolume.FillTheVolume(50, temperature);
                Wait(100);
            }
        }
        protected void DrainWithPause(int needVolumeToDrain)
        {
            var drainedVolume = 0;
            while (drainedVolume != needVolumeToDrain && CurrentFilledVolume != 0)
            {
                drainedVolume += 50;
                MulticookerVolume.DrainTheVolume(50);
                Wait(100);
            }
        }
        protected void Wait(int minutes)
        {
            Task.WaitAll(Task.Delay(minutes));
        }
    }
}
