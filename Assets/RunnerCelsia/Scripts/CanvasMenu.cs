using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasMenu : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoPuntaje;
    public TextMeshProUGUI textoVida;

    int countRecords;
    bool usernotexist = true;

    private void Start()
    {
        textoTiempo.text = "Tiempo: " + (CanvasController.tiempo).ToString("00");
        textoPuntaje.text = "Puntaje: " + (CanvasController.Score).ToString("00");
        textoVida.text = "Vidas: " + (CanvasController.vida).ToString("00");

        Debug.Log("Nombre:" + CanvasForm.tNombre.ToString());
        Debug.Log("Cedula:" + CanvasForm.tCedula.ToString());
        Debug.Log("Correo:" + CanvasForm.tCorreo.ToString());

        if (!PlayerPrefs.HasKey("count"))
        {
            countRecords = 1;
            Debug.Log("countRecords:" + countRecords);
            PlayerPrefs.SetInt("count", countRecords);

            PlayerPrefs.SetString("Nombre" + countRecords.ToString(), CanvasForm.tNombre);
            PlayerPrefs.SetString("Cedula" + countRecords.ToString(), CanvasForm.tCedula.ToString());
            PlayerPrefs.SetString("Correo" + countRecords.ToString(), CanvasForm.tCorreo);
            PlayerPrefs.SetFloat("Score" + countRecords.ToString(), CanvasController.Score);

            for (int i = 1; i <= countRecords; i++)
            {
                Debug.Log(PlayerPrefs.GetString("Nombre" + i) + " - " + PlayerPrefs.GetString("Cedula" + i) + " - " + PlayerPrefs.GetFloat("Score" + i));
            }
        }
        else
        {
            countRecords = PlayerPrefs.GetInt("count");

            for (int j = 1; j <= countRecords; j++)
            {
                if (PlayerPrefs.HasKey("Cedula" + j))
                {
                    if (PlayerPrefs.GetString("Cedula" + j) == CanvasForm.tCedula.ToString())
                    {
                        usernotexist = false;
                        Debug.Log("usernotexist: " + usernotexist);
                        if (PlayerPrefs.GetFloat("Score" + j) < CanvasController.Score)
                        {
                            PlayerPrefs.SetFloat("Score" + j, CanvasController.Score);
                            Debug.Log("user change record to: " + CanvasController.Score);
                        }
                    }
                }
            }

            if (usernotexist)
            {
                countRecords++;
                PlayerPrefs.SetInt("count", countRecords);
                Debug.Log("countRecords:" + countRecords);

                PlayerPrefs.SetString("Nombre" + countRecords.ToString(), CanvasForm.tNombre);
                PlayerPrefs.SetString("Cedula" + countRecords.ToString(), CanvasForm.tCedula.ToString());
                PlayerPrefs.SetString("Correo" + countRecords.ToString(), CanvasForm.tCorreo);
                PlayerPrefs.SetFloat("Score" + countRecords.ToString(), CanvasController.Score);
            }

            for (int i = 1; i <= countRecords; i++)
            {
                Debug.Log(PlayerPrefs.GetString("Nombre" + i) + " - " + PlayerPrefs.GetString("Cedula" + i) + " - " + PlayerPrefs.GetFloat("Score" + i));
            }
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            Comenzar();
    }

    public void Comenzar()
    {

        SceneManager.LoadScene(3);

    }

}
