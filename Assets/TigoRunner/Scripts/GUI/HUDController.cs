using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text Distance;
    public Text DistanceP2;
    public Text Coins;
    public Text CoinsP2;

    public GameObject SprintPowerUp;
    public GameObject SprintPowerUpP2;
    public GameObject MagnetPowerUp;
    public GameObject MagnetPowerUpP2;
    public GameObject MultiplyPowerUp;
    public GameObject MultiplyPowerUpP2;

    public Text SprintPowerUpText;
    public Text SprintPowerUpTextP2;
    public Text MagnetPowerUpText;
    public Text MagnetPowerUpTextP2;
    public Text MultiplyPowerUpText;
    public Text MultiplyPowerUpTextP2;

    private float _timeSprint;
    private float _timeSprintP2;
    private float _timeMultiply;
    private float _timeMultiplyP2;
    private float _timeMagnet;
    private float _timeMagnetP2;

    private Controller _controllerP1;
    private Controller _controllerP2;
    public Controller ControllerP1
    {
        set 
        { 
            _controllerP1 = value;
            _controllerP1.OnPowerUpSprintTaken += PowerUpSprintTaken;
            _controllerP1.OnPowerUpMagnetTaken += PowerUpMagnetTaken;
            _controllerP1.OnPowerUpMultiplyTaken += PowerUpMultiplyTaken;
        }
        get { return _controllerP1; }
    }

    public Controller ControllerP2
    {
        set
        {
            _controllerP2 = value;
            _controllerP2.OnPowerUpSprintTaken += PowerUpSprintTaken;
            _controllerP2.OnPowerUpMagnetTaken += PowerUpMagnetTaken;
            _controllerP2.OnPowerUpMultiplyTaken += PowerUpMultiplyTaken;
        }
        get { return _controllerP2; }
    }

    void OnEnable()
    {
        /*Controller.instance.OnPowerUpSprintTaken += PowerUpSprintTaken;
        Controller.instance.OnPowerUpMagnetTaken += PowerUpMagnetTaken;
        Controller.instance.OnPowerUpMultiplyTaken += PowerUpMultiplyTaken;*/
    }

    void OnDisable()
    {
        /*Controller.instance.OnPowerUpSprintTaken -= PowerUpSprintTaken;
        Controller.instance.OnPowerUpMagnetTaken -= PowerUpMagnetTaken;
        Controller.instance.OnPowerUpMultiplyTaken -= PowerUpMultiplyTaken;*/

        _controllerP1.OnPowerUpSprintTaken -= PowerUpSprintTaken;
        _controllerP1.OnPowerUpMagnetTaken -= PowerUpMagnetTaken;
        _controllerP1.OnPowerUpMultiplyTaken -= PowerUpMultiplyTaken;

        _controllerP2.OnPowerUpSprintTaken -= PowerUpSprintTaken;
        _controllerP2.OnPowerUpMagnetTaken -= PowerUpMagnetTaken;
        _controllerP2.OnPowerUpMultiplyTaken -= PowerUpMultiplyTaken;
    }

    private void PowerUpSprintTaken(Player player)
    {
        if (player.Equals(Player.Player1))
        {
            SprintPowerUp.SetActive(true);
            _timeSprint = ControllerP1.timeSprint;
            StartCoroutine(SprintTimer());
        }
        else
        {
            SprintPowerUpP2.SetActive(true);
            _timeSprintP2 = ControllerP2.timeSprint;
            StartCoroutine(SprintTimerP2());
        }
    }

    IEnumerator SprintTimer()
    {
        while (_timeSprint > 0)
        {
            _timeSprint -= Time.deltaTime;
            SprintPowerUpText.text = ((int)_timeSprint).ToString();
            yield return 0;
        }
        SprintPowerUp.SetActive(false);
    }

    IEnumerator SprintTimerP2()
    {
        while (_timeSprintP2 > 0)
        {
            _timeSprintP2 -= Time.deltaTime;
            SprintPowerUpTextP2.text = ((int)_timeSprintP2).ToString();
            yield return 0;
        }
        SprintPowerUpP2.SetActive(false);
    }

    private void PowerUpMagnetTaken(Player player)
    {
        if (player.Equals(Player.Player1))
        {
            MagnetPowerUp.SetActive(true);
            _timeMagnet = ControllerP1.timeMagnet;
            StartCoroutine(MagnetTimer());
        }
        else
        {
            MagnetPowerUpP2.SetActive(true);
            _timeMagnetP2 = ControllerP2.timeMagnet;
            StartCoroutine(MagnetTimerP2());
        }
    }

    IEnumerator MagnetTimer()
    {
        while (_timeMagnet > 0)
        {
            _timeMagnet -= Time.deltaTime;
            MagnetPowerUpText.text = ((int)_timeMagnet).ToString();
            yield return 0;
        }
        MagnetPowerUp.SetActive(false);
    }

    IEnumerator MagnetTimerP2()
    {
        while (_timeMagnetP2 > 0)
        {
            _timeMagnetP2 -= Time.deltaTime;
            MagnetPowerUpTextP2.text = ((int)_timeMagnetP2).ToString();
            yield return 0;
        }
        MagnetPowerUpP2.SetActive(false);
    }

    private void PowerUpMultiplyTaken(Player player)
    {
        if (player.Equals(Player.Player1))
        {
            MultiplyPowerUp.SetActive(true);
            _timeMultiply = ControllerP1.timeMultiply;
            StartCoroutine(MultiplyTimer());
        }
        else
        {
            MultiplyPowerUpP2.SetActive(true);
            _timeMultiplyP2 = ControllerP2.timeMultiply;
            StartCoroutine(MultiplyTimerP2());
        }
    }

    IEnumerator MultiplyTimer()
    {
        while (_timeMultiply > 0)
        {
            _timeMultiply -= Time.deltaTime;
            MultiplyPowerUpText.text = ((int)_timeMultiply).ToString();
            yield return 0;
        }
        MultiplyPowerUp.SetActive(false);
    }

    IEnumerator MultiplyTimerP2()
    {
        while (_timeMultiplyP2 > 0)
        {
            _timeMultiplyP2 -= Time.deltaTime;
            MultiplyPowerUpTextP2.text = ((int)_timeMultiplyP2).ToString();
            yield return 0;
        }
        MultiplyPowerUpP2.SetActive(false);
    }

    public void CleanHUD()
    {
        SprintPowerUp.SetActive(false);
        MagnetPowerUp.SetActive(false);
        MultiplyPowerUp.SetActive(false);
        SprintPowerUpP2.SetActive(false);
        MagnetPowerUpP2.SetActive(false);
        MultiplyPowerUpP2.SetActive(false);
    }

    void Update()
    {
        if (Distance)
        {
            Distance.text = ((int)GameAttribute.gameAttribute.distance).ToString();
        }
        if (Coins)
        {
            Coins.text = GameAttribute.gameAttribute.coin.ToString();
        }

        if (DistanceP2)
        {
            DistanceP2.text = ((int)GameAttribute.gameAttribute.distanceP2).ToString();
        }
        if (CoinsP2)
        {
            CoinsP2.text = GameAttribute.gameAttribute.coinP2.ToString();
        }
    }
}
