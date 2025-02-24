
// secondo piano: GREEN + PARALL


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Dynamic;
using System.Net.Sockets;
using System.Net;
using UnityEngine.SocialPlatforms.Impl;
using System.Text;

public class WISCONSIN3 : MonoBehaviour
{
    
    private const int bufferSize = 1024;
    private const string serverIP = "192.168.1.8"; //indirizzo IP del computer --> cmd > ipconfig
    private const int port = 12346;

    // Create tcp/ip socket
    //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private Socket clientSocket;
    private bool isConnected = false;


    public int Score3 = 0;
    public TextMeshPro scoreText;
    public AudioSource right;

    public string[,] primo = new string[12, 4];
    public MATRICE mat;

    public bool firstCondition = true;
    public float interval = 60.0f;
    public float elapsedTime = 0.0f;

    public int gameNumber;


    void Start()
    {
        /*
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
        */

        TryConnect();

        scoreText.text = "Score: " + Score3.ToString();

        mat = GetComponent<MATRICE>();

        //creare copia della matrice principale
        Array.Copy(mat.matrice, primo, mat.matrice.Length);

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

    public void Update()
    {
        //aggiorna il tempo trascorso
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= interval)
        {
            //alterna la condizione
            firstCondition = !firstCondition;

            //reset del tempo trascorso
            elapsedTime = 0.0f;

        }
    }


    public void OnTriggerEnter(Collider other)
    {

        for (int r = 0; r < primo.GetLength(0); r++)
        {
            if (other.CompareTag(primo[r, 0]))
            {
                if (firstCondition)
                {
                    if (primo[r, 3] == "0" && primo[r, 1] == "green")
                    {
                        Aumenta(primo[r, 0]);
                        primo[r, 3] = "1";
                        right.Play();
                    }
                }
                else
                {
                    if (primo[r, 3] == "0" && primo[r, 2] == "parall")
                    {
                        Aumenta(primo[r, 0]);
                        primo[r, 3] = "1";
                        right.Play();
                    }
                }

            }
        }

    }

    public void Aumenta(string objectType)
    {
        Score3 += 1;
        scoreText.text = "Score: " + Score3.ToString();

        /*
        DateTime orario = DateTime.Now;
        string orarioString = orario.ToString("HH:mm:ss");

        string numcubi = objectType + " su vassoio 2: " + Score3.ToString() + ", " + orarioString + "\n";
        SendDataToServer(numcubi);
        */

        if (isConnected)
        {
            try
            {
                DateTime orario = DateTime.Now;
                string orarioString = orario.ToString("HH:mm:ss");
                string numcubi = objectType + " su vassoio 2: " + Score3.ToString() + ", " + orarioString + "\n";
                SendDataToServer(numcubi);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Failed to send data: " + ex.Message);
                isConnected = false; // Segniamo la connessione come persa
            }
        }
    }


    /*
    void SendDataToServer(string numcubi)
    {
        byte[] msgBytes = Encoding.ASCII.GetBytes(numcubi);
        clientSocket.Send(msgBytes);
    }
    */

    void SendDataToServer(string numcubi)
    {
        if (isConnected && clientSocket != null)
        {
            try
            {
                byte[] msgBytes = Encoding.ASCII.GetBytes(numcubi);
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
