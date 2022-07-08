using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasOpciones2 : MonoBehaviour
{
    public TMP_InputField MaxVelText;
   
    public TMP_InputField VarVelText;

    public TMP_InputField DivText;

    public static int MaxVel = 50;
    public static float VarVel = 0.2f;
    public static int  Div = 5;
    public static int Dist = 800;

    // Start is called before the first frame update
    void Start()
    {
        //tiempoLimite = gameObject.GetComponentInChildren<TMP_InputField>();
        MaxVelText.text = PlayerPrefs.GetInt("MaxVel", 50).ToString();
        VarVelText.text = PlayerPrefs.GetFloat("VarVel", 0.2f).ToString();
        DivText.text = PlayerPrefs.GetInt("Div", 5).ToString();
        MaxVel = PlayerPrefs.GetInt("MaxVel", 50);
        VarVel = PlayerPrefs.GetFloat("VarVel", 0.2f);
        Div = PlayerPrefs.GetInt("Div", 5);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Comenzar();

        /*foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log("KeyCode down: " + kcode);
            }
        }*/
    }

    public void Comenzar()
    {
        MaxVel = int.Parse(MaxVelText.text);
        VarVel = float.Parse(VarVelText.text);
        Div = int.Parse(DivText.text);
        PlayerPrefs.SetInt("MaxVel", MaxVel);
        PlayerPrefs.SetInt("Div", Div);
        PlayerPrefs.SetFloat("VarVel", VarVel);
        SceneManager.LoadScene(0);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Clearplayerprefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("MaxVel", MaxVel);
        PlayerPrefs.SetInt("Div", Div);
        PlayerPrefs.SetFloat("VarVel", VarVel);
        PlayerPrefs.SetInt("tiempo", CanvasOpciones.tiempo);
        PlayerPrefs.SetInt("vida", CanvasOpciones.live);
        PlayerPrefs.SetFloat("sen", CanvasOpciones.sens);
        PlayerPrefs.SetInt("tiempoExt", CanvasOpciones.tiempoExt);
        PlayerPrefs.SetInt("vidaExt", CanvasOpciones.liveExt);
    }
}

