using System.Drawing;

namespace KitchenObject.BL
{
    public class ElectricKettle : Kettle, IElectricalKitchenObject
    {
        public bool IsPowered { get; set; }
        public bool IsOnn { get; set; }


        private readonly IElectricalKitchenObject KettleElectrical;


        public ElectricKettle(Color color) : base(color)
        {
            KettleElectrical = this;
            Volume = 3_000;
        }


        public override void Use()
        {
            if (CurrentTemperature < 90)
            {
                IsMaxTemperature = false;
            }
            if (!IsMaxTemperature)
            {
                if (CurrentFilledVolume < 600)
                {
                    DrainWithPause(Volume);
                    FillWithPause(Volume, 36);

                }
                BoilWatter();
            }
        }
        protected override void BoilWatter()
        {
            KettleElectrical.On();
            if (KettleElectrical.IsOnn)
            {
                WriteToConsoleControler("Start boiling water");
                IsMaxTemperature = false;
                WatterIsBoiling = true;
                while (!IsMaxTemperature)
                {
                    Wait(100);
                    RaiseTemperatue(9_000 / Volume);
                }
                WriteToConsoleControler("Stop boiling water");
                WatterIsBoiling = false;
                KettleElectrical.Off();
            }
        }
    }
}
