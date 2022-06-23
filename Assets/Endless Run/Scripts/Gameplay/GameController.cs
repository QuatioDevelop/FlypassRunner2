using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GameController : MonoBehaviour
{
    public PatternSystem patSysm; //pattern system
    public CameraFollow cameraFol1;	//camera
    public CameraFollow cameraFol2;	//camera
    public float speedAddEveryDistance = 300;
    public float speedAdd = 0.5f;
    public float speedMax = 20;
    public GameObject playerPref1;
    public GameObject playerPref2;
    public Vector3 posStart;
    public bool previewProgressBar;
    public bool useShowPercent;
    public Rect rect_progressbar, rect_percent_text;
    [HideInInspector]
    public int countAddSpeed;
    public int countAddSpeedP2;
    public AudioSource Music;
    public GameObject HUD;
    public Text timerText;
    public static GameController instance;

    private bool _createGui;
    private float _percentCount;
    private float _distanceCheck;
    private float _distanceCheckP2;

    private float timer = 0;
    private bool isTiming = true;
    private float gameTime = 30;

    private Controller _controllerP1;
    private Controller _controllerP2;

    void Start()
    {
        if (PlayerPrefs.HasKey("_time"))
            gameTime = PlayerPrefs.GetInt("_time");


        if (Application.isPlaying)
        {
            instance = this;
            StartCoroutine(WaitLoading());
        }
    }

    void beginTimer()
    {
        timer = 0;
        isTiming = true;
    }

    //Loading method

    IEnumerator WaitLoading()
    {
        while (patSysm.loadingComplete == false)
        {
            yield return 0;
        }
        StartCoroutine(InitPlayer());
    }

    //Spawn player method
    IEnumerator InitPlayer()
    {
        GameObject go1 = (GameObject)Instantiate(playerPref1, posStart, Quaternion.identity);
        GameObject go2 = (GameObject)Instantiate(playerPref2, posStart, Quaternion.identity);
        cameraFol1.target = go1.transform;
        cameraFol2.target = go2.transform;
        _controllerP1 = go1.GetComponent<Controller>();
        _controllerP2 = go2.GetComponent<Controller>();
        HUD.GetComponent<HUDController>().ControllerP1 = _controllerP1;
        HUD.GetComponent<HUDController>().ControllerP2 = _controllerP2;
        Invoke("ShowHUD", 0.5f);
        yield return 0;
        StartCoroutine(UpdatePerDistance());
    }

    void ShowHUD()
    {
        HUD.SetActive(true);
    }

    //update distance score
    IEnumerator UpdatePerDistance()
    {
        while (true)
        {
            if (PatternSystem.instance.loadingComplete)
            {
                if (GameAttribute.gameAttribute.pause == false
                    && GameAttribute.gameAttribute.isPlaying == true
                    && GameAttribute.gameAttribute.life > 0)
                {
                    /*if (Controller.instance.transform.position.z > 0)
                    {
                        GameAttribute.gameAttribute.distance += GameAttribute.gameAttribute.speed * Time.deltaTime;
                        _distanceCheck += GameAttribute.gameAttribute.speed * Time.deltaTime;
                        if (_distanceCheck >= speedAddEveryDistance)
                        {
                            GameAttribute.gameAttribute.speed += speedAdd;
                            Music.pitch += 0.1f;
                            if (GameAttribute.gameAttribute.speed >= speedMax)
                            {
                                GameAttribute.gameAttribute.speed = speedMax;
                            }
                            countAddSpeed++;
                            _distanceCheck = 0;
                        }
                    }*/

                    if (_controllerP1.velocity > 1)
                    {
                        GameAttribute.gameAttribute.distance += GameAttribute.gameAttribute.speed * Time.deltaTime;
                        _distanceCheck += GameAttribute.gameAttribute.speed * Time.deltaTime;
                        if (_distanceCheck >= speedAddEveryDistance)
                        {
                            GameAttribute.gameAttribute.speed += speedAdd;
                            Music.pitch += 0.1f;
                            if (GameAttribute.gameAttribute.speed >= speedMax)
                            {
                                GameAttribute.gameAttribute.speed = speedMax;
                            }
                            countAddSpeed++;
                            _distanceCheck = 0;
                        }
                    }

                    
                }

                /*print("========================================================");
                print(_controllerP1.velocity);
                print(_controllerP2.velocity);*/

                if (GameAttribute.gameAttribute.pause == false
                    && GameAttribute.gameAttribute.isPlaying == true
                    && GameAttribute.gameAttribute.lifeP2 > 0)
                {
                    if (_controllerP2.velocity > 1)
                    {
                        GameAttribute.gameAttribute.distanceP2 += GameAttribute.gameAttribute.speedP2 * Time.deltaTime;
                        _distanceCheckP2 += GameAttribute.gameAttribute.speedP2 * Time.deltaTime;
                        if (_distanceCheckP2 >= speedAddEveryDistance)
                        {
                            GameAttribute.gameAttribute.speedP2 += speedAdd;
                            //Music.pitch += 0.1f;
                            if (GameAttribute.gameAttribute.speedP2 >= speedMax)
                            {
                                GameAttribute.gameAttribute.speedP2 = speedMax;
                            }
                            countAddSpeedP2++;
                            _distanceCheckP2 = 0;
                        }
                    }
                }

                if (isTiming 
                    && GameAttribute.gameAttribute.pause == false
                    && GameAttribute.gameAttribute.isPlaying == true)
                {
                    timer += Time.deltaTime;
                    //print(" time:::"+timer);
                }
                else if (GameAttribute.gameAttribute.pause == false
                    && GameAttribute.gameAttribute.isPlaying == true)
                {
                    //print(":::beginTimer:::");
                    beginTimer();
                }

                timerText.text = (gameTime-timer).ToString("0:00.0");

                if (timer > gameTime)
                {
                    //print("Game Over!!");
                    GameAttribute.gameAttribute.isPlaying = false;
                    GameAttribute.gameAttribute.isTimeOver = true;
                }

            }
            yield return 0;
        }
    }

    public void StopCamera()
    {
        _controllerP1.stopCamera();
        _controllerP2.stopCamera();
    }
    //reset game
    public void ResetGame()
    {
        GameAttribute.gameAttribute.isPlaying = false;
        GameAttribute.gameAttribute.isTimeOver = false;
        isTiming = false;
        timer = 0;
        _distanceCheck = 0;
        _distanceCheckP2 = 0;
        countAddSpeed = 0;
        countAddSpeedP2 = 0;
        Music.pitch = 0.8f;
        HUD.GetComponent<HUDController>().CleanHUD();
        PatternSystem.instance.Reset();
        cameraFol1.Reset();
        cameraFol2.Reset();
        _controllerP1.Reset();
        _controllerP1.timeMagnet = 0;
        _controllerP1.timeMultiply = 0;
        _controllerP1.timeSprint = 0;
        _controllerP2.Reset();
        _controllerP2.timeMagnet = 0;
        _controllerP2.timeMultiply = 0;
        _controllerP2.timeSprint = 0;
        //yield return 0;
    }

}
