using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;



public class NOTTE_RICERCA : MonoBehaviour
{
    public TextMesh finale;

    public int Score = 0;
    public TextMeshPro scoreText;
    public AudioSource right;

   
    void Start()
    {
        
        scoreText.text = "Punteggio: " + Score.ToString();

    }


    public string[,] matrice = new string[12, 2]
        {
            { "Purple1", "0" },
            { "Purple2", "0" },
            { "Purple3", "0" },
            { "Green1", "0" },
            { "Green2", "0" },
            { "Green3", "0" },
            { "Red1", "0" },
            { "Red2", "0" },
            { "Red3", "0" },
            { "Blue1", "0" },
            { "Blue2", "0" },
            { "Blue3", "0" }
        };


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
}


