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
        static TrainReservationEntities Rb = new TrainReservationEntities();
        static string cls;
        static int ttltick;
        static int uid;
        

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
                Console.WriteLine("Enter User Name: ");
                us.UserName = Console.ReadLine();
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
            Console.Write("\t\t****************************************************************************");
            Console.WriteLine("\t\t\t\t\t\t\t\tWelcome To User Menu");
            Console.WriteLine("\t\t****************************************************************************");
            Console.WriteLine("\t\tPress 1 for 'Book Ticket' :-");
            Console.WriteLine("\t\tPress 2 for 'Cancel Ticket' :- ");
            Console.WriteLine("\t\tPress 3 for 'Show Booking Details' :- ");
            Console.WriteLine("\t\tPress 4 for 'Show Cancellation Details' :- ");
            Console.WriteLine("\t\tPress 5 for 'Exit' :-");
            Console.Write("YOUR OPTION CHOICE :- ");
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
                        break;
                    }
                default:
                    Console.WriteLine("Enter the Valid Number");
                    UserOptions();
                    break;
            }

        }

        public static void ShowTrain()
        {
            Console.WriteLine();
            Console.WriteLine("\t\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\t\t\t\t---Train Details---");
            Console.WriteLine("\t\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
           
            var trains = Rb.TrainDetails.Where(t => t.TrainStatus == "Active").ToList();
            int ct = 1;
            Console.WriteLine($"->\tTrain No.\t\tTrain Name\t\tSource Station\t\tFinal Station");
            foreach (var train in trains)
            {
                Console.WriteLine($"{ct}\t{train.TrainNo}\t\t\t{train.TrainName}\t\t{train.Source_Station}\t{train.Final_Station}");
                ct++;

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
                Console.WriteLine("~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                Console.WriteLine("\t\t----Your Booking Details----");
                Console.WriteLine("~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*\n");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine($"User ID: {uid} PNR No: {PNRNo} Booking Date Time: {bt.Booking_Date_Time} Train Number: {trNo}");
                Console.WriteLine($"Class: {cls} Number of Tickets: {ttltick} Total Fare: {tktprc} Status: Booked");
                Console.WriteLine("-------------------------------------------------------------------------------------------------");



            }
            else if (res == 'N' || res == 'n')
            {
                Environment.Exit(0);
            }

        }
        public static void showSeatNCalPrice()
        {
            
            Booking_Details bt = new Booking_Details();
            Console.Write("Enter Train Number You want to Book:  ");
            trNo = int.Parse(Console.ReadLine());

            
            var trSeat = Rb.Class_Type.FirstOrDefault(t => t.TrainNo == trNo);
            var seatPrice = Rb.TicketPrices.FirstOrDefault(t => t.TrainNo == trNo);

            if (trSeat != null)
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("\t\tTicket Prices of Train Class");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine($"\tYou Selected Train Number: {trSeat.TrainNo}\n");
                Console.WriteLine("\tClass    : Seat Available    : Ticket Price         ");
                Console.WriteLine($"\t1 AC       : {trSeat.C1_AC}               : {seatPrice.C1_AC_Price}");
                Console.WriteLine($"\t2 AC       : {trSeat.C2_AC}               : {seatPrice.C2_AC_Price}");
                Console.WriteLine($"\tSleeper    : {trSeat.SL}               : {seatPrice.SL_Price}");
                Console.WriteLine("\t---------Select Class You Want to Book Your Seat----------");
                Console.WriteLine("\tPress 1 for 1AC\n\tPress 2 for 2AC\n\tPress 3 for SL");

            }
            else
            {
                Console.WriteLine("No Train Found.. Please check the train Number");
            }
            // Calculate Ticket Price....

            Console.Write("Type Class: ");
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


            Console.WriteLine("Your Total Ticket Price is: " + tktprc);

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
                Console.WriteLine("Press Y to Continue and N to exit");
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
                Console.WriteLine("No Book ID Found....");
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
                    Console.WriteLine($"Book ID: {bt.PNR_NO}\t\tTrain No: {bt.TrainNo}\t\tBooking Date&Time :{bt.Booking_Date_Time}\n" +
                        $"Source: {bt.TrainDetail.Source_Station}\tDestination: {bt.TrainDetail.Final_Station}\nTotal Fare: {bt.Total_Price}\t Status: {bt.Booking_Status}");
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
                    Console.WriteLine($"Cancel ID: {bt.Cancel_ID}\tTrain No: {bt.TrainNo}\t\tBooking Date&Time :{bt.Cancel_Date_Time}\n" +
                        $"Source: {bt.TrainDetail.Source_Station}\tDestination: {bt.TrainDetail.Final_Station}\nRefund Amount: {bt.Refund_Amount}");
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