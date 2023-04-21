using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class MovPlayer : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    public PatternSystem patternSystem;
    public Transform wheel1;
    public Transform wheel2;
    public Transform wheel3;
    public Transform wheel4;
    public Transform cam;
    PostProcessProfile post;
    GameObject player;

    public AudioSource arranque;
    public AudioSource loopcar;
    bool loopaudiocar = false;

    public static bool stop = true;
    public static bool countdownover = false;

    public static float speed = 5;

    Vector3 rot = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 5;
        stop = true;
        cam.transform.position = new Vector3(cam.position.x, cam.position.y, player.transform.position.z - 3.57f);
        Debug.Log("La Velocidad Maxima es " + CanvasOpciones2.MaxVel);
        Debug.Log("La Velocidad Varible es " + CanvasOpciones2.VarVel);

    }

    // Update is called once per frame
    void Update()
    {
        if (patternSystem.loadingComplete && countdownover)
        {
            //Loopaudiocar();
            if (stop)
            {
                if (speed < CanvasOpciones2.MaxVel)
                {
                    speed += (Time.deltaTime * CanvasOpciones2.VarVel);

                    /*if (speed <= 5)
                    {
                        arranque.Stop();
                        loopcar.Stop();
                        loopaudiocar = false;
                    }*/
                }
                else
                {
                    speed = 10;
                }
            }
            //print(speed + "Velocity");
            m_Rigidbody.velocity = new Vector3(0, 0, speed);
            cam.transform.position = new Vector3(cam.position.x, cam.position.y, player.transform.position.z - 3.57f);
            rot.z += (speed*6);
            rot.y = 90;
            if (wheel1) wheel1.eulerAngles    = rot;
            if (wheel2) wheel2.eulerAngles    = -rot;
            if (wheel3) wheel3.eulerAngles    = rot;
            if (wheel4) wheel4.eulerAngles = -rot;
        }

        if (Input.GetKey(KeyCode.R))
        {
            MovPlayer.countdownover = false;
            SceneManager.LoadScene(1);
        }
        if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2"))
        {
            MovPlayer.countdownover = false;
            SceneManager.LoadScene(1);
        }
    }

    /*public void Loopaudiocar()
    {
        if (!loopaudiocar)
        {
            arranque.Stop();
            loopcar.Stop();
            StartCoroutine(Loopaudio());
            loopaudiocar = true;
        }
    }*/

    /*public IEnumerator Loopaudio()
    {
        arranque.Play();
        yield return new WaitForSeconds(9);
        loopcar.Play();
    }*/
}
