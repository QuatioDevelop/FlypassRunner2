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

    public static bool anon = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(0);
    }

    public void Comenzar()
    {
        Debug.Log("Start");
        if (nombre.text != "" && cedula.text != "" && correo.text != "")
        {
            Debug.Log("Start 2");
            tNombre = nombre.text;
            tCedula = cedula.text;
            tCorreo = correo.text;
            anon = false;
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

    public void ComenzarAnonimo()
    {
        tNombre = "Anonimo";
        tCedula = "0000";
        tCorreo = "Anonimo";
        anon = true;
        SceneManager.LoadScene(2);
    }
}
