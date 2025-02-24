using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScriptRedW : MonoBehaviour
{
    //public static ScoreScript instance;

    public int RedScore = 0;
    public TextMeshPro scoreText;
    public AudioSource rightR;
    //public TextMesh messaggio;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + RedScore.ToString();
    }

    public string[,] matriceR = new string[4, 2]
        {
            { "Red1", "0" },
            { "Red2", "0" },
            { "Red3", "0" },
            { "Red4", "0" }
        };

    public void OnTriggerEnter(Collider other)
    {

        for (int r = 0; r < matriceR.GetLength(0); r++)
        {
            if (other.CompareTag(matriceR[r, 0]))
            {
                if (matriceR[r, 1] == "0")
                {
                    Aumenta();
                    matriceR[r, 1] = "1";
                    rightR.Play();
                }
            }
        }

    }

    void Aumenta()
    {
        RedScore += 1;
        scoreText.text = "Score: " + RedScore.ToString();
    }

}
