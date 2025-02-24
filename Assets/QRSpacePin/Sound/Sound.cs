using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource right;

    void OnTriggerEnter()
    {
        right.Play();
    }
}
