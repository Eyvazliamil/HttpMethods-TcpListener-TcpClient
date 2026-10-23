using Shared.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Client.MainProcesses
{
    public class MainProc
    {
        public static void MethodForMainProgram()
        { 
            string[] methods = { "Add", "Delete", "GetAll", "GetByld", "Update", "Exit" }; 
            var car = new Car();

            var client = new TcpClient("127.0.0.1", 27001);
            //var stream = client.GetStream();

            //var bw = new BinaryWriter(stream);
            //var br = new BinaryReader(stream); 
             
            while (true)
            {
                short ind_m = Client.MenuHelper.ManageMenu.ShowMenu(methods, "================ METHODS ================");
                switch (ind_m)
                {
                    case 0:
                        {
                            try
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.WriteLine("Please enter properties of car.");
                                Console.ResetColor();
                                Console.Write("Make: ");
                                car.Make = Console.ReadLine();

                                Console.Write("Model: ");
                                car.Model = Console.ReadLine();

                                Console.Write("Year: ");
                                car.Year = ushort.Parse(Console.ReadLine()!);

                                Console.Write("VIN: ");
                                car.VIN = Console.ReadLine();

                                Console.Write("Color: ");
                                car.Color = Console.ReadLine();

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Car added successfully!");
                                Console.ResetColor();
                                Console.ReadKey();
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Error: {ex.Message}");
                                Console.ResetColor(); 
                                Console.ReadKey();
                            }
                            break;
                        }
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        return;
                } 
            }
        }
    }
}
