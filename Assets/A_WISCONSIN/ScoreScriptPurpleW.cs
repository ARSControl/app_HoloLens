using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScriptPurpleW : MonoBehaviour
{
    //public static ScoreScript instance;

    public int PurpleScore = 0;
    public TextMeshPro scoreText;
    public AudioSource rightP;
    //public TextMesh messaggio;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + PurpleScore.ToString();
    }

    public string[,] matriceP = new string[4, 2]
        {
            { "Purple1", "0" },
            { "Purple2", "0" },
            { "Purple3", "0" },
            { "Purple4", "0" }
        };

    public void OnTriggerEnter(Collider other)
    {

        // Verifica se il cubo verde ha appena toccato il piano verde
        for (int r = 0; r < matriceP.GetLength(0); r++)
        {
            if (other.CompareTag(matriceP[r, 0]))
            {
                if (matriceP[r, 1] == "0")
                {
                    Aumenta();
                    matriceP[r, 1] = "1";
                    rightP.Play();
                }
            }
        }

    }

    void Aumenta()
    {
        PurpleScore += 1;
        scoreText.text = "Score: " + PurpleScore.ToString();
    }

}
