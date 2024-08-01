namespace KitchenObject.BL
{
    public interface IObjectWithVolume
    {
        public int Volume { get; set; }
        public int CurrentTemperature { get; set; }
        public int CurrentFilledVolume { get; set; }


        public void FillTheVolume(int volume, int temperature)
        {
            if (CurrentTemperature == 0)
            {
                CurrentTemperature = temperature;
            }
            else
            {
                CurrentTemperature = (CurrentFilledVolume * CurrentTemperature + volume * temperature) / (CurrentFilledVolume + volume);
            }
            CurrentFilledVolume = CurrentFilledVolume + volume < Volume ? CurrentFilledVolume + volume : Volume;
        }
        public void FillTheVolume(IObjectWithVolume objectWithVolume, int volume)
        {
            var canToFillVolume = volume < objectWithVolume.CurrentFilledVolume ? volume : objectWithVolume.CurrentFilledVolume;
            FillTheVolume(canToFillVolume, objectWithVolume.CurrentTemperature);
            objectWithVolume.DrainTheVolume(canToFillVolume);
        }
        public void DrainTheVolume(int volume)
        {
            CurrentFilledVolume = CurrentFilledVolume - volume > 0 ? CurrentFilledVolume - volume : 0;
            if (CurrentFilledVolume == 0)
            {
                CurrentTemperature = 0;
            }
        }
        public void LostTemperaturePerMinute(int minute)
        {
            if (CurrentFilledVolume != 0)
            {
                if (CurrentTemperature > 28)
                {
                    CurrentTemperature = (int)(CurrentTemperature - (minute * (1_000d / Volume)));
                }
                else if (CurrentTemperature < 28)
                {
                    CurrentTemperature += minute * (1_000 / Volume);
                }

            }
        }
        public void FillTheFullVolume(int temperature)
        {
            FillTheVolume(Volume, temperature);
        }
        public void DrainTheFullVolume()
        {
            CurrentFilledVolume = 0;
            CurrentTemperature = 0;
        }
    }
}
