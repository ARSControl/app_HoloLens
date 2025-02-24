using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScriptO : MonoBehaviour
{

    public ScoreScriptGreenO Green;
    public ScoreScriptRedO Red;
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
