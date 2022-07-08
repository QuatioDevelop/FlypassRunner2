using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tollgenerate : MonoBehaviour
{
    public GameObject[] toll;
    public GameObject barrer;
    public GameObject markCoin;
    public GameObject markCoinP;

    public GameObject GenTolls;
    public GameObject GenBarr;
    public GameObject GenMark;
    public GameObject GenCoinP;

    public GameObject player;

    public static int cantidad;
    bool flag = false;

    int select;
    int select2;
    int select3;
    int select4;

    GameObject lasttoll;
    GameObject lastBarr;
    GameObject lastMarkCoin;
    GameObject lastMarkCoinP;

    bool endStart = false;
    float distance;

    Vector3 ubi = new Vector3(0.23f, -0.09f, 160);
    Vector3 x1 = new Vector3(-1.2f, 0, 30);
    Vector3 x2 = new Vector3(1.2f, 0, 30);

    Vector3 x1_2 = new Vector3(-1.2f, 0, 52);
    Vector3 x2_2 = new Vector3(1.2f, 0, 52);

    Vector3 x1_3 = new Vector3(-1.2f, 0, 482);
    Vector3 x2_3 = new Vector3(1.2f, 0, 482);

    float x = 1;
    float y = 1;
    float z = 1;
    float a = 1;

    int i = 0;
    int j = 0;
    int k = 0;
    int l = 0;

    int q = 0;
    int w = 0;
    int e = 0;
    int r = 0;


    // Start is called before the first frame update
    void Start()
    {
        cantidad = CanvasOpciones.tiempo / CanvasOpciones2.Div;

        while (x <= CanvasOpciones2.Dist)
        {
            i++;
            select = Random.Range(0, (toll.Length));
            GameObject tollp = Instantiate(toll[select], GenTolls.transform);
            tollp.transform.position = new Vector3(ubi.x, ubi.y, ubi.z * (i));
            lasttoll = tollp;
            x = lasttoll.transform.position.z;
        }

        while (y <= CanvasOpciones2.Dist)
        {
            j++;
            select2 = (int)Random.Range(0, 2);
            GameObject barr = Instantiate(barrer, GenBarr.transform);

            if (select2 == 1)
            {
                barr.transform.position = new Vector3(x1.x, x1.y, x1.z * (j));
            }
            else
            {
                barr.transform.position = new Vector3(x2.x, x2.y, x2.z * (j));
            }
            lastBarr = barr;
            y = lastBarr.transform.position.z;
        }

        while (z <= CanvasOpciones2.Dist)
        {
            k++;
            select3 = (int)Random.Range(0, 2);
            GameObject coin = Instantiate(markCoin, GenMark.transform);

            if (select3 == 1)
            {
                coin.transform.position = new Vector3(x1_2.x, x1_2.y, x1_2.z * (k));
            }
            else if (select3 == 0)
            {
                coin.transform.position = new Vector3(x2_2.x, x2_2.y, x2_2.z * (k));
            }
            lastMarkCoin = coin;
            z = lastMarkCoin.transform.position.z;
        }

        while (a <= CanvasOpciones2.Dist)
        {
            l++;
            select4 = (int)Random.Range(0, 2);
            GameObject CoinP = Instantiate(markCoinP, GenCoinP.transform);

            if (select4 == 1)
            {
                CoinP.transform.position = new Vector3(x1_3.x, x1_3.y, x1_3.z * (l));
            }
            else if (select4 == 0)
            {
                CoinP.transform.position = new Vector3(x2_3.x, x2_3.y, x2_3.z * (l));
            }
            lastMarkCoinP = CoinP;
            a = lastMarkCoinP.transform.position.z;
        }

        endStart = true;
    }
    void Update()
    {
        if (endStart)
        {
            distance = Vector3.Distance(player.transform.position, lasttoll.transform.position);
            print(distance + "Distancia para iniciar la pool");
        }
        if (distance <= (lasttoll.transform.position.z / 2) && endStart)
        {
            StartCoroutine(pool());
            StartCoroutine(poolBarr());
            StartCoroutine(poolCoin());
            StartCoroutine(poolCoinP());
            endStart = false;
        }
    }

    IEnumerator pool()
    {

        if (GenTolls.transform.GetChild(q))
        {
            Vector3 toll = GenTolls.transform.GetChild(GenTolls.transform.childCount - 1).gameObject.transform.position;
            GenTolls.transform.GetChild(q).gameObject.transform.position = new Vector3(toll.x, toll.y, toll.z + (ubi.z * (q + 1)));
            print(q + " Toll " + i);
            q++;
        }

        if (q == i)
        {
            q = 0;
        }

        yield return new WaitForSeconds(15f);
        StartCoroutine(pool());
    }

    IEnumerator poolBarr()
    {

        if (GenBarr.transform.GetChild(w))
        {
            Vector3 Barr = GenBarr.transform.GetChild(GenBarr.transform.childCount - 1).gameObject.transform.position;
            GenBarr.transform.GetChild(e).gameObject.transform.position = new Vector3(Barr.x, Barr.y, Barr.z + (x1.z * (w + 1)));
            print(w + " Barr " + j);
            w++;
        }

        if (w == j)
        {
            w = 0;
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(poolBarr());
    }

    IEnumerator poolCoin()
    {

        if (GenMark.transform.GetChild(e))
        {
            Vector3 Mark = GenMark.transform.GetChild(GenMark.transform.childCount - 1).gameObject.transform.position;
            GenMark.transform.GetChild(e).gameObject.transform.position = new Vector3(Mark.x, Mark.y, Mark.z + (x2_2.z * (e + 1)));
            print(e + " Coin " + k);
            e++;
        }

        if (e == k)
        {
            e = 0;
        }

        yield return new WaitForSeconds(3f);
        StartCoroutine(poolCoin());
    }

    IEnumerator poolCoinP()
    {
        if (GenCoinP.transform.GetChild(r))
        {
            Vector3 CoinP = GenCoinP.transform.GetChild(GenCoinP.transform.childCount - 1).gameObject.transform.position;
            GenCoinP.transform.GetChild(r).gameObject.transform.position = new Vector3(CoinP.x, CoinP.y, CoinP.z + (x1_3.z * (r + 1)));
            print(r + " Coin P " + l);
            r++;
        }

        if (r == l)
        {
            r = 0;
        }

        yield return new WaitForSeconds(45f);
        StartCoroutine(poolCoinP());
    }
}