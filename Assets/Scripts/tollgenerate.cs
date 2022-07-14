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

    Vector3 x1_3 = new Vector3(-1.2f, 0, 130);
    Vector3 x2_3 = new Vector3(1.2f, 0, 130);

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

    bool coin = true;
    bool barr = true;
    bool coinp = true;
    bool tolls = true;


    // Start is called before the first frame update
    void Awake()
    {

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
            for (int i = 0; i < GenTolls.transform.childCount; i++)
            {
                if ((lastBarr.transform.localPosition.z) >= (GenTolls.transform.GetChild(i).gameObject.transform.position.z - 35) && (lastBarr.transform.localPosition.z) <= (GenTolls.transform.GetChild(i).gameObject.transform.position.z + 35))
                {
                    barr.SetActive(false);
                }
            }
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

    void Start()
    {
        lasttoll = GenTolls.transform.GetChild(i / 2).gameObject;
    }

    void Update()
    {
        if (player.transform.position.z >= (lasttoll.transform.position.z) && endStart)
        {
            
            if(tolls && coin && coinp && barr)
            {
                StartCoroutine(pool());
                StartCoroutine(poolBarr());
                StartCoroutine(poolCoin());
                StartCoroutine(poolCoinP());
                tolls = false;
                barr = false;
                coin = false;
                coinp = false;
                endStart = false;
            }
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
            tolls = true;
            q = 0;
            lasttoll = GenTolls.transform.GetChild(i/2).gameObject;
            endStart = true;
        }

        if (q == i/2)
        {
            tolls = true;     
            lasttoll = GenTolls.transform.GetChild(GenTolls.transform.childCount - 1).gameObject;
            endStart = true;
        }

        yield return new WaitForSeconds(0.1f);
        if (!tolls)
        {
            StartCoroutine(pool());
        }
    }

    IEnumerator poolBarr()
    {

        if (GenBarr.transform.GetChild(w))
        {
            GenBarr.transform.GetChild(w).gameObject.SetActive(true);
            Vector3 Barr = GenBarr.transform.GetChild(GenBarr.transform.childCount - 1).gameObject.transform.position;
            GenBarr.transform.GetChild(w).gameObject.transform.position = new Vector3(Barr.x, Barr.y, Barr.z + (x1.z * (w + 1)));
            print(w + " Barr " + j);
            for (int i = 0; i < GenTolls.transform.childCount; i++)
            {
                if ((GenBarr.transform.GetChild(w).gameObject.transform.position.z) >= (GenTolls.transform.GetChild(i).gameObject.transform.position.z - 35) && (GenBarr.transform.GetChild(w).gameObject.transform.position.z) <= (GenTolls.transform.GetChild(i).gameObject.transform.position.z + 35))
                {
                    GenBarr.transform.GetChild(w).gameObject.SetActive(false);
                }
            }
            w++;
        }

        if (w == j)
        {
            w = 0;
            barr = true;
        }

        if(w == j / 2)
        {
            barr = true;
        }

        yield return new WaitForSeconds(0.1f);
        if (!barr)
        {
            StartCoroutine(poolBarr());
        }
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
            coin = true;
        }

        if (e == k/2)
        {
            coin = true;
        }

        yield return new WaitForSeconds(0.1f);
        if (!coin)
        {
            StartCoroutine(poolCoin());
        }
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
            coinp = true;
        }

        if (r == l/2)
        {
            coinp = true;
        }

        yield return new WaitForSeconds(0.1f);
        if (!coinp)
        {
            StartCoroutine(poolCoinP());
        }
    }
}