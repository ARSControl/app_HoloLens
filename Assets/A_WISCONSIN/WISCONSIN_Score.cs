using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WISCONSIN_Score : MonoBehaviour
{

    public WISCONSIN3 Green;
    public WISCONSIN2 Red;
    public WISCONSIN4 Blue;
    public WISCONSIN1 Purple;

    public MATRICE mat;
    public string[,] finale = new string[12, 4];

    public TextMesh messaggio;

    public void Start()
    {
        mat = GetComponent<MATRICE>();
        Array.Copy(mat.matrice, finale, mat.matrice.Length);
    }

    void Update()
    {
        Fine();
    }

    void Fine()
    {
        if (Red.Score2 + Green.Score3 + Blue.Score4 + Purple.Score1 == finale.GetLength(0)) //verifica che somma di tutti i punteggi sia uguale a lunghezza matrice 
        {
            messaggio.text = "Ce l'hai fatta!";
        }
    }

}
