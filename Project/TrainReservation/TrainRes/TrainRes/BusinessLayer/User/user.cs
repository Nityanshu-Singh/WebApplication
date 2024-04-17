using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainRes.BusinessLayer.User
{
    class user
    {
        static int tktprc = 0;
        static int trNo;
        static TrainReservationEntities1 Rb = new TrainReservationEntities1();
        static string cls;
        static int ttltick;
        static int uid;
        static int age;
        

        public static void UserLogin()
        {

            UserDetail us = new UserDetail();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\tPress 1 for Existing User");
            Console.WriteLine("\t2.Press 2 for New User");
            Console.Write("Your Option Choice: ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            if (n == 1)
            {
                ValidateUser();
            }
            else if (n == 2)
            {
                Console.WriteLine("Enter UserId: ");
                uid = int.Parse(Console.ReadLine()); 
                us.UserID = uid;
                Console.WriteLine("Enter Your Name: ");
                us.UserName = Console.ReadLine();
                Console.WriteLine("Enter Your Age: ");
                age = int.Parse(Console.ReadLine());
                us.Age = age;
                Console.WriteLine("Enter Your Password: ");
                us.Password = Console.ReadLine();

                Rb.UserDetails.Add(us);
                Rb.SaveChanges();
            }


        }
        static void ValidateUser()
        {
            Console.Write("Enter UserId: ");
            uid = int.Parse(Console.ReadLine());
            Console.Write("Enter Your Password: ");
            string pass = Console.ReadLine();
            bool vl = validate(uid, pass);
            if (vl)
            {
                Console.WriteLine("Validation Successful.......");
                UserOptions();
            }
            else
            {
                Console.WriteLine("Validation Failed");
                UserLogin();

            }
        }
        private static bool validate(int uid, string pass)
        {
            var user = Rb.UserDetails.FirstOrDefault(x => x.UserID == uid && x.Password == pass);
            return user != null;
        }
        public static void UserOptions()
        {
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("\t\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("\t\t      Welcome To User Menu     ");
                Console.WriteLine("\t\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("\t\t1. Book Ticket");
                Console.WriteLine("\t\t2. Cancel Ticket");
                Console.WriteLine("\t\t3. Show Booking Details");
                Console.WriteLine("\t\t4. Show Cancellation Details");
                Console.WriteLine("\t\t5. Exit");
                Console.Write("\t\tEnter your choice: ");
                int n = int.Parse(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        {
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            BookTicket(uid);
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            CanceledTicket();
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            ShowBookedTicket(uid);
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            CanceledTicket(uid);
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            break;
                        }
                    case 5:
                        {
                            flag = false;
                            break;
                        }
                    default:
                        Console.WriteLine("Enter the Valid Number");
                        
                        break;
                }
            }

        }

        public static void ShowTrain()
        {
            Console.WriteLine();
            Console.WriteLine("\t\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\t\t\t\t---Active Train Details---");
            Console.WriteLine("\t\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            var activeTrains = Rb.TrainDetails.Where(t => t.TrainStatus == "Active").ToList();
            int count = 1;
            Console.WriteLine($"S.no\tTrain No.\t\tTrain Name\t\t\tSource Station\t\tFinal Station");
            foreach (var train in activeTrains)
            {
                Console.WriteLine($"{count}\t{train.TrainNo,-12}\t{train.TrainName,-25}\t{train.Source_Station,-20}\t{train.Final_Station,-20}");
                count++;
            }

            Console.WriteLine("\t\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

        }
        public static void BookTicket(int uid)    // we can add status booked or cancelled....????
        {

            Class_Type ct = new Class_Type();
            TicketPrice tp = new TicketPrice();
            Booking_Details bt = new Booking_Details();
            UserDetail ud = new UserDetail();
            //Console.WriteLine(us.user_id);
            ShowTrain();

            showSeatNCalPrice();
            Console.WriteLine("Press Y to continue and N to exits: ");
            char res = char.Parse(Console.ReadLine());
            if (res == 'Y' || res == 'y')
            {
                
               
                Random r = new Random();
                int PNRNo = r.Next(10000, 99999);      // for generating random number....
                bt.PNR_NO = PNRNo;
                bt.Booking_Date_Time = DateTime.Now;
                bt.TrainNo = trNo;
                bt.Class_Type = cls;
                bt.NumberofTickets = ttltick;
                bt.Total_Price = tktprc;
                bt.UserID = uid;
                bt.Booking_Status = "Booked";

                Rb.Booking_Details.Add(bt);

                Rb.SaveChanges();

                Rb.UpdateBooking(trNo, cls, ttltick);   // call procedure to update the seats...
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("\t\t\t\t----Your Booking Details----");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine($"| User ID: {uid,-10} | PNR No: {PNRNo,-10} | Booking Date Time: {bt.Booking_Date_Time,-20} | Train Number: {trNo,-10} |");
                Console.WriteLine($"| Class: {cls,-9} | Number of Tickets: {ttltick,-10} | Total Ticket Price: {tktprc,-15} | Status: Booked |");
                Console.WriteLine("------------------------------------------------------------------------------------------------");

            }
            else if (res == 'N' || res == 'n')
            {
                Environment.Exit(0);
            }

        }
        public static void showSeatNCalPrice()
        {
            
            Booking_Details bt = new Booking_Details();
            here:
            Console.Write("Enter Train Number You want to Book:  ");
            trNo = int.Parse(Console.ReadLine());

            
            var trSeat = Rb.Class_Type.FirstOrDefault(t => t.TrainNo == trNo);
            var seatPrice = Rb.TicketPrices.FirstOrDefault(t => t.TrainNo == trNo);

            var trSeat1 = Rb.TrainDetails.Where(t => t.TrainNo == trNo && t.TrainStatus == "Active").FirstOrDefault();
            if (trSeat1 != null)
            {
                Console.WriteLine("\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("\t\t\tTicket Prices by Train Class");
                Console.WriteLine("\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine($"\tSelected Train Number: {trSeat.TrainNo}\n");

                Console.WriteLine("\t-----------------------------------------------------------");
                Console.WriteLine("\t| Class      | Seats Available | Ticket Price             |");
                Console.WriteLine("\t-----------------------------------------------------------");
                Console.WriteLine($"\t| 1. 1AC     | {trSeat.C1_AC,-15} | {seatPrice.C1_AC_Price,-25} |");
                Console.WriteLine($"\t| 2. 2AC     | {trSeat.C2_AC,-15} | {seatPrice.C2_AC_Price,-25} |");
                Console.WriteLine($"\t| 3. Sleeper | {trSeat.SL,-15} | {seatPrice.SL_Price,-25} |");
                Console.WriteLine("\t-----------------------------------------------------------");
                Console.WriteLine("\t--- Please Select the Class You Want to Book Your Seat ---");
                Console.WriteLine("\tPress 1 for 1AC\n\tPress 2 for 2AC\n\tPress 3 for Sleeper");



            }
            else
            {
                Console.WriteLine("No Train Found.. Please check the train Number");
                goto here;
            }
            // Calculate Ticket Price....

            Console.Write("Press for choice for chooose Class Type: ");
            cls = Console.ReadLine();
            Console.Write("Enter the Number of Tickets you want to book: ");
            ttltick = int.Parse(Console.ReadLine());          // number of seats...
           if (cls == "1".ToUpper())
            {
                tktprc = (int)(ttltick * seatPrice.C1_AC_Price);
            }
            else if (cls == "2".ToUpper())
            {
                tktprc = ((int)(ttltick * seatPrice.C2_AC_Price));
            }
            else if (cls == "3".ToUpper())
            {
                tktprc = (int)(ttltick * seatPrice.SL_Price);
            }


            Console.WriteLine("Your Total Ticket Price is:Rs." + tktprc);

        }

        public static void CanceledTicket()
        {
            CancelTicket ct = new CancelTicket();
            Booking_Details bt = new Booking_Details();
            Console.WriteLine("Enter Your Book ID to cancel: ");
            int bid = int.Parse(Console.ReadLine());
            var x = Rb.Booking_Details.Find(bid);
            if (x != null)
            {

                ct.PNR_No = x.PNR_NO;
                Random r = new Random();
                int cid = r.Next(10000, 99999);
                ct.Cancel_ID = cid;
                ct.UserId = x.UserID;
                ct.TrainNo = x.TrainNo;
                ct.Cancel_Date_Time = DateTime.Now;

                ct.Refund_Amount = (int)x.Total_Price-120;


                Console.WriteLine($"Your Refunded Amount will be: {ct.Refund_Amount} After Rs.120 Deduction");
                Console.WriteLine("Press Y to Cancel Ticket and N to exit");
                char res = char.Parse(Console.ReadLine());
                if (res == 'Y' || res == 'y')
                {
                    Rb.CancelTickets.Add(ct);           // bookId(fk in cancel ticket) is showing null as u remove primary key from booktable,

                    x.Booking_Status = "Cancelled";          // here I am changing status of booking details...
                    //calling stored procedure for updating seats
                    var trno = (int)x.TrainNo;
                    string classtype = x.Class_Type;
                    int nutic = x.NumberofTickets;
                    Rb.UpdateCancelTicket(trno, classtype, nutic);

                    Rb.SaveChanges();
                }
                else if (res == 'N' || res == 'n')
                {
                    Environment.Exit(0);
                }



            }
            else
            {
                Console.WriteLine("No PNR No. Found....");
            }
        }


        static void ShowBookedTicket(int uid)
        {
            var booked_tkt = Rb.Booking_Details.Where(bt => bt.UserID == uid);
            //var booked_tkt = Rb.bookTickets.Find(uid);
            if (booked_tkt.Any())            //The Any() method checks if there are any matching booked tickets.  NOT Working->if(booked_tkt!=null)
            {
                foreach (var bt in booked_tkt)
                {
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------");
                    Console.WriteLine($"Booking Details:");
                    Console.WriteLine($"  Book ID: {bt.PNR_NO}\t\tTrain No: {bt.TrainNo}\t\tBooking Date & Time: {bt.Booking_Date_Time}\n" +
                        $"  Source Station: {bt.TrainDetail.Source_Station}\tFinal Station: {bt.TrainDetail.Final_Station}\n" +
                        $"  Total Fare: {bt.Total_Price}\t\tStatus: {bt.Booking_Status}");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------\n");

                }
            }
            else
            {
                Console.WriteLine("No Booking Details Found");
            }
        }

        static void CanceledTicket(int uid)
        {
            var cancel_tkt = Rb.CancelTickets.Where(bt => bt.UserId == uid);
            //var cancel_tkt = Rb.cancelTickets.Find(uid);
            if (cancel_tkt.Any())
            {
                foreach (var bt in cancel_tkt)
                {
                    Console.WriteLine("\n---------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"Cancellation Details:");
                    Console.WriteLine($"  Cancel ID: {bt.Cancel_ID}\tTrain No: {bt.TrainNo}\t\tCancelling Date & Time: {bt.Cancel_Date_Time}\n" +
                        $"  Source Station: {bt.TrainDetail.Source_Station}\tFinal Station: {bt.TrainDetail.Final_Station}\n" +
                        $"  Refund Amount: {bt.Refund_Amount}");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------\n");

                }
            }
            else
            {
                Console.WriteLine("No Cancellation Details Found");
            }
        }
    }
}