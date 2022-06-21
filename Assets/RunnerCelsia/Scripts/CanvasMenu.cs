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

    private void Start()
    {
        textoTiempo.text = "Tiempo: " + (CanvasController.tiempo).ToString("00");
        textoPuntaje.text = "Puntaje: " + (CanvasController.Score).ToString("00");
        textoVida.text = "Vidas: " + (CanvasController.vida).ToString("00");

    }

    private void Update()
    {
        if(Input.anyKey)
            SceneManager.LoadScene(2);

    }

    public void Comenzar()
    {
        SceneManager.LoadScene(2);

    }

}
