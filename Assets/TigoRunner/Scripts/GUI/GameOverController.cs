using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameObject PanelGameOver;
    public GameObject FinalPanel;
    public GameObject Player_1;
    public GameObject Player_2;

    private bool _ended = false;

    void Update()
    {
        /*if (GameAttribute.gameAttribute.life <= 0)
        {
            PanelGameOver.SetActive(true);
        }*/

        if (GameAttribute.gameAttribute.isTimeOver && !_ended)
        {
            _ended = true;
            PanelGameOver.SetActive(true);
            if(GameAttribute.gameAttribute.distance * GameAttribute.gameAttribute.coin > GameAttribute.gameAttribute.distanceP2 * GameAttribute.gameAttribute.coinP2)
            {
                Player_1.SetActive(true);
            }
            else
            {
                Player_2.SetActive(true);
            }

            Invoke("ShowFinalPanel", 4);

        }

        
    }

    public void ShowFinalPanel()
    {
        Player_1.SetActive(false);
        Player_2.SetActive(false);
        FinalPanel.SetActive(true);
    }

    public void ResetGame()
    {
        /*PanelGameOver.SetActive(false);
        GameController.instance.ResetGame();
        GameAttribute.gameAttribute.Reset();*/
        GameController.instance.StopCamera();
        Application.LoadLevel("Gameplay");
    }
}
