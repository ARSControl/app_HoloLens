using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScriptBlueW : MonoBehaviour
{
    //public static ScoreScript instance;

    public int BlueScore = 0;
    public TextMeshPro scoreText;
    public AudioSource rightB;
    //public TextMesh messaggio;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + BlueScore.ToString();
    }

    public string[,] matriceB = new string[4, 2]
        {
            { "Blue1", "0" },
            { "Blue2", "0" },
            { "Blue3", "0" },
            { "Blue4", "0" }
        };

    public void OnTriggerEnter(Collider other)
    {

        // Verifica se il cubo verde ha appena toccato il piano verde
        for (int r = 0; r < matriceB.GetLength(0); r++)
        {
            if (other.CompareTag(matriceB[r, 0]))
            {
                if (matriceB[r, 1] == "0")
                {
                    Aumenta();
                    matriceB[r, 1] = "1";
                    rightB.Play();
                }
            }
        }

    }

    void Aumenta()
    {
        BlueScore += 1;
        scoreText.text = "Score: " + BlueScore.ToString();
    }

}
