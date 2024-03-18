using System.Diagnostics;
using System.Drawing;

namespace ParkingLotSytem
{
    
    internal class Program
    {
        enum VehicleSize
        {
            Motorcycle,
            Compact,
            Large
        }
        abstract class Vehicle
        {
            string LicensePlate { get; set; }
            public VehicleSize Size { get; set; }
            public Vehicle(VehicleSize size, string licensePlate)
            {
                Size = size;
                LicensePlate = licensePlate;
            }
        }
        class Motorcycle : Vehicle
        {
            public Motorcycle(string licensePlate) : base(VehicleSize.Motorcycle, licensePlate) { }
        }
        class Car : Vehicle
        {
            public Car(string licensePlate) : base(VehicleSize.Compact, licensePlate) { }
        }
        class Truck : Vehicle
        {
            public Truck(string licensePlate) : base(VehicleSize.Large, licensePlate) { }
        }
        class ParkingSpot
        {
            public Vehicle ParkedVehicle { get; set; }
            public bool isAvailable()
            {
                return ParkedVehicle == null;
            }
            public bool CanFitVehicle(Vehicle vehicle)
            {
                if (ParkedVehicle != null)
                {
                    return false;
                }
                if (vehicle.Size == VehicleSize.Motorcycle)
                {
                    return true;
                }
                if (vehicle.Size == VehicleSize.Compact && ParkedVehicle.Size != VehicleSize.Large)
                {
                    return true;
                }
                if (vehicle.Size == VehicleSize.Large)
                {
                    return true;
                }
                return false;
            }
            public bool ParkVehicle(Vehicle vehicle)
            {
                if (CanFitVehicle(vehicle))
                {
                    ParkedVehicle = vehicle;
                    return true;
                }
                return false;
            }
            public bool RemoveVehicle()
            {
                ParkedVehicle = null;
                return true;
            }
            public Vehicle GetVehicle()
            {
                return ParkedVehicle;
            }
            
        }   
        class Level
        {
            List<ParkingSpot> parkingspots;
            public Level(int spots)
            {
                parkingspots = new List<ParkingSpot>();
                for (int i = 0; i < spots; i++)
                {
                    parkingspots.Add(new ParkingSpot());
                }
            }
            public bool HasAvailableSpot(Vehicle vehicle)
            {
                foreach (ParkingSpot spot in parkingspots)
                {
                    if (spot.CanFitVehicle(vehicle))
                    {
                        return true;
                    }
                }
                return false;
            }
            public bool ParkVehicle(Vehicle vehicle)
            {
                foreach (ParkingSpot spot in parkingspots)
                {
                    if (spot.CanFitVehicle(vehicle))
                    {
                        spot.ParkedVehicle = vehicle;
                        return true;
                    }
                }
                return false;
            }
            public bool RemoveVehicle(Vehicle vehicle)
            {
                foreach (ParkingSpot spot in parkingspots)
                {
                    if (spot.ParkedVehicle == vehicle)
                    {
                        spot.ParkedVehicle = null;
                        return true;
                    }
                }
                return false;
            }
        }
        class ParkingLot
        {
            List<Level> levels;
            public ParkingLot(int numLevels, int numSpots)
            {
                levels = new List<Level>(numLevels);
                for (int i = 0; i < numLevels; i++)
                {
                    this.levels.Add(new Level(numSpots));
                }
            }
            public bool ParkVehicle(Vehicle vehicle)
            {
                foreach (Level level in levels)
                {
                    if (level.HasAvailableSpot(vehicle))
                    {
                        return true;
                    }
                }
                return false;
            }
            public bool RemoveVehicle(Vehicle vehicle)
            {
                foreach (Level level in levels)
                {
                    if (level.RemoveVehicle(vehicle))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        static void Main(string[] args)
        {
            int levels = 2;

            int spotsPerLevel = 10;
            ParkingLot parkingLot = new ParkingLot(levels, spotsPerLevel);

            Car car1 = new Car("ABC123");
            Car car2 = new Car("DEF456");
            Truck truck1 = new Truck("GHI789");
            Motorcycle bike1 = new Motorcycle("JKL012");

            Console.WriteLine("Park Car1: " + parkingLot.ParkVehicle(car1));
            Console.WriteLine("Park Car2: " + parkingLot.ParkVehicle(car2));
            Console.WriteLine("Park Truck1: " + parkingLot.ParkVehicle(truck1));
            Console.WriteLine("Park Bike1: " + parkingLot.ParkVehicle(bike1));

            Console.WriteLine("Remove Car1: " + parkingLot.RemoveVehicle(car1));
            Console.WriteLine("Remove Car2: " + parkingLot.RemoveVehicle(car2));
            Console.WriteLine("Remove Truck1: " + parkingLot.RemoveVehicle(truck1));
            Console.WriteLine("Remove Bike1: " + parkingLot.RemoveVehicle(bike1));
        }
    }
}