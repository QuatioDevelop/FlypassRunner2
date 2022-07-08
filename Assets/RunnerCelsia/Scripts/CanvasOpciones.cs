using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasOpciones : MonoBehaviour
{
    public TMP_InputField tiempoLimite;
    public Slider Sen;
    public TextMeshProUGUI sensText;
    public TMP_InputField liveMax;
    public TMP_InputField liveExtText;
    public TMP_InputField tiempoLimiteExt;

    public static int tiempo = 180;
    public static int live = 3;
    public static float sens = 1;
    public static int liveExt = 3;
    public static int tiempoExt = 30;

    // Start is called before the first frame update
    void Start()
    {
        //tiempoLimite = gameObject.GetComponentInChildren<TMP_InputField>();

        tiempoLimite.text = PlayerPrefs.GetInt("tiempo", 180).ToString();
        liveMax.text = PlayerPrefs.GetInt("vida", 5).ToString();
        Sen.value = PlayerPrefs.GetFloat("sen",1);
        tiempo = PlayerPrefs.GetInt("tiempo", 180);
        live = PlayerPrefs.GetInt("vida", 5);
        sens = PlayerPrefs.GetFloat("sen", 1);
        liveExtText.text = PlayerPrefs.GetInt("vidaExt", 3).ToString();
        tiempoLimiteExt.text = PlayerPrefs.GetInt("tiempoExt", 30).ToString();
        liveExt = PlayerPrefs.GetInt("vidaExt", 3);
        tiempoExt = PlayerPrefs.GetInt("tiempoExt", 30);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Comenzar();

        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(5);

            sensText.text = Sen.value.ToString("0.00");
        /*foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log("KeyCode down: " + kcode);
            }
        }*/

        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A))
        {
            SceneManager.LoadScene(4);
        }
    }

    public void Comenzar()
    {
        int.TryParse(tiempoLimite.text, out tiempo);
        int.TryParse(liveMax.text, out live);
        int.TryParse(tiempoLimiteExt.text, out tiempoExt);
        int.TryParse(liveExtText.text, out liveExt);
        PlayerPrefs.SetInt("tiempo", tiempo);
        PlayerPrefs.SetInt("vida", live);
        PlayerPrefs.SetFloat("sen", Sen.value);
        PlayerPrefs.SetInt("tiempoExt", tiempoExt);
        PlayerPrefs.SetInt("vidaExt", liveExt);
        CanvasOpciones2.MaxVel = PlayerPrefs.GetInt("MaxVel", 50);
        CanvasOpciones2.VarVel = PlayerPrefs.GetFloat("VarVel", 0.2f);
        sens = Sen.value;
        SceneManager.LoadScene(3);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
