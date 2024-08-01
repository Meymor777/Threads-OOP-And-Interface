using System.Drawing;

namespace KitchenObject.BL
{
    public class Kettle : KitchenObject, IObjectWithVolume
    {
        public int Volume { get; set; } = 2_500;
        public int CurrentTemperature { get; set; }
        public int CurrentFilledVolume { get; set; }
        public bool IsMaxTemperature { get; protected set; }
        public bool WatterIsBoiling { get; protected set; }


        const int MaxTemperature = 100;
        private readonly IObjectWithVolume KettleVolume;


        public Kettle(Color color) : base(color)
        {
            KettleVolume = this;
        }


        public override void Use()
        {
            if (CurrentTemperature < 90)
            {
                IsMaxTemperature = false;
            }
            if (!IsMaxTemperature)
            {
                if (CurrentFilledVolume < 500)
                {
                    DrainWithPause(Volume);
                    FillWithPause(Volume, 36);
                }
                BoilWatter();
                StopBoiling();
            }
        }
        protected virtual void BoilWatter()
        {
            WriteToConsoleControler("Start boiling water");
            IsMaxTemperature = false;
            WatterIsBoiling = true;
            while (!IsMaxTemperature)
            {
                Wait(100);
                RaiseTemperatue(7_500 / Volume);
            }
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
        private void StopBoiling()
        {
            WriteToConsoleControler("Stop boiling water");
            WatterIsBoiling = false;
        }


        protected void FillWithPause(int needVolumeToFill, int temperature)
        {
            var filledVolume = 0;
            while (filledVolume != needVolumeToFill)
            {
                filledVolume += 50;
                KettleVolume.FillTheVolume(50, temperature);
                Wait(100);
            }
        }
        protected void DrainWithPause(int needVolumeToDrain)
        {
            var drainedVolume = 0;
            while (drainedVolume != needVolumeToDrain && CurrentFilledVolume != 0)
            {
                drainedVolume += 50;
                KettleVolume.DrainTheVolume(50);
                Wait(100);
            }
        }
        protected void Wait(int minutes)
        {
            Task.WaitAll(Task.Delay(minutes));
        }
    }
}
