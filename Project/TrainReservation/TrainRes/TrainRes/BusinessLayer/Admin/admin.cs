using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainRes.BusinessLayer.Admin
{
    class admin
    {
        static TrainReservationEntities Rb = new TrainReservationEntities();
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
            Console.WriteLine("-----------WELCOME TO ADMIN MENU----------- ");
            Console.WriteLine("Press 1 for 'Add Train'");
            Console.WriteLine("Press 2 for 'Modify Train'");
            Console.WriteLine("Press 3 for 'Delete Train'");
            Console.WriteLine("Press 4 for 'Show Trains Chat'");
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
                    User.user.ShowTrain();
                    AdminOptions();
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("Invalid Option....");
                    AdminOptions();
                    break;
            }

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
            Rb.TrainDetails.Add(t);
            Rb.SaveChanges();

        }
        public static void UpdateTrain()
        {
            User.user.ShowTrain();
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
                AdminOptions();

            }
            else
            {
                Console.WriteLine("TRAIN NOT FOUND....");
                AdminOptions();
            }

        }
    


        
        public static void DeleteTrain()
        {
            User.user.ShowTrain();
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
