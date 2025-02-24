using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class ScoreScriptRed : MonoBehaviour
{

    private const int bufferSize = 1024;
    private const string serverIP = "192.168.1.8"; //indirizzo IP del computer --> cmd > ipconfig
    private const int port = 12346;

    // Create tcp/ip socket
    //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


    private Socket clientSocket;
    private bool isConnected = false;


    public int RedScore = 0;
    public TextMeshPro scoreText;
    public AudioSource rightR;

    //variabile per identificare il gioco
    public int gameNumber;


    public string[,] matriceR = new string[6, 2]
    {
        { "Red", "0" },
        { "Red1", "0" },
        { "Red2", "0" },
        { "Red3", "0" },
        { "Red4", "0" },
        { "Red5", "0" }
        //{ "Red6", "0" },
        //{ "Red7", "0" },
        //{ "Red8", "0" },
        //{ "Red9", "0" }
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

        scoreText.text = "Red Score: " + RedScore.ToString();
    }
    */

    void Start()
    {
        TryConnect();

        scoreText.text = "Red Score: " + RedScore.ToString();
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


    void OnTriggerEnter(Collider other)
    {

        // Verifica se il cubo rosso ha appena toccato il piano rosso
        for (int r = 0; r < matriceR.GetLength(0); r++)
        {
            if (other.CompareTag(matriceR[r, 0]))
            {
                if (matriceR[r, 1] == "0")
                {
                    Aumenta();
                    rightR.Play();
                    matriceR[r, 1] = "1";
                }
            }
        }
    }

    void Aumenta()
    {
        RedScore += 1;
        scoreText.text = "Red Score: " + RedScore.ToString();

        /*
        DateTime orario = DateTime.Now;
        string orarioString = orario.ToString("HH:mm:ss");

        string numcubiRed = "Red: " + RedScore.ToString() + ", " + orarioString + "\n";
        SendDataToServer(numcubiRed);
        */

        if (isConnected)
        {
            try
            {
                DateTime orario = DateTime.Now;
                string orarioString = orario.ToString("HH:mm:ss");
                string numcubiRed = "Red: " + RedScore.ToString() + ", " + orarioString + "\n";
                SendDataToServer(numcubiRed);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Failed to send data: " + ex.Message);
                isConnected = false; // Segniamo la connessione come persa
            }
        }
    }

    /*
    void SendDataToServer(string numcubiRed)
    {
        byte[] msgBytes = Encoding.ASCII.GetBytes(numcubiRed);
        clientSocket.Send(msgBytes);
    }
    */

    void SendDataToServer(string numcubiRed)
    {
        if (isConnected && clientSocket != null)
        {
            try
            {
                byte[] msgBytes = Encoding.ASCII.GetBytes(numcubiRed);
                clientSocket.Send(msgBytes);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Failed to send data: " + ex.Message);
                isConnected = false;
            }
        }
    }

    /*
    private void OnApplicationQuit()
    {
        // close socket
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
    */

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
