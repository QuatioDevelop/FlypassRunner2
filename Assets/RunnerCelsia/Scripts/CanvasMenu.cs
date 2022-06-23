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

    public TextMeshProUGUI n1;
    public TextMeshProUGUI n2;
    public TextMeshProUGUI n3;
    public TextMeshProUGUI n4;
    public TextMeshProUGUI n5;

    //public List<string> stringscores = new List<string>();
    //public List<float> scores = new List<float>();
    //public List<int> index = new List<int>();

    public string[] stringscores;
    public string[] sortstringscores;
    public float[] scores;
    public float[] sortscores;

    int countRecords;
    bool usernotexist = true;

    private void Start()
    {
        textoTiempo.text = "Tiempo: " + (CanvasController.tiempo).ToString("00");
        textoPuntaje.text = "Puntaje: " + (CanvasController.Score).ToString("00");
        textoVida.text = "Vidas: " + (CanvasController.vida).ToString("00");

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

        Showranquing();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            Comenzar();
    }

    public void Showranquing()
    {
        countRecords = PlayerPrefs.GetInt("count");

        stringscores = new string[countRecords];
        sortstringscores = new string[countRecords];

        scores = new float[countRecords];
        sortscores = new float[countRecords];

        for (int i = 1; i <= countRecords; i++)
        {
            scores[i-1] = PlayerPrefs.GetFloat("Score" + i.ToString());
            stringscores[i-1] = " " + PlayerPrefs.GetString("Nombre" + i.ToString()) + " - " + PlayerPrefs.GetString("Cedula" + i.ToString()) + " - " + PlayerPrefs.GetFloat("Score" + i.ToString()).ToString("00");
        }

        for (int i = 0; i < scores.Length; i++)
        {
            float maxValue = Mathf.Max(scores);

            sortscores[i] = maxValue;

            for (int s = 0; s < scores.Length; s++)
            {
                if (scores[s] == maxValue)
                {
                    scores[s] = 0;
                    sortstringscores[i] = stringscores[s];
                    stringscores[s] = "";
                }
            }
        }

        n1.text += sortstringscores[0];
        n2.text += sortstringscores[1];
        n3.text += sortstringscores[2];
        n4.text += sortstringscores[3];
        n5.text += sortstringscores[4];
        
    }

    public void Comenzar()
    {

        SceneManager.LoadScene(3);

    }

}
