namespace KitchenObject.BL
{
    public class TemperatureControler
    {
        public object LockObject { get; set; }
        public bool EndControling { get; set; }
        public List<IObjectWithVolume> ObjectsWithVolumes { get; set; }


        public TemperatureControler(object lockObject, List<IObjectWithVolume> objectsWithVolumes)
        {
            LockObject = lockObject;
            ObjectsWithVolumes = objectsWithVolumes;
        }


        public void ControlTemperature()
        {
            while (!EndControling)
            {
                Task.WaitAll(Task.Delay(1000));
                lock (LockObject)
                {
                    foreach (var objectWithVolumes in ObjectsWithVolumes)
                    {
                        objectWithVolumes.LostTemperaturePerMinute(1);
                    }
                }
            }
        }
    }
}
