namespace KitchenObject.BL
{
    public class StatusControler
    {
        public object LockObject { get; set; }
        public bool EndControling { get; set; }
        public List<IObjectWithVolume> ObjectsWithVolumes { get; set; }


        public StatusControler(object lockObject, List<IObjectWithVolume> objectsWithVolumes)
        {
            LockObject = lockObject;
            ObjectsWithVolumes = objectsWithVolumes;
        }


        public void WriteStatus()
        {
            while (!EndControling)
            {
                lock (LockObject)
                {
                    var cursorBefore = Console.CursorTop;
                    Console.CursorTop = 1;
                    foreach (var objectWithVolumes in ObjectsWithVolumes)
                    {
                        var type = objectWithVolumes.GetType();
                        Console.WriteLine($"{type.Name.PadRight(21)}:  {objectWithVolumes.CurrentFilledVolume.ToString().PadRight(4)}  " +
                            $"{objectWithVolumes.CurrentTemperature.ToString().PadRight(3)}");
                    }
                    Console.CursorTop = cursorBefore;
                }
                Task.WaitAll(Task.Delay(100));
            }
        }
    }
}
