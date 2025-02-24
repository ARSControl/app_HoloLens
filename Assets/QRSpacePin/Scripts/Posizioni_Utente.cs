using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.EventSystems;



public class Posizioni_Utente : MonoBehaviour
{
    private const int bufferSize = 1024;
    private const string serverIP = "192.168.1.8"; //indirizzo IP del computer --> cmd > ipconfig
    private const int port = 12345;

    public GameObject Utente;

    private Vector3 previousPosition;
    private float timeSinceLastPosition;

    private float lastSaveTime; // Tempo dell'ultimo salvataggio
    private float saveInterval = 1f; // Intervallo di tempo per il salvataggio (in secondi)


    // Create tcp/ip socket
    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    

    // Start is called before the first frame update
    public void Start()
    {

        try
        {

            // Initialize position tracking variables
            previousPosition = Camera.main.transform.position;
            //timeSinceLastPosition = 0f;

            // connect to server
            clientSocket.Connect(IPAddress.Parse(serverIP), port);
            Debug.Log("Connected to server.");

        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }

        lastSaveTime = Time.time;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSaveTime >= saveInterval)
        {
            SaveValues();
            lastSaveTime = Time.time;
        }

    }


    void SaveValues()
    {
        float x = Camera.main.transform.position.x;
        float y = Camera.main.transform.position.y;
        float z = Camera.main.transform.position.z;

        // tempo trascorso da inizio applicazione
        // float currentTime = Time.time;

        // Ottieni il tempo reale corrente
        DateTime orario = DateTime.Now;
        string orarioString = orario.ToString("HH:mm:ss");  // Stampa orario in una stringa


        string pos = x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ", " + ", " + orarioString + "\n";

        SendDataToServer(pos);

    }

    
    void SendDataToServer(string pos)
    {
        byte[] msgBytes = Encoding.ASCII.GetBytes(pos);
        clientSocket.Send(msgBytes);
    }


    private void OnApplicationQuit()
    {
        // close socket
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }

}
