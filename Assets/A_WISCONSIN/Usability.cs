using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;



public class Usability : MonoBehaviour
{
    public TextMesh finale;

    public int Score = 0;
    public TextMeshPro scoreText;
    public AudioSource right;

    void Start()
    {
        scoreText.text = "Score: " + Score.ToString();

    }

    public string[,] matrice = new string[16, 2]
        {
            { "Purple1", "0" },
            { "Purple2", "0" },
            { "Purple3", "0" },
            { "Purple4", "0" },
            { "Green1", "0" },
            { "Green2", "0" },
            { "Green3", "0" },
            { "Green4", "0" },
            { "Red1", "0" },
            { "Red2", "0" },
            { "Red3", "0" },
            { "Red4", "0" },
            { "Blue1", "0" },
            { "Blue2", "0" },
            { "Blue3", "0" },
            { "Blue4", "0" },
        };


    public void OnTriggerEnter(Collider other)
    {

        // Verifica se il cubo verde ha appena toccato il piano verde
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
        scoreText.text = "Score: " + Score.ToString();
        /*
        DateTime orario = DateTime.Now;
        string orarioString = orario.ToString("HH:mm:ss");
        addScore(Score.ToString(), orarioString, "C:\\Users\\gabbi\\OneDrive - Unimore\\Desktop\\UNIVERSITA\\LAUREA MAGISTRALE\\TIROCINIO\\tcp_socket-main\\numcubi.txt");
        */
    }

    /*
    public static void addScore(string Score, string orario, string filepath)
    {
        try 
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                file.WriteLine(Score + ',' + orario);
            }
        }
        catch(Exception ex)
        {
            throw new Exception("This program failed:", ex);
        }
    }
    */
    public void Update()
    {
        End();
    }

    void End()
    {
        if (Score == 16)
        {
            finale.text = "Ce l'hai fatta!";
        }
    }


}


