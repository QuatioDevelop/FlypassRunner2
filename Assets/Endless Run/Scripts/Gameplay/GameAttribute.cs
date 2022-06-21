// <summary>
// Game attribute.
// this script use for set all attribute in game(ex speedgame,character life)
// </summary>

using UnityEngine;

public class GameAttribute : MonoBehaviour
{

    public float starterSpeed = 5; //Speed Character
    public float starterLife = 1; //Life character

    [HideInInspector]
    public float distance;
    [HideInInspector]
    public float distanceP2;
    [HideInInspector]
    public float coin;
    [HideInInspector]
    public float coinP2;
    [HideInInspector]
    public int level = 0;
    [HideInInspector]
    public bool isPlaying;
    [HideInInspector]
    public bool isTimeOver = false;
    [HideInInspector]
    public bool pause = false;
    [HideInInspector]
    public bool ageless = false;
    [HideInInspector]
    public bool deleyDetect = false;
    [HideInInspector]
    public float multiplyValue;

    [HideInInspector]
    public float speed = 5;
    [HideInInspector]
    public float speedP2 = 5;
    [HideInInspector]
    public float life = 3;
    [HideInInspector]
    public float lifeP2 = 3;

    public static GameAttribute gameAttribute;

    void Start()
    {
        //Setup all attribute
        gameAttribute = this;
        DontDestroyOnLoad(this);
        speed = starterSpeed;
        speedP2 = starterSpeed;
        distance = 0;
        distanceP2 = 0;
        coin = 0;
        coinP2 = 0;
        life = starterLife;
        lifeP2 = starterLife;
        level = 0;
        pause = false;
        deleyDetect = false;
        ageless = false;
        isPlaying = true;
    }

    public void CountDistance(float amountCount)
    {
        distance += amountCount * Time.smoothDeltaTime;
    }

    public void ActiveShakeCamera()
    {
        CameraFollow.instance.ActiveShake();
    }

    public void Pause(bool isPause)
    {
        //pause varible
        pause = isPause;
    }

    public void Resume()
    {
        //resume
        pause = false;
    }

    public void Reset()
    {
        //Reset all attribute when character die
        speed = starterSpeed;
        speedP2 = starterSpeed;
        distance = 0;
        distanceP2 = 0;
        coin = 0;
        coinP2 = 0;
        life = starterLife;
        lifeP2 = starterLife;
        level = 0;
        pause = false;
        deleyDetect = false;
        ageless = false;
        isPlaying = true;
        Building.instance.Reset();
        Item.instance.Reset();
        PatternSystem.instance.Reset();
        /*CameraFollow.instance.Reset();
        Controller.instance.Reset();
        Controller.instance.timeMagnet = 0;
        Controller.instance.timeMultiply = 0;
        Controller.instance.timeSprint = 0;*/
    }
}
