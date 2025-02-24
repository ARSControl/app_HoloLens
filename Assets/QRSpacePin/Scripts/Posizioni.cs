// CONSIDERO SFERA FERMA PER 4 SECONDI PRIMA DI SALVARE LA POSIZIONE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.EventSystems;

public class Posizioni : MonoBehaviour
{
    private const int bufferSize = 1024;
    private const string serverIP = "192.168.1.6"; //indirizzo IP del computer --> cmd > ipconfig
    private const int port = 12345;

    public GameObject sferaPrefab;

    private Vector3 previousPosition;
    private float timeSinceLastPosition;
    

    // Create tcp/ip socket
    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


    // Start is called before the first frame update
    public void Start()
    {
        try
        {

            // Initialize position tracking variables
            previousPosition = sferaPrefab.transform.position;
            timeSinceLastPosition = 0f;

            // connect to server
            clientSocket.Connect(IPAddress.Parse(serverIP), port);
            Debug.Log("Connected to server.");


        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (spawnedSfera.activeSelf)
        //{
            float x = sferaPrefab.transform.position.x;
            float y = sferaPrefab.transform.position.y;
            float z = sferaPrefab.transform.position.z;

            // Check if the position has changed
            if (sferaPrefab.transform.position != previousPosition)
            {
                // If the position has changed, reset the timer and update the previous position
                timeSinceLastPosition = 0f;
                previousPosition = sferaPrefab.transform.position;
            }
            else
            {
                // If the position is the same, increment the timer
                timeSinceLastPosition += Time.deltaTime;
            }

            // If the object has stayed in the same position for more than 5 seconds, save the position
            if (timeSinceLastPosition >= 5f)
            {
                string pos = x.ToString() + ", " + y.ToString() + ", " + z.ToString() + "\n";
                byte[] msgBytes = Encoding.ASCII.GetBytes(pos);
                clientSocket.Send(msgBytes);

                // Reset the timer to avoid repeated saving of the same position
                timeSinceLastPosition = 0f;
            }
        //}
    }

    private void OnApplicationQuit()
    {
        // close socket
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }

}
