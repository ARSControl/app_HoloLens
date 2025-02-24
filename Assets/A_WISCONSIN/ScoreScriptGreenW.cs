using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScriptGreenW : MonoBehaviour
{
    //public static ScoreScript instance;

    public int GreenScore = 0;
    public TextMeshPro scoreText;
    public AudioSource rightG;
    //public TextMesh messaggio;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + GreenScore.ToString();
    }

    public string[,] matriceG = new string[4, 2]
        {
            { "Green1", "0" },
            { "Green2", "0" },
            { "Green3", "0" },
            { "Green4", "0" }
        };

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
                    matriceG[r, 1] = "1";
                    rightG.Play();
                }
            }
        }

    }

    void Aumenta()
    {
        GreenScore += 1;
        scoreText.text = "Score: " + GreenScore.ToString();
    }

}
