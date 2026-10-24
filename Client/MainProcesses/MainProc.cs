using Shared.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Server.Interfaces;

namespace Client.MainProcesses
{
    public class MainProc
    { 
        public static void MethodForMainProgram()
        { 
            string[] methods = { "Add", "Delete", "GetAll", "GetByld", "Update", "Exit" }; 

            while (true)
            {
                short ind_m = Client.MenuHelper.ManageMenu.ShowMenu(methods, "================ METHODS ================");
                switch (ind_m)
                {
                    case 0:
                        { 
                            Car car = new Car();
                            try
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.WriteLine("Please enter properties of car.");
                                Console.ResetColor();

                                Console.Write("Id: ");
                                car.Id = ushort.Parse(Console.ReadLine()!);

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

                                //repository.Add(car);

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
                        {
                            Console.Clear();
                            Console.Write("Enter an id to delete: ");

                            ushort id = ushort.Parse(Console.ReadLine()!);
                            //repository.Delete(id);
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("========== All Cars ==========");
                            //repository.GetAll();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("========== Get By Id ==========");
                            Console.Write("Enter an id to get: ");
                            short id = Convert.ToInt16(Console.ReadLine());
                            //repository.GetByld(id);
                            break;
                        }
                    case 4:
                        { 
                            //repository.Update(car);
                            break;
                        }
                    case 5:
                        return;
                } 
            }
        }
    }
}
