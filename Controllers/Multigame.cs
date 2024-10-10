using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;

namespace Boggle.Controllers
{
    public class Multigame
    {
        public void PlayHost()
        {
            bool Continue = true;
            Host host = new Host();
            host.makeHost();
            //Makes host and sets up threads to handle clients
            while (Continue == true)
            {
                host.StartGame();
                //Game is run by threads handling clients

                Console.WriteLine("Want to Continue Y/N then enter");
                String? X = Console.ReadLine();
                if (X.Equals("Y")) { }//do nothing
                else if (X.Equals("N"))
                {
                    Continue = false;
                    host.stopGame();
                }//stop
                else
                {
                    while (!X.Equals("Y") || !X.Equals("Y"))
                    {
                        Console.WriteLine("Invalid input do beter Y/N then enter");
                        if (X.Equals("Y")) { }//do nothing
                        else if (X.Equals("N"))
                        {
                            Continue = false;
                            host.stopGame();
                        }//stop
                    }
                }

                    Thread.Sleep(2500);
                host.ShowScore();
                Thread.Sleep(2500);
                
            }


        }


        public void playClient(String IP)
        {
            //Make and connect client to host
            Client b = new Client();
            int t = b.StartClient(IP);
            if (t == 1)
            {
                Console.WriteLine("bad address");
                return;
            }
            while (!b.gameEnded)
            {
                b.StartGame();
                b.playGame();
                // b.SendScore(); need input for score to put here
            }
            //TODO SHOW FINAL SCORE
        }
    }
}

