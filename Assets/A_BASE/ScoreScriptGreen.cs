using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class ScoreScriptGreen : MonoBehaviour
{
    //public static ScoreScript instance;

    private const int bufferSize = 1024;
    private const string serverIP = "192.168.1.8"; //indirizzo IP del computer --> cmd > ipconfig
    private const int port = 12346;

    // Create tcp/ip socket
    //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private Socket clientSocket;
    private bool isConnected = false;


    public int GreenScore = 0;
    public TextMeshPro scoreText;
    public AudioSource rightG;

    //variabile per identificare il gioco
    public int gameNumber;

    public string[,] matriceG = new string[6, 2]
    {
        { "Green", "0" },
        { "Green1", "0" },
        { "Green2", "0" },
        { "Green3", "0" },
        { "Green4", "0" },
        { "Green5", "0" }
        //{ "Green6", "0" },
        //{ "Green7", "0" },
        //{ "Green8", "0" },
        //{ "Green9", "0" }

    };

    /*
    // Start is called before the first frame update
    void Start()
    {
        try
        {

            // connect to server
            clientSocket.Connect(IPAddress.Parse(serverIP), port);
            Debug.Log("Connected to server.");

            SendGameHeader(gameNumber);

        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }

        scoreText.text = "Green Score: " + GreenScore.ToString();
    }
    */

    void Start()
    {
        TryConnect();

        scoreText.text = "Green Score: " + GreenScore.ToString();
    }

    private void TryConnect()
    {
        try
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Impostiamo un timeout più breve per il tentativo di connessione
            IAsyncResult result = clientSocket.BeginConnect(IPAddress.Parse(serverIP), port, null, null);
            bool success = result.AsyncWaitHandle.WaitOne(1000, true); // 1 secondo di timeout

            if (success)
            {
                clientSocket.EndConnect(result);
                isConnected = true;
                Debug.Log("Connected to server.");
                SendGameHeader(gameNumber);
            }
            else
            {
                // Timeout raggiunto
                clientSocket.Close();
                isConnected = false;
                Debug.Log("Connection timeout - running in offline mode.");
            }
        }
        catch (Exception ex)
        {
            isConnected = false;
            Debug.Log("Connection failed - running in offline mode. Error: " + ex.Message);
        }
    }

    void SendGameHeader(int gameNumber)
    {
        if (isConnected)
        {
            string header = $"\n=== INIZIO GIOCO {gameNumber} ===\n";
            SendDataToServer(header);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
       
        // Verifica se il cubo verde ha appena toccato il piano verde
        for (int r = 0; r < matriceG.GetLength(0); r++)
        {
            if (other.CompareTag(matriceG[r, 0]))
            {
                if (matriceG[r, 1] == "0")
                {
                    Aumenta();
                    rightG.Play();
                    matriceG[r, 1] = "1";
                }
            }
        }

    }

    void Aumenta()
    {
        GreenScore += 1;
        scoreText.text = "Green Score: " + GreenScore.ToString();
        /*
        DateTime orario = DateTime.Now;
        string orarioString = orario.ToString("HH:mm:ss");

        string numcubiGreen = "Green: " + GreenScore.ToString() + ", " + orarioString + "\n";
        SendDataToServer(numcubiGreen);
        */

        if (isConnected)
        {
            try
            {
                DateTime orario = DateTime.Now;
                string orarioString = orario.ToString("HH:mm:ss");
                string numcubiGreen = "Green: " + GreenScore.ToString() + ", " + orarioString + "\n";
                SendDataToServer(numcubiGreen);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Failed to send data: " + ex.Message);
                isConnected = false; // Segniamo la connessione come persa
            }
        }
    }
    /*
    void SendDataToServer(string numcubiGreen)
    {
        byte[] msgBytes = Encoding.ASCII.GetBytes(numcubiGreen);
        clientSocket.Send(msgBytes);
    }


    private void OnApplicationQuit()
    {
        // close socket
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
    */

    void SendDataToServer(string numcubiGreen)
    {
        if (isConnected && clientSocket != null)
        {
            try
            {
                byte[] msgBytes = Encoding.ASCII.GetBytes(numcubiGreen);
                clientSocket.Send(msgBytes);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Failed to send data: " + ex.Message);
                isConnected = false;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (isConnected && clientSocket != null)
        {
            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Error during socket cleanup: " + ex.Message);
            }
        }
    }
}
