using UnityEngine;
using System.Collections;

public enum Player
{
    Player1,
    Player2
}

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AnimationManager))]
public class Controller : MonoBehaviour
{
    

    public enum DirectionInput
    {
        Null, Left, Right
    }

    public enum Position
    {
        Left, Right
    }

    public Player PlayerNumber;
    public CoinRotation coinRotate;
    public GameObject magnet;
    public float speedMove = 5;
    public float gravity;

    [HideInInspector]
    public bool isRoll;
    [HideInInspector]
    public bool isMultiply;
    [HideInInspector]
    public CharacterController characterController;

    [HideInInspector]
    public float timeSprint;
    [HideInInspector]
    public float timeMagnet;
    [HideInInspector]
    public float timeMultiply;

    private bool activeInput;

    private Vector3 moveDir;
    private Vector2 currentPos;

    public bool opticalFlow;
    public bool keyInput;
    public bool touchInput;

    private Position positionStand;
    private DirectionInput directInput;
    private AnimatorManager animatorManager;
    private OpticalFlow of;
    private int cameraWidth = 640;
    private int cameraHeight = 480;

    private Vector3 previous;
    public float velocity;

    public delegate void TakeItemAction(Player player);

    public event TakeItemAction OnPowerUpSprintTaken;
    public event TakeItemAction OnPowerUpMagnetTaken;
    public event TakeItemAction OnPowerUpMultiplyTaken;

    public static Controller instance;

    private float _x = 0;
    private float _x2 = 0.5f;
    private float _y = 0;
    private float _y2 = 0;
    private float _width = 0.5f;
    private float _width2 = 0.5f;
    private float _height = 1;
    private float _height2 = 1;
    private float _threshold = 1.2f;

    private float x1;
    private float y1;
    private float w1;
    private float h1;
                    
    private float x2;
    private float y2;
    private float w2;
    private float h2;

    //Check item collider
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            if (col.GetComponent<Item>().useAbsorb)
            {
                col.GetComponent<Item>().useAbsorb = false;
                col.GetComponent<Item>().StopAllCoroutines();
            }
            if (!col.GetComponent<Item>().collides.Contains(this))
            {
                col.GetComponent<Item>().collides.Add(this);
                col.GetComponent<Item>().ItemGet(this);
                col.GetComponent<Item>().ChangeLayer(PlayerNumber == Player.Player1 ? "Item_P2" : "Item_P1");
            }
        }
    }

    void Start()
    {
        if (opticalFlow)
        {
            of = GameObject.FindObjectOfType<OpticalFlow>();
            cameraWidth = of.cameraWidth;
            cameraHeight = of.cameraHeight;
        }

        if (PlayerPrefs.HasKey("_x"))
            _x = PlayerPrefs.GetFloat("_x");
        if (PlayerPrefs.HasKey("_x2"))
            _x2 = PlayerPrefs.GetFloat("_x2");
        if (PlayerPrefs.HasKey("_y"))
            _y = PlayerPrefs.GetFloat("_y");
        if (PlayerPrefs.HasKey("_y2"))
            _y2 = PlayerPrefs.GetFloat("_y2");
        if (PlayerPrefs.HasKey("_width"))
            _width = PlayerPrefs.GetFloat("_width");
        if (PlayerPrefs.HasKey("_width2"))
            _width2 = PlayerPrefs.GetFloat("_width2");
        if (PlayerPrefs.HasKey("_height"))
            _height = PlayerPrefs.GetFloat("_height");
        if (PlayerPrefs.HasKey("_height2"))
            _height2 = PlayerPrefs.GetFloat("_height2");
        if (PlayerPrefs.HasKey("_threshold"))
            _threshold = PlayerPrefs.GetFloat("_threshold");

        x1 = cameraWidth - _x * cameraWidth - _width * cameraWidth;
        //y1 = _y * cameraHeight;
        y1 = cameraHeight - cameraHeight * _y - cameraHeight * _height;
        w1 = _width * cameraWidth;
        h1 = _height * cameraHeight;

        x2 = cameraWidth - _x2 * cameraWidth - _width2 * cameraWidth;
        //y2 = _y2 * cameraHeight;
        y2 = cameraHeight - cameraHeight * _y2 - cameraHeight * _height2;
        w2 = _width2 * cameraWidth;
        h2 = _height2 * cameraHeight;

        //Set state character
        instance = this;
        characterController = GetComponent<CharacterController>();
        animatorManager = GetComponent<AnimatorManager>();
        speedMove = GameAttribute.gameAttribute.speed;
        magnet.SetActive(false);
        Invoke("WaitStart", 0.2f);
    }

    //Reset state,variable when character die
    public void Reset()
    {
        transform.position = new Vector3(-1.5f, transform.position.y, -5);
        animatorManager.Reset();
        positionStand = Position.Left;
        isRoll = false;
        isMultiply = false;
        magnet.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(UpdateAction());
    }

    public void WaitStart()
    {
        //print("WaitStart");
        StartCoroutine(UpdateAction());
    }

    //Update Loop
    IEnumerator UpdateAction()
    {
        while ((GameAttribute.gameAttribute.life > 0 && PlayerNumber.Equals(Player.Player1)) || (GameAttribute.gameAttribute.lifeP2 > 0 && PlayerNumber.Equals(Player.Player2)))
        {
             if (GameAttribute.gameAttribute.pause == false && GameAttribute.gameAttribute.isPlaying && PatternSystem.instance.loadingComplete)
            {
                if (keyInput)
                {
                    if (PlayerNumber == Player.Player1)
                    {
                        KeyInput();
                    }
                    else
                    {
                        KeyInputA();
                    }
                }

                if (opticalFlow && of.CameraStarted)
                    ProcessOpticalFlow();

                if (touchInput)
                {
                    DirectionAngleInput();
                }
                CheckLane();
                MoveForward();
            }
            yield return 0;
        }

        StartCoroutine(MoveBack());
        animatorManager.Dead();
        //GameController.instance.StartCoroutine(GameController.instance.ResetGame());
    }

    IEnumerator MoveBack()
    {
        float z = transform.position.z - 0.5f;
        bool complete = false;
        while (complete == false)
        {
            CalcVelocity();

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, z), 2 * Time.deltaTime);
            if ((transform.position.z - z) < 0.05f)
            {
                complete = true;
            }
            yield return 0;
        }

        yield return 0;
    }
    
    private void MoveForward()
    {
        speedMove = (PlayerNumber == Player.Player1) ? GameAttribute.gameAttribute.speed : GameAttribute.gameAttribute.speedP2;

        CalcVelocity();
        if (characterController.isGrounded)
        {
            moveDir = Vector3.zero;
        }
        moveDir.z = 0;
        moveDir += transform.TransformDirection(Vector3.forward * speedMove);
        moveDir.y -= gravity * Time.deltaTime;

        //print(moveDir + "    " + velocity);
        characterController.Move(moveDir * Time.deltaTime);
    }

    private void CalcVelocity()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
    }

    private void CheckLane()
    {
        if (positionStand == Position.Left)
        {
            if (directInput == DirectionInput.Right)
            {
                positionStand = Position.Right;
                //Play sfx when step
                SoundManager.instance.PlayingSound("Step");
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(-1.5f, transform.position.y, transform.position.z), 6 * Time.deltaTime);
            //print(transform.position);
        }
        else
        {
            if (directInput == DirectionInput.Left)
            {
                positionStand = Position.Left;
                //Play sfx when step
                SoundManager.instance.PlayingSound("Step");
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(1.5f, transform.position.y, transform.position.z), 6 * Time.deltaTime);
            //print(transform.position);
        }
    }

    private OpticalFlowDataStr opticalFlowDataStr;
    private void ProcessOpticalFlow()
    {

        /*
        _x = 0;
        _x2 = 0.5f;
        _y = 0;
        _y2 = 0;
        _width = 0.5f;
        _width2 = 0.5f;
        _height = 1;
        _height2 = 1;
        
         * * 
        x1 = cameraWidth - _x * cameraWidth - _width * cameraWidth;
        y1 = _y * cameraHeight;
        w1 = _width * cameraWidth;
        h1 = _height * cameraHeight;
         */

        if (PlayerNumber == Player.Player1)
        {
            //print(PlayerNumber + " x1:" + (int)x1 + " y1:" + (int)y1 + " w1:" + (int)w1 + " h1:" + (int)h1 + " _threshold:" + _threshold);
            opticalFlowDataStr = of.processOpticalFlow((int)x1, (int)y1, (int)w1, (int)h1, _threshold);
        }
        else
        {
            //print(PlayerNumber + " x2:" + (int)x2 + " y2:" + (int)y2 + " w2:" + (int)w2 + " h2:" + (int)h2 + " _threshold:" + _threshold);
            opticalFlowDataStr = of.processOpticalFlow((int)x2, (int)y2, (int)w2, (int)h2, _threshold);
        }

        /*if (PlayerNumber == Player.Player1)
            opticalFlowDataStr = of.processOpticalFlow(cameraWidth / 2, 0, cameraWidth / 2, cameraHeight, 1.2f);
        else
            opticalFlowDataStr = of.processOpticalFlow(0, 0, cameraWidth / 2, cameraHeight, 1.2f);*/

        //print(PlayerNumber + ":: " + opticalFlowDataStr.isActive + " ** " + opticalFlowDataStr.force + " ** " + opticalFlowDataStr.dir);

        if (opticalFlowDataStr.isActive)
        {
            activeInput = true;
        }

        if (activeInput)
        {
            if (Mathf.Abs(opticalFlowDataStr.dir) < 90)
            {
                directInput = DirectionInput.Left;
                activeInput = false;
            }
            else if (Mathf.Abs(opticalFlowDataStr.dir) > 90)
            {
                directInput = DirectionInput.Right;
                activeInput = false;
            }

        }
        else
        {
            directInput = DirectionInput.Null;
        }
    }

    //Key input method
    private void KeyInput()
    {
        if (Input.anyKeyDown)
        {
            activeInput = true;
        }

        if (activeInput)
        {
            if (Input.GetKey(KeyCode.A))
            {
                directInput = DirectionInput.Left;
                activeInput = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                directInput = DirectionInput.Right;
                activeInput = false;
            }

        }
        else
        {
            directInput = DirectionInput.Null;
        }
    }
    private void KeyInputA()
    {
        if (Input.anyKeyDown)
        {
            activeInput = true;
        }

        if (activeInput)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                directInput = DirectionInput.Left;
                activeInput = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                directInput = DirectionInput.Right;
                activeInput = false;
            }

        }
        else
        {
            directInput = DirectionInput.Null;
        }
    }

    //Touch input method
    private void TouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPos = Input.mousePosition;
            activeInput = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (activeInput)
            {
                if ((Input.mousePosition.x - currentPos.x) > 40)
                {
                    directInput = DirectionInput.Right;
                    activeInput = false;
                }
                else if ((Input.mousePosition.x - currentPos.x) < -40)
                {
                    directInput = DirectionInput.Left;
                    activeInput = false;
                }
            }
            else
            {
                directInput = DirectionInput.Null;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            directInput = DirectionInput.Null;
        }
        currentPos = Input.mousePosition;
    }

    private void DirectionAngleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPos = Input.mousePosition;
            activeInput = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (activeInput)
            {
                float ang = CalculateAngle.GetAngle(currentPos, Input.mousePosition);
                //Debug.Log(ang);
                if ((Input.mousePosition.x - currentPos.x) > 20)
                {
                    if (ang < 45 && ang > -45)
                    {
                        directInput = DirectionInput.Right;
                        activeInput = false;
                    }
                }
                else if ((Input.mousePosition.x - currentPos.x) < -20)
                {
                    if (ang < 45 && ang > -45)
                    {
                        directInput = DirectionInput.Left;
                        activeInput = false;
                    }
                }
                else if ((Input.mousePosition.y - currentPos.y) > 20)
                {
                    if ((Input.mousePosition.x - currentPos.x) > 0)
                    {
                        if (ang <= 45 && ang >= -45)
                        {
                            directInput = DirectionInput.Right;
                            activeInput = false;
                        }
                    }
                    else if ((Input.mousePosition.x - currentPos.x) < 0)
                    {
                        if (ang >= -45)
                        {
                            directInput = DirectionInput.Left;
                            activeInput = false;
                        }
                    }
                }
                else if ((Input.mousePosition.y - currentPos.y) < -20)
                {
                    if ((Input.mousePosition.x - currentPos.x) > 0)
                    {
                        if (ang >= -45)
                        {
                            directInput = DirectionInput.Right;
                            activeInput = false;
                        }
                    }
                    else if ((Input.mousePosition.x - currentPos.x) < 0)
                    {
                        if (ang <= 45)
                        {
                            directInput = DirectionInput.Left;
                            activeInput = false;
                        }
                    }

                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            directInput = DirectionInput.Null;
            activeInput = false;
        }

    }

    //Sprint Item
    public void Sprint(float speed, float time, Player player)
    {
        if (!PlayerNumber.Equals(player))
            return;

        StopCoroutine("CancelSprint");
        if(PlayerNumber.Equals(Player.Player1)) GameAttribute.gameAttribute.speed = speed; else GameAttribute.gameAttribute.speedP2 = speed;
        timeSprint = time;
        if (OnPowerUpSprintTaken != null)
        {
            OnPowerUpSprintTaken(PlayerNumber);
        }
        StartCoroutine(CancelSprint());
    }

    IEnumerator CancelSprint()
    {
        while (timeSprint > 0)
        {
            timeSprint -= 1 * Time.deltaTime;
            yield return 0;
        }
        int i = 0;
        if (PlayerNumber.Equals(Player.Player1)) GameAttribute.gameAttribute.speed = GameAttribute.gameAttribute.starterSpeed; else GameAttribute.gameAttribute.speedP2 = GameAttribute.gameAttribute.starterSpeed;
        //GameAttribute.gameAttribute.speed = GameAttribute.gameAttribute.starterSpeed;
        while (i < GameController.instance.countAddSpeed + 1)
        {
            if (PlayerNumber.Equals(Player.Player1)) GameAttribute.gameAttribute.speed += GameController.instance.speedAdd; else GameAttribute.gameAttribute.speedP2 += GameController.instance.speedAdd;
            //GameAttribute.gameAttribute.speed += GameController.instance.speedAdd;
            i++;
        }
    }

    //Magnet Item
    public void Magnet(float time, Player player)
    {
        if (!PlayerNumber.Equals(player))
            return;

        StopCoroutine("CancelMagnet");
        magnet.SetActive(true);
        timeMagnet = time;
        if (OnPowerUpMagnetTaken != null)
        {
            OnPowerUpMagnetTaken(PlayerNumber);
        }
        StartCoroutine(CancelMagnet());
    }

    IEnumerator CancelMagnet()
    {

        while (timeMagnet > 0)
        {
            timeMagnet -= 1 * Time.deltaTime;
            yield return 0;
        }
        magnet.SetActive(false);
    }

    //Multiply Item
    public void Multiply(float time, Player player)
    {
        if (!PlayerNumber.Equals(player))
            return;

        StopCoroutine("CancelMultiply");
        isMultiply = true;
        timeMultiply = time;
        if (OnPowerUpMultiplyTaken != null)
        {
            OnPowerUpMultiplyTaken(PlayerNumber);
        }
        StartCoroutine(CancelMultiply());
    }

    IEnumerator CancelMultiply()
    {
        while (timeMultiply > 0)
        {
            timeMultiply -= 1 * Time.deltaTime;
            yield return 0;
        }
        isMultiply = false;
    }

    public void stopCamera()
    {
        if (opticalFlow)
            of.stop();
        else
            GameObject.FindObjectOfType<OpticalFlow>().stop();
    }
}
