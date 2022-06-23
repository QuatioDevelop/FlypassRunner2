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

    public static int MaxVel = 50;
    public static float VarVel = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //tiempoLimite = gameObject.GetComponentInChildren<TMP_InputField>();
        MaxVelText.text = MaxVel.ToString();
        VarVelText.text = VarVel.ToString();
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
        SceneManager.LoadScene(0);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Clearplayerprefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

