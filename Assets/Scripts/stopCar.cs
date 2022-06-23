using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopCar : MonoBehaviour
{
    GameObject[] player;
    float timer;
    bool enter;
    public bool critical;
    public bool score;
    public bool scoreN;
    ParticleSystem particulaCarga;
    public AudioSource audioE;
    public bool noItem;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        particulaCarga = GameObject.FindGameObjectWithTag("ParticulaCarga").GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !noItem)
        {
            //other.GetComponent<MovPlayer>().stop = false;
            MovPlayer.stop = false;
            print("esta Frenando");
            enter = true;
            if (score)
            {
                CanvasController.Score += 200;
                particulaCarga.Play();
                audioE.Play();
            }
            if (scoreN)
            {
                CanvasController.Score -= 300;
            }
            if (critical)
            {
                CanvasController.vida -= 1;
                MovPlayer.speed = 0;
                MovPlayer.stop = false;
            }
            print(MovPlayer.stop + " Entro");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !noItem)
        {
            if(MovPlayer.speed > 0.5f)
            {
                MovPlayer.speed -= (Time.deltaTime / 15) * 2;
            }
            else
            {
                MovPlayer.speed = 0;
            }
            if (critical)
            {
                MovPlayer.speed = 0;
                MovPlayer.stop = false;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !scoreN && !score && critical) 
        {
            MovPlayer.stop = true;
            MovPlayer.speed = 5;
            MovPlayer.stop = false;

        }
    }


    void Update()
    {
        if (enter)
        {
            if (MovPlayer.stop == false)
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    MovPlayer.stop = true;
                    timer = 0;
                    enter = false;
                }
            }
        } 
    }

}
