using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;

namespace Boggle.Controllers
{
    public class Client
    {
        public TcpClient? client;
        public NetworkStream? stream;
        private bool gameStarted = false;
        public bool gameEnded = false;






        //waits until host address is entered in to command line
        public string getHostAddress(String IP)
        {

            Console.WriteLine("Enter host Address then hit enter");
            while (true)
            {
                String? iP = Console.ReadLine(); // This line pauses the program until Enter is pressed

                if (iP == null)
                {
                    Console.WriteLine("enter a non empty address");
                }
                else
                {
                    return iP;
                }


            }
        }











        //Makes the client thread that talks to the host
        public int StartClient(String IP)
        {

            this.client = new TcpClient();
            try
            {
                this.client.Connect(IPAddress.Parse(IP), 14921);
                this.stream = this.client.GetStream();
                Thread receiveThread = new Thread(StartGame);
                receiveThread.Start();

            }
            catch { return 1; }
            return 0;



        }











        //waits for host to start game and then says game is started
        public void StartGame()
        {
            byte[] buffer = new byte[512];
            int bytesRead;

            while (!gameEnded)
            {
                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    
                    

                    string info = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    if (info.Equals("Start")){
                        Console.WriteLine("Game Starting");
                        this.gameStarted = true;
                    }
                    if (info.Equals("END"))
                    {
                        Console.WriteLine("GAME OVER");
                        this.gameEnded = true;
                    }
                }
                catch
                {
                 
                }
                //Left over code to kill connection
                //   Console.WriteLine("Host disconnected.");
                // stream.Close();
                // client.Close();
            }





        }

        public void playGame() //Plays the game and flips gameStarted
        {
            //Play Game

            // wait for next game
            this.gameStarted = false;
        }





        //Send info to host
        public void SendScore(int Info)
        {
            String Message = Info.ToString();
            byte[] InfoBytes = Encoding.ASCII.GetBytes(Message);
            this.stream.Write(InfoBytes, 0, InfoBytes.Length);

        }

    }
}





