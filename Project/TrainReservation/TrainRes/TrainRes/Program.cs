using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainRes.BusinessLayer.Admin;
using TrainRes.BusinessLayer.User;

namespace TrainRes
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool flag = true;
            while(flag)
            {
                Console.WriteLine("\t\t~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                Console.WriteLine("\t\t\t\t----Indian Railways Wishes You A Happy Journey----");
                Console.WriteLine("\t\t~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*\n");

                Console.WriteLine("\t\tPress 1 for Admin :-");
                Console.WriteLine("\t\tPress 2 for User :-");
                Console.WriteLine("\t\tPress 3 for Exit :-");

                int n = int.Parse(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        {
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            Console.WriteLine("Enter Your Admin Details..... ");
                            admin.ValidateAdmin();
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter Your User Details:");
                            user.UserLogin();
                           
                            break;
                        }
                    case 3:
                        {
                            flag = false;
                            Console.WriteLine("Hope You Like our Services...");
                            Console.WriteLine("Have a Great Day!");
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid Selection....");
                        break;
                }
            }
            
            Console.Read();
        }

    }
}
