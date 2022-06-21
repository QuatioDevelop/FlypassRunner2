using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tollgenerate : MonoBehaviour
{
    public GameObject[] toll;
    public GameObject barrer;
    public static int cantidad;
    bool flag = false;
    int select;
    int select2;
    int x = 0;
    Vector3 ubi = new Vector3(0.23f, -0.09f, 160);
    Vector3 x1 = new Vector3(-1.2f, 0, 30);
    Vector3 x2 = new Vector3(1.2f, 0,  30);
    // Start is called before the first frame update
    void Start()
    {
        cantidad = CanvasOpciones.tiempo/5;
        for(int i = 0; i < cantidad; i++)
        {
            select = Random.Range(0, (toll.Length));  
            GameObject tollp = Instantiate(toll[select], this.transform );
            tollp.transform.position = new Vector3(ubi.x, ubi.y, ubi.z * (i+1));
        }

        for(int i = 0; i < ((cantidad*5)*2); i++)
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
    }
}
