namespace Boggle.Controllers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;

    public class Host
{
    //Host is player 0
    // Clients start from 1 up
    String[] Names = new String[10];
    int[] playerScore = new int[10];
    
    private List<TcpClient> clients = new List<TcpClient>();
    int numPlayers = 1;
    public static TcpListener? server;
    private bool gameStarted = false;
    public bool EndGame = false;
    static private object clientsLock = new object();
    private bool isRunning = true;
    public String Address = null;



    // Code that makes the Host and opens up the IP addresses
    public void makeHost()
    {
        IPAddress address = IPAddress.Any;
        
        server = new TcpListener(address, 14921);
        
        server.Start();
       
        
        String playerCount = "Host Started Address: " + IPAddress.Broadcast.ToString() + " waiting for players player count: 0";

        this.Address= address.ToString();

        Console.WriteLine(playerCount);
        Task.Run(() => this.takeClients());


    }

    private void takeClients()
    {
        while (!gameStarted)
        {
            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("Client connected.");



            // Add clients to the list in a synchronized manner
            lock (clientsLock)
            {
                clients.Add(client);
            }
            Console.WriteLine("There are %d clients", clients.Count());

            // Start a thread to handle client messages
            Task.Run(() => HandleClientMessages(client, clients.Count()));
        }
        server.Stop();
    }

    private void HandleClientMessages(TcpClient client, int ClientId)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[512];
        int bytesRead;


        while (this.EndGame == false)
        {
            while (this.gameStarted == true)
            {
                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    
                        

                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);


                    //TODO Put WHat to to with client INFO
                    // ITS IN MESSAGE
                    int Score = int.Parse(message);
                    lock (clientsLock)
                    {
                        playerScore[ClientId] += Score;
                    }


                    //put score in place





                }
                catch
                {
                   
                }
            }
        }
            lock (clientsLock)
            {
                clients.Remove(client);
            }

        
    }

    public void ShowScore() //should show score
    {

    }

    public void SEND(string message)
    {
        lock (clientsLock)
        {
            foreach (TcpClient client in clients)
            {
                
                    NetworkStream stream = client.GetStream();

                    byte[] ByteMessage = Encoding.ASCII.GetBytes(message);

                    stream.Write(ByteMessage, 0, ByteMessage.Length);
                
            }
        }
    }

    public void StartGame()
    {
        //When game is started
        Console.WriteLine("Type enter to Start Game");


        Console.ReadLine();
        this.gameStarted = true;
        this.SEND("START");

        Console.WriteLine("GameStarted");
        //
        //ADD GAME CODE
        //below

    }

    public void stopGame()
    {
        this.EndGame = true;
        this.SEND("END");
    }
    

}
