using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoScript : MonoBehaviour
{
    CanvasController canvasController;
    ParticleSystem particulaCarga;
    ParticleSystem particulaDam;
    AudioSource audioCarga;
    AudioSource efect;
    AudioSource efectD;

    // Start is called before the first frame update
    void Start()
    {
        canvasController = GameObject.FindObjectOfType<CanvasController>();
        particulaCarga = GameObject.FindGameObjectWithTag("ParticulaCarga").GetComponent<ParticleSystem>();
        particulaDam = GameObject.FindGameObjectWithTag("ParticulaDamage").GetComponent<ParticleSystem>();
        efect = GameObject.FindGameObjectWithTag("EfectSound").GetComponent<AudioSource>();
        efectD = GameObject.FindGameObjectWithTag("DamageSound").GetComponent<AudioSource>();
        audioCarga = gameObject.GetComponentInChildren<AudioSource>();

        for (int i = 0; i < (tollgenerate.cantidad * 5); i++)
        {
            if ((this.transform.localPosition.z) >= ((160 * (i + 1)) - 35) && (this.transform.localPosition.z) <= ((160 * (i + 1)) + 35))
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(gameObject.tag == "Carga")
            {
                CanvasController.energia += 10;
                audioCarga.Play();
                particulaCarga.Play();
            }
            else if (gameObject.tag == "Moneda")
            {
                efect.Play();
                CanvasController.Score += 100;
                gameObject.SetActive(false);
                //Invoke("ActivarObjeto", 2);
                particulaCarga.Play();

            }
            else
            {
                MovPlayer.speed = 5;
                //CanvasController.energia -= 10;
                CanvasController.vida -= 1;
                gameObject.SetActive(false);
                Invoke("ActivarObjeto", 2);
                particulaDam.Play();
                //efectD.Play();

            }

        }
    }


    public void ActivarObjeto()
    {
        gameObject.SetActive(true);

    }
}
