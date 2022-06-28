using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RankData : MonoBehaviour
{
    public string name;
    public string document;
    public float score;
    public string email;

    public RankData()
    {
        name = "Dummi";
        document = "0123456789";
        score = 10f;
        email = "dummi@mail.com";
    }
}
