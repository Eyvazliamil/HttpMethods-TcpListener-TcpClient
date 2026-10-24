using Server.Interfaces;
using Shared.SharedClasses;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    { 
        Repository repository = new Repository();   
        var tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 27001);

        tcpListener.Start(10);

        Console.WriteLine($"{tcpListener.Server.LocalEndPoint} listening.....");

        while (true)
        {
            TcpClient client = tcpListener.AcceptTcpClient();

            using NetworkStream stream = client.GetStream();

            using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
            using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);

            try
            {
                string command = reader.ReadString(); 
                 
                if (command == "Add")
                {
                    Car car = ReadCar(reader);

                    Console.WriteLine($"Received car: {car.Id}");

                    bool result = repository.Add(car);

                    writer.Write(result);
                    writer.Flush();

                    Console.WriteLine(result ? $"Car {car.Id} added to server." : "Car could not be added.");
                }
                else if(command == "Delete")
                {
                    ushort id = reader.ReadUInt16();
                    bool result = repository.Delete(id);

                    writer.Write(result);
                    Console.WriteLine(result ? $"Car {id} deleted." : $"Car {id} not found.");
                    writer.Write(result);
                    writer.Flush();
                }
                else if(command == "GetAll")
                {
                    var cars = repository.GetAll();

                    writer.Write(cars!.Count);

                    foreach (Car car in cars)
                        WriteCar(writer, car);

                    Console.WriteLine($"Sent {cars.Count} cars to client.");  
                    writer.Flush();

                }
                else if(command == "GetById")
                {
                    ushort id = reader.ReadUInt16();
                    Car? car = repository.GetByld(id);

                    writer.Write(car != null);

                    if (car != null)
                        WriteCar(writer, car); 
                    writer.Flush();
                }
                else if(command == "Update")
                {
                    Car car = ReadCar(reader);
                    bool result = repository.Update(car);

                    writer.Write(result);
                    Console.WriteLine( result ? $"Car {car.Id} updated." : $"Car {car.Id} not found."); 
                    writer.Write(result);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
        static Car ReadCar(BinaryReader reader)
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

        static void WriteCar(BinaryWriter writer, Car car)
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
    }
}