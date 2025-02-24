using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;



public class NumCubiUsability: MonoBehaviour
{

    private const int bufferSize = 1024;
    private const string serverIP = "192.168.1.8"; //indirizzo IP del computer --> cmd > ipconfig
    private const int port = 12346;

    // Create tcp/ip socket
    //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private Socket clientSocket;
    private bool isConnected = false;


    public TextMesh finale;

    public int Score = 0;
    public TextMeshPro scoreText;
    public AudioSource right;

    public int gameNumber;


    public string[,] matrice = new string[12, 2]
    {
        { "Purple1", "0" },
        { "Purple2", "0" },
        { "Purple3", "0" },
        //{ "Purple4", "0" },
        { "Green1", "0" },
        { "Green2", "0" },
        { "Green3", "0" },
        //{ "Green4", "0" },
        { "Red1", "0" },
        { "Red2", "0" },
        { "Red3", "0" },
        //{ "Red4", "0" },
        { "Blue1", "0" },
        { "Blue2", "0" },
        { "Blue3", "0" }
        //{ "Blue4", "0" }
    };

    /*
    void Start()
    {

        try
        {

            // connect to server
            clientSocket.Connect(IPAddress.Parse(serverIP), port);
            Debug.Log("Connected to server.");

        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }

        scoreText.text = "Punteggio: " + Score.ToString();

    }
    */

    void Start()
    {
        TryConnect();

        scoreText.text = "Punteggio: " + Score.ToString();
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

        // Verifica se il cubo ha toccato il piano 
        for (int r = 0; r < matrice.GetLength(0); r++)
        {
            if (other.CompareTag(matrice[r, 0]))
            {
                if (matrice[r, 1] == "0")
                {
                    Aumenta();
                    matrice[r, 1] = "1";
                    right.Play();
                }
            }
        }

    }

    public void Aumenta()
    {
        Score += 1;
        scoreText.text = "Punteggio: " + Score.ToString();
        /*
        DateTime orario = DateTime.Now;
        string orarioString = orario.ToString("HH:mm:ss");

        string numcubi = Score.ToString() + ", " + orarioString + "\n";
        SendDataToServer(numcubi);
        */
        if (isConnected)
        {
            try
            {
                DateTime orario = DateTime.Now;
                string orarioString = orario.ToString("HH:mm:ss");
                string numcubiRed = "Punteggio: " + Score.ToString() + ", " + orarioString + "\n";
                SendDataToServer(numcubiRed);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Failed to send data: " + ex.Message);
                isConnected = false; // Segniamo la connessione come persa
            }
        }
    }


    public void Update()
    {
        End();
    }


    void End()
    {
        if (Score == 12)
        {
            finale.text = "Ce l'hai fatta!";
        }
    }

    /*
    void SendDataToServer(string numcubi)
    {
        byte[] msgBytes = Encoding.ASCII.GetBytes(numcubi);
        clientSocket.Send(msgBytes);
    }


    private void OnApplicationQuit()
    {
        // close socket
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
    */

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



