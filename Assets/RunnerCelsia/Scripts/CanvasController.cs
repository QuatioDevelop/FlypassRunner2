using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public PatternSystem patternSystem;

    public Slider sliderEnergia;

    public TextMeshProUGUI textoEnergia;
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoVida;

    public GameObject panelSinEnergia;
    public GameObject panelGano;

    public static float energia = 60;
    public static float Score = 0;
    public static float tiempo = 0;
    public static float vida = 0;
    public float tiempoLimite;
    GameObject[] player;

    public int countdown = 3;
    public bool onecall = false;
    public TextMeshProUGUI countdowntext;
    public AudioSource ttogo;

    // Start is called before the first frame update
    void Start()
    {
        sliderEnergia.value = energia;
        tiempoLimite = CanvasOpciones.tiempo;

        energia = 60;
        Score = 0;
        vida = CanvasOpciones.live;
        tiempo = CanvasOpciones.tiempo;
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
        if (patternSystem.loadingComplete)
        {
            if(MovPlayer.countdownover)
            {
                tiempo -= Time.deltaTime;

                textoTiempo.text = "" + tiempo.ToString("00");

                //energia -= Time.deltaTime;

                //sliderEnergia.value = energia;
                if (MovPlayer.stop == true)
                {
                    Score += Time.deltaTime;
                }

                textoEnergia.text = sliderEnergia.value.ToString("00") + "%";
                textoScore.text = Score.ToString("00");
                textoVida.text = vida.ToString("00");

                if (energia <= 0)
                {
                    energia = 0;
                    panelSinEnergia.SetActive(true);
                    Invoke("Restart", 2);
                    MovPlayer.speed = 0;
                    PlayerController.speedX = 0;
                }

                if (vida <= 0)
                {
                    panelGano.SetActive(true);
                    Invoke("Restart", 2);
                    MovPlayer.speed = 0;
                    PlayerController.speedX = 0;
                }

                /*if (energia >= 100)
                    energia = 100;*/

                if (tiempo <= 0)
                {
                    panelGano.SetActive(true);
                    tiempo = 0;
                    Invoke("Restart", 2);
                    MovPlayer.speed = 0;
                    PlayerController.speedX = 0;
                }
            }
            else
            {
                if (!onecall)
                {
                    StartCoroutine(Countdowncorrutine());
                    ttogo.Play();
                    onecall = true;
                }
            }
        }
    }

    public IEnumerator Countdowncorrutine()
    {
        countdowntext.text = countdown.ToString();
        yield return new WaitForSeconds(1);
        countdown -= 1;

        if (countdown <= 0)
        {
            MovPlayer.countdownover = true;
            countdowntext.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(Countdowncorrutine());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        MovPlayer.countdownover = false;
    }

    public void Salir()
    {
        Application.Quit();
    }
}
