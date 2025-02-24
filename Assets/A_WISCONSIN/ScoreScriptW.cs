using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreScriptW : MonoBehaviour
{

    public ScoreScriptGreenW Green;
    public ScoreScriptRedW Red;
    public ScoreScriptBlueW Blue;
    public ScoreScriptPurpleW Purple;
    
    public TextMesh messaggio;

    void Update()
    {
        Fine();
    }

    void Fine()
    {
        if (Green.GreenScore == Green.matriceG.GetLength(0) & Red.RedScore == Red.matriceR.GetLength(0) & Blue.BlueScore == Blue.matriceB.GetLength(0) & Purple.PurpleScore == Purple.matriceP.GetLength(0))
        {
            messaggio.text = "Ce l'hai fatta!";
        }
    }

}
