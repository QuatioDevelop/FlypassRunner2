using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class CanvasForm : MonoBehaviour
{
    public TMP_InputField nombre;
    public TMP_InputField cedula;
    public TMP_InputField correo;
    public TextMeshProUGUI alerta;

    public static string tNombre;
    public static string tCedula;
    public static string tCorreo;

    public void Comenzar()
    {
        if (nombre.text != "" && cedula.text != "" && correo.text != "")
        {
            tNombre = nombre.text;
            tCedula = cedula.text;
            tCorreo = correo.text;
            SceneManager.LoadScene(2);
        }
        else
        {
            alerta.text = "Faltan campos por completar";
            StartCoroutine(CountDownAlerta());
        }
    }

    public IEnumerator CountDownAlerta()
    {
        yield return new WaitForSeconds(2);
        alerta.text = "";
    }
}
