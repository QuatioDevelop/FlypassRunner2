using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasRank : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoPuntaje;
    public TextMeshProUGUI textoVida;

    public TextMeshProUGUI n1;
    public TextMeshProUGUI n2;
    public TextMeshProUGUI n3;
    public TextMeshProUGUI n4;
    public TextMeshProUGUI n5;

    public TextMeshProUGUI n12;
    public TextMeshProUGUI n22;
    public TextMeshProUGUI n32;
    public TextMeshProUGUI n42;
    public TextMeshProUGUI n52;

    public string[] stringnames;
    public string[] sortstringnames;

    public string[] stringscores;
    public string[] sortstringscores;

    public float[] scores;
    public float[] sortscores;

    int countRecords;
    bool usernotexist = true;

    private void Start()
    {
        Showranquing();
    }

    public void Showranquing()
    {
        countRecords = PlayerPrefs.GetInt("count");

        stringnames = new string[countRecords];
        sortstringnames = new string[countRecords];

        stringscores = new string[countRecords];
        sortstringscores = new string[countRecords];

        scores = new float[countRecords];
        sortscores = new float[countRecords];

        for (int i = 1; i <= countRecords; i++)
        {
            scores[i - 1] = PlayerPrefs.GetFloat("Score" + i.ToString());
            stringscores[i - 1] = PlayerPrefs.GetFloat("Score" + i.ToString()).ToString("00");
            stringnames[i - 1] = " " + PlayerPrefs.GetString("Nombre" + i.ToString());
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

                    sortstringnames[i] = stringnames[s];
                    stringnames[s] = "";
                }
            }
        }

        n1.text += sortstringnames[0];
        n2.text += sortstringnames[1];
        n3.text += sortstringnames[2];
        n4.text += sortstringnames[3];
        n5.text += sortstringnames[4];

        n12.text += sortstringscores[0];
        n22.text += sortstringscores[1];
        n32.text += sortstringscores[2];
        n42.text += sortstringscores[3];
        n52.text += sortstringscores[4];

    }

    public void Siguiente()
    {
        SceneManager.LoadScene(0);
    }
}
