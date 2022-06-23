using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasRank : MonoBehaviour
{

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

    public TextMeshProUGUI n13;
    public TextMeshProUGUI n23;
    public TextMeshProUGUI n33;
    public TextMeshProUGUI n43;
    public TextMeshProUGUI n53;

    public TextMeshProUGUI n14;
    public TextMeshProUGUI n24;
    public TextMeshProUGUI n34;
    public TextMeshProUGUI n44;
    public TextMeshProUGUI n54;

    public string[] stringnames;
    public string[] sortstringnames;

    public string[] stringdocs;
    public string[] sortstringdocs;

    public string[] stringemails;
    public string[] sortstringemails;

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

        stringdocs = new string[countRecords];
        sortstringdocs = new string[countRecords];

        stringemails = new string[countRecords];
        sortstringemails = new string[countRecords];

        stringscores = new string[countRecords];
        sortstringscores = new string[countRecords];

        scores = new float[countRecords];
        sortscores = new float[countRecords];

        for (int i = 1; i <= countRecords; i++)
        {
            scores[i - 1] = PlayerPrefs.GetFloat("Score" + i.ToString());
            stringscores[i - 1] = PlayerPrefs.GetFloat("Score" + i.ToString()).ToString("00");
            stringnames[i - 1] = " " + PlayerPrefs.GetString("Nombre" + i.ToString());
            stringdocs[i - 1] = " " + PlayerPrefs.GetString("Cedula" + i.ToString());
            stringemails[i - 1] = " " + PlayerPrefs.GetString("Correo" + i.ToString());
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

                    sortstringdocs[i] = stringdocs[s];
                    stringdocs[s] = "";

                    sortstringemails[i] = stringemails[s];
                    stringemails[s] = "";
                }
            }
        }

        switch (sortstringnames.Length)
        {
            case 0:
                break;

            case 1:
                n1.text += sortstringnames[0];
                n12.text += sortstringscores[0];
                n13.text += sortstringdocs[0];
                n14.text += sortstringemails[0];
                break;

            case 2:
                n1.text += sortstringnames[0];
                n2.text += sortstringnames[1];
                n12.text += sortstringscores[0];
                n22.text += sortstringscores[1];
                n13.text += sortstringdocs[0];
                n23.text += sortstringdocs[1];
                n14.text += sortstringemails[0];
                n24.text += sortstringemails[1];
                break;

            case 3:
                n1.text += sortstringnames[0];
                n2.text += sortstringnames[1];
                n3.text += sortstringnames[2];
                n12.text += sortstringscores[0];
                n22.text += sortstringscores[1];
                n32.text += sortstringscores[2];
                n13.text += sortstringdocs[0];
                n23.text += sortstringdocs[1];
                n33.text += sortstringdocs[2];
                n14.text += sortstringemails[0];
                n24.text += sortstringemails[1];
                n34.text += sortstringemails[2];
                break;

            case 4:
                n1.text += sortstringnames[0];
                n2.text += sortstringnames[1];
                n3.text += sortstringnames[2];
                n4.text += sortstringnames[3];
                n12.text += sortstringscores[0];
                n22.text += sortstringscores[1];
                n32.text += sortstringscores[2];
                n42.text += sortstringscores[3];
                n13.text += sortstringdocs[0];
                n23.text += sortstringdocs[1];
                n33.text += sortstringdocs[2];
                n43.text += sortstringdocs[3];
                n14.text += sortstringemails[0];
                n24.text += sortstringemails[1];
                n34.text += sortstringemails[2];
                n44.text += sortstringemails[3];
                break;

            case 5:
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
                n13.text += sortstringdocs[0];
                n23.text += sortstringdocs[1];
                n33.text += sortstringdocs[2];
                n43.text += sortstringdocs[3];
                n53.text += sortstringdocs[4];
                n14.text += sortstringemails[0];
                n24.text += sortstringemails[1];
                n34.text += sortstringemails[2];
                n44.text += sortstringemails[3];
                n54.text += sortstringemails[4];
                break;
            default:
                break;
        }

        //n1.text += sortstringnames[0];
        //n2.text += sortstringnames[1];
        //n3.text += sortstringnames[2];
        //n4.text += sortstringnames[3];
        //n5.text += sortstringnames[4];
        //
        //n12.text += sortstringscores[0];
        //n22.text += sortstringscores[1];
        //n32.text += sortstringscores[2];
        //n42.text += sortstringscores[3];
        //n52.text += sortstringscores[4];
        //
        //n13.text += sortstringdocs[0];
        //n23.text += sortstringdocs[1];
        //n33.text += sortstringdocs[2];
        //n43.text += sortstringdocs[3];
        //n53.text += sortstringdocs[4];
        //
        //n14.text += sortstringemails[0];
        //n24.text += sortstringemails[1];
        //n34.text += sortstringemails[2];
        //n44.text += sortstringemails[3];
        //n54.text += sortstringemails[4];

    }

    public void Siguiente()
    {
        SceneManager.LoadScene(0);
    }
}
