
using Shared.SharedClasses;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Client.MainProcesses
{
    public class MainProc
    {
        private const string ServerIp = "127.0.0.1";
        private const int ServerPort = 27001;
         
        public static void AddCar()
        {
            try
            {
                Car car = ReadCarFromConsole();

                using TcpClient client = new TcpClient();
                client.Connect(ServerIp, ServerPort);

                using NetworkStream stream = client.GetStream();
                using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);
                using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true);

                writer.Write("Add");
                WriteCar(writer, car);
                writer.Flush();

                bool result = reader.ReadBoolean();

                Console.ForegroundColor = result
                    ? ConsoleColor.Green
                    : ConsoleColor.Red;

                Console.WriteLine(result
                    ? "Car added successfully!"
                    : "Car could not be added.");

                Console.ResetColor();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }

            Console.ReadKey();
        }
         
        public static void DeleteCar()
        {
            try
            {
                Console.Write("Enter an id to delete: ");
                ushort id = ushort.Parse(Console.ReadLine()!);

                using TcpClient client = new TcpClient();
                client.Connect(ServerIp, ServerPort);

                using NetworkStream stream = client.GetStream();
                using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);
                using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true);

                writer.Write("Delete");
                writer.Write(id);
                writer.Flush();

                bool result = reader.ReadBoolean();

                Console.WriteLine(result ? "Car deleted successfully!" : "Car not found.");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }

            Console.ReadKey();
        } 
        public static void GetAllCars()
        {
            try
            {
                using TcpClient client = new TcpClient();
                client.Connect(ServerIp, ServerPort);

                using NetworkStream stream = client.GetStream();
                using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);
                using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true);

                writer.Write("GetAll");
                writer.Flush();

                int count = reader.ReadInt32();

                Console.WriteLine("========== All Cars ==========");

                for (int i = 0; i < count; i++)
                {
                    Car car = ReadCar(reader);
                    PrintCar(car);
                }

                if (count == 0)
                    Console.WriteLine("No cars found.");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }

            Console.ReadKey();
        }
         
        public static void GetCarById()
        {
            try
            {
                Console.Write("Enter an id to get: ");
                ushort id = ushort.Parse(Console.ReadLine()!);

                using TcpClient client = new TcpClient();
                client.Connect(ServerIp, ServerPort);

                using NetworkStream stream = client.GetStream();
                using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);
                using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true);

                writer.Write("GetById");
                writer.Write(id);
                writer.Flush();

                bool found = reader.ReadBoolean();

                if (found)
                {
                    Car car = ReadCar(reader);
                    PrintCar(car);
                }
                else
                {
                    Console.WriteLine("Car not found.");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }

            Console.ReadKey();
        }
         
        public static void UpdateCar()
        {
            try
            {
                Console.WriteLine("Enter the updated car properties:");
                Car car = ReadCarFromConsole();

                using TcpClient client = new TcpClient();
                client.Connect(ServerIp, ServerPort);

                using NetworkStream stream = client.GetStream();
                using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);
                using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true);

                writer.Write("Update");
                WriteCar(writer, car);
                writer.Flush();

                bool result = reader.ReadBoolean();

                Console.WriteLine(result
                    ? "Car updated successfully!"
                    : "Car not found.");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }

            Console.ReadKey();
        }
         
        private static Car ReadCarFromConsole()
        {
            Car car = new Car();

            Console.Write("Id: ");
            car.Id = ushort.Parse(Console.ReadLine()!);

            Console.Write("Make: ");
            car.Make = Console.ReadLine() ?? "";

            Console.Write("Model: ");
            car.Model = Console.ReadLine() ?? "";

            Console.Write("Year: ");
            car.Year = ushort.Parse(Console.ReadLine()!);

            Console.Write("VIN: ");
            car.VIN = Console.ReadLine() ?? "";

            Console.Write("Color: ");
            car.Color = Console.ReadLine() ?? "";

            return car;
        }
         
        private static void WriteCar(BinaryWriter writer, Car car)
        {
            writer.Write(car.Id);
            writer.Write(car.Make ?? "");
            writer.Write(car.Model ?? "");

            writer.Write(car.Year.HasValue);
            if (car.Year.HasValue)
                writer.Write(car.Year.Value);

            writer.Write(car.VIN ?? "");
            writer.Write(car.Color ?? "");
        }
         
        private static Car ReadCar(BinaryReader reader)
        {
            return new Car
            {
                Id = reader.ReadInt32(),
                Make = reader.ReadString(),
                Model = reader.ReadString(), 
                Year = reader.ReadBoolean() ? reader.ReadUInt16() : null, 
                VIN = reader.ReadString(),
                Color = reader.ReadString()
            };
        }

        private static void PrintCar(Car car)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Id: {car.Id}");
            Console.WriteLine($"Make: {car.Make}");
            Console.WriteLine($"Model: {car.Model}");
            Console.WriteLine($"Year: {car.Year}");
            Console.WriteLine($"VIN: {car.VIN}");
            Console.WriteLine($"Color: {car.Color}");
        }

        private static void ShowError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }

        public static void MethodForMainProgram()
        {
            string[] methods =
            {
                "Add", "Delete", "GetAll", "GetById", "Update", "Exit"
            };

            while (true)
            {
                short ind_m = Client.MenuHelper.ManageMenu.ShowMenu(
                    methods,
                    "================ METHODS ================"
                );

                Console.Clear();

                switch (ind_m)
                {
                    case 0:
                        AddCar();
                        break;

                    case 1:
                        DeleteCar();
                        break;

                    case 2:
                        GetAllCars();
                        break;

                    case 3:
                        GetCarById();
                        break;

                    case 4:
                        UpdateCar();
                        break;

                    case 5:
                        return;
                }
            }
        }
    }
}
