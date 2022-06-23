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

    public static int tiempo = 180;
    public static int live = 3;
    public static float sens = 1;

    // Start is called before the first frame update
    void Start()
    {
        //tiempoLimite = gameObject.GetComponentInChildren<TMP_InputField>();

        tiempoLimite.text = PlayerPrefs.GetInt("tiempo", 180).ToString();
        liveMax.text = PlayerPrefs.GetInt("vida", 5).ToString();
        Sen.value = PlayerPrefs.GetFloat("Sen");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Comenzar();

        sensText.text = Sen.value.ToString("0.00");
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
        int.TryParse(tiempoLimite.text, out tiempo);
        int.TryParse(liveMax.text, out live);
        PlayerPrefs.SetInt("tiempo", tiempo);
        PlayerPrefs.SetInt("vida", live);
        PlayerPrefs.SetFloat("sen", Sen.value);
        sens = Sen.value;
        SceneManager.LoadScene(3);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
