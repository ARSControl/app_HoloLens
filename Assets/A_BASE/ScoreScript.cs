using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScript : MonoBehaviour
{
    
    public ScoreScriptGreen Green;
    public ScoreScriptRed Red;
    public TextMesh messaggio;

    void Update()
    {
        Fine();
    }

    void Fine()
    {
        if (Green.GreenScore == Green.matriceG.GetLength(0) & Red.RedScore == Red.matriceR.GetLength(0))
        {
            messaggio.text = "Ce l'hai fatta!";
        }
    }

}
