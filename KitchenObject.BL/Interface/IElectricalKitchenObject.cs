namespace KitchenObject.BL
{
    public interface IElectricalKitchenObject
    {
        public bool IsOnn { get; set; }


        public void On()
        {
            IsOnn = true;
        }
        public void Off()
        {
            IsOnn = false;
        }
    }

}
