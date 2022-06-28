using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveRank : MonoBehaviour
{
    [SerializeField]
    private RankData rankData;

    private string[] stringnames;
    private string[] sortstringnames;

    private string[] stringdocs;
    private string[] sortstringdocs;

    private string[] stringemails;
    private string[] sortstringemails;

    private string[] stringscores;
    private string[] sortstringscores;

    private float[] scores;
    private float[] sortscores;

    int countRecords;
    bool usernotexist = true;

    private void Start()
    {
        Calculateranquing();
    }

    public void Calculateranquing()
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
            stringscores[i - 1] = PlayerPrefs.GetFloat("Score" + i.ToString()).ToString();
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
    }

    public void save()
    {
        /*string datatosave = "";

        for (int i = 0; i < sortstringscores.Length; i++)
        {
            rankData.name = (i+1).ToString()+ "."+sortstringnames[i];
            rankData.document = sortstringdocs[i];
            rankData.score = float.Parse(sortstringscores[i]);
            rankData.email = sortstringemails[i];

            datatosave += JsonUtility.ToJson(rankData);
        }

        string path = Path.Combine(Application.persistentDataPath, "rank.csv");
        File.WriteAllText(path, datatosave);*/

        string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "RankingFlyPassRunner.csv");
        TextWriter tw = new StreamWriter(path, false);
        tw.WriteLine("#, Nombre, Documento, Puntos, Correo");

        for (int i = 0; i < sortstringscores.Length; i++)
        {
            tw.WriteLine((i + 1).ToString() + ".," + sortstringnames[i] + "," + sortstringdocs[i] + "," + float.Parse(sortstringscores[i]) + "," + sortstringemails[i]);
        }
        tw.Close();
    }
}
