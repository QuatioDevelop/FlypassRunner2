﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tollgenerate : MonoBehaviour
{
    public GameObject[] toll;
    public GameObject barrer;
    public GameObject markCoin;
    public static int cantidad;
    bool flag = false;
    int select;
    int select2;
    int select3;
    int x = 0;
    Vector3 ubi = new Vector3(0.23f, -0.09f, 160);
    Vector3 x1 = new Vector3(-1.2f, 0, 30);
    Vector3 x2 = new Vector3(1.2f, 0, 30);

    Vector3 x1_2 = new Vector3(-1.2f, 0, 52);
    Vector3 x2_2 = new Vector3(1.2f, 0, 52);
    // Start is called before the first frame update
    void Start()
    {
        cantidad = CanvasOpciones.tiempo / CanvasOpciones2.Div;
        for (int i = 0; i < cantidad; i++)
        {
            select = Random.Range(0, (toll.Length));
            GameObject tollp = Instantiate(toll[select], this.transform);
            tollp.transform.position = new Vector3(ubi.x, ubi.y, ubi.z * (i + 1));
        }

        for (int i = 0; i < ((cantidad * CanvasOpciones2.Div) * 2); i++)
        {

            select2 = (int)Random.Range(0, 2);

            if (select2 == 1)
            {

                GameObject tollp = Instantiate(barrer, this.transform);
                tollp.transform.position = new Vector3(x1.x, x1.y, x1.z * (i + 1));

            }
            else
            {

                GameObject tollp = Instantiate(barrer, this.transform);
                tollp.transform.position = new Vector3(x2.x, x2.y, x2.z * (i + 1));

            }
        }

        for (int i = 0; i < (cantidad * CanvasOpciones2.Div); i++)
        {

            select3 = (int)Random.Range(0, 3);

            if (select2 == 1)
            {

                GameObject tollp = Instantiate(markCoin, this.transform);
                tollp.transform.position = new Vector3(x1_2.x, x1_2.y, x1_2.z * (i + 1));

            }
            else if (select2 == 0)
            {

                GameObject tollp = Instantiate(markCoin, this.transform);
                tollp.transform.position = new Vector3(x2_2.x, x2_2.y, x2_2.z * (i + 1));

            }
        }
    }
}
