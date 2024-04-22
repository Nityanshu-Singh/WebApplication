using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainRes.BusinessLayer.Admin
{
    class admin
    {
        static TrainReservationEntities1 Rb = new TrainReservationEntities1();
        static TrainDetail t = new TrainDetail();
        public static void AdminLogin()
        {

        }
        public static void ValidateAdmin()
        {
            Console.WriteLine("Enter AdminId: ");
            int Aid = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Password: ");
            string pass = Console.ReadLine();
            bool vl = validate(Aid, pass);
            if (vl)
            {
                AdminOptions();
            }
            else
            {
                Console.WriteLine("Invalid AdminId or Password !!");
                ValidateAdmin();

            }
        }
        private static bool validate(int Aid, string pass)
        {
            var admin = Rb.AdminDetails.FirstOrDefault(x => x.AdminID == Aid && x.Admin_Password == pass);
            return admin != null;
        }
        public static void AdminOptions()
        {
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("-----------WELCOME TO ADMIN MENU----------- ");
                Console.WriteLine("Press 1 for 'Add Train'");
                Console.WriteLine("Press 2 for 'Modify Train'");
                Console.WriteLine("Press 3 for 'Delete Train'");
                Console.WriteLine("Press 4 for 'Show Trains Chart'");
                Console.WriteLine("Press 5 for 'Exit'");
                int n = int.Parse(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        Addtrain();
                        break;
                    case 2:
                        UpdateTrain();
                        break;
                    case 3:
                        DeleteTrain();
                        break;
                    case 4:
                        DisplayAdminTrain();
                        UpdateTrain();
                        //AdminOptions();
                        break;
                    case 5:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Option....");
                      
                        break;
                }
            }

        }

        public static void DisplayAdminTrain()
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\t\t\t\t\t---Train Details---");
            Console.ResetColor();
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
            var trains = Rb.TrainDetails.ToList();
            int ct = 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"->\tTrain-No\tTrain-Name\t\tSource\t\tDestination\t\tStatus");
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            foreach (var train in trains)
            {
                Console.WriteLine($"{ct}\t{train.TrainNo}\t\t{train.TrainName.PadRight(20)}\t{train.Source_Station.PadRight(10)}\t{train.Final_Station.PadRight(15)}\t{train.TrainStatus}");
                ct++;
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");

        }
        public static void Addtrain()
        {
            TrainDetail t = new TrainDetail();
            Console.WriteLine("Enter Train Number: ");
            t.TrainNo = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Train Name: ");
            t.TrainName = Console.ReadLine();
            Console.WriteLine("Enter the Source Station: ");
            t.Source_Station = Console.ReadLine();
            Console.WriteLine("Enter the Final Station: ");
            t.Final_Station = Console.ReadLine();
            t.TrainStatus = "Active";
            Rb.TrainDetails.Add(t);
            Rb.SaveChanges();

            // Adding seats of new trains
            Console.Write("Enter 1AC Seats: ");
            int firstAcSeats = int.Parse(Console.ReadLine());
            Console.Write("Enter 2AC Seats: ");
            int SecAcSeats = int.Parse(Console.ReadLine());
            Console.Write("Enter SL Seats: ");
            int SLSeats = int.Parse(Console.ReadLine());
            Rb.AddclassSeats(t.TrainNo, firstAcSeats, SecAcSeats, SLSeats);    // calling procedure to add the train seats of 1ac,2ac,and sl class.

            // Adding fare of new trains
            Console.Write("Enter 1AC Ticket Price: ");
            int firstAcTicketPrice = int.Parse(Console.ReadLine());
            Console.Write("Enter 2AC Ticket Price: ");
            int SecAcTicketPrice = int.Parse(Console.ReadLine());
            Console.Write("Enter SL Ticket Price: ");
            int SLTicketPrice = int.Parse(Console.ReadLine());
            Rb.AddclassPrice(t.TrainNo, firstAcTicketPrice, SecAcTicketPrice, SLTicketPrice); // calling procedure to add the fares....

            //Rb.SaveChanges();
            Console.WriteLine("SuccessFully Train is Added....");

        }
        public static void UpdateTrain()
        {
            DisplayAdminTrain();
            Console.WriteLine("Enter the Train Number You want to modify: ");
            int trNo = int.Parse(Console.ReadLine());
            var updTrNo = Rb.TrainDetails.Find(trNo);
            if (updTrNo != null)
            {
                Console.WriteLine($"Enter Train New Name for Train Number: {trNo}");
                string tname = Console.ReadLine();
                updTrNo.TrainName = tname;
                Rb.SaveChanges();
                Console.WriteLine("Your Train Name Has been modified...");
                //AdminOptions();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("TRAIN NOT FOUND....");
                Console.ResetColor();
                AdminOptions();
            }

        }
    


        
        public static void DeleteTrain()
        {
            DisplayAdminTrain();
            Console.WriteLine("Enter Train Number You want to delete: ");
            int trNo = int.Parse(Console.ReadLine());
            var trainRemove = Rb.TrainDetails.Find(trNo);
            if (trainRemove != null)
            {
                Console.WriteLine("Are You Sure You Want to delete: Y/N");
                string ans = Console.ReadLine().ToUpper();
                if (ans == "Y")
                {

                    
                    trainRemove.TrainStatus = "NotRunning";        
                    Rb.SaveChanges();
                    Console.WriteLine("Your Train Has been Deleted....");
                    AdminOptions();
                }
                else
                {
                    AdminOptions();
                }
            }
            else
            {
                AdminOptions();
            }
        }
    }
}
