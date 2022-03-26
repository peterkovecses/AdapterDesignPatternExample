namespace AdapterDesignPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // The adapter is needed to add the Racecar object to this list.
            List<IVehicle> vehicles = new List<IVehicle>();
            vehicles.Add(new Car("Mercedes", 2000, 286));
            vehicles.Add(new Car("Ford", 1400, 180));
            vehicles.Add(new RacecarToVehicleAdapter(new Racecar("Porsche", 1390, 500)));

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"Weight power ratio: {vehicle.GetWeightPowerRatio()}");
            }
        }

        // ITarget interface
        public interface IVehicle
        {
            double GetWeightPowerRatio();
        }

        public class Car : IVehicle
        {
            public string Manufacturer { get; set; }
            public int Weight { get; set; }
            public double Power { get; set; }

            public Car(string name, int weight, int power)
            {
                Manufacturer = name;
                Weight = weight;
                Power = power;
            }

            double IVehicle.GetWeightPowerRatio()
            {
                return ComputeService.ComputeWeightPowerRatio(Weight, Power);
            }
        }

        // Adaptee class
        public class Racecar
        {
            private int _weight;
            private double _power;

            public string Manufacturer { get; set; }
            public int Weight
            {
                get
                {
                    return _weight;
                }
                set
                {
                    _weight = value;
                    WeightPowerRatio = ComputeService.ComputeWeightPowerRatio(_weight, _power);
                }
            }

            public double Power
            {
                get
                {
                    return _power;
                }
                set
                {
                    _power = value;
                    WeightPowerRatio = ComputeService.ComputeWeightPowerRatio(_weight, _power);
                }
            }

            public double WeightPowerRatio { get; private set; }

            public Racecar(string name, int weight, double power)
            {
                Manufacturer = name;
                Weight = weight;
                Power = power;
            }
        }

        // The adapter is derived from the class to be wrapped and implements the interface to be used.
        public class RacecarToVehicleAdapter : Racecar, IVehicle
        {
            public RacecarToVehicleAdapter(Racecar racecar) : base(racecar.Manufacturer, racecar.Weight, racecar.Power)
            {

            }

            double IVehicle.GetWeightPowerRatio()
            {
                return WeightPowerRatio;
            }
        }

        public static class ComputeService
        {
            public static double ComputeWeightPowerRatio(int weight, double power)
            {
                if (power == 0)
                    return 0;

                return weight / power;
            }
        }
    }
}