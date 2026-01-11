/// <summary>
/// GUI manager
/// this script use to control all GUI in game
/// </summary>

using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GUIManager : MonoBehaviour
{
    public enum ButtonType
    {
        pause, resume, exit, restart, sumRestart, sumExit
    }


    [System.Serializable]
    public class RectGroup
    {
        public string name;
        public float x, y, sizeX, sizeY;
    }

    //GUI Item state(ex double jump state, multiply state)
    [System.Serializable]
    public class GroupGUITexture
    {
        public string name;
        public Item.TypeItem itemType;
        public Image guiImage;
        public Text guiText;
        public Color colorText;
        public float x, y, sizeX, sizeY;
    }

    //GUI Buton(ex. pause button , resume button)
    [System.Serializable]
    public class GroupGUIButton
    {
        public string name;
        public ButtonType buttonType;
        public Image guiImage;
        public Button guiButton;
        public Sprite buttonNormal;
        public Sprite buttonActive;
        public float x, y, sizeX, sizeY;
    }


    //GUI Score(ex. distance score,coin)
    [System.Serializable]
    public class GroupGUIScore
    {
        public string name;
        public Image guiImage;
        public float x, y, sizeX, sizeY;
    }



    public bool isPreview; //set to preview GUI
    public Text[] guiText;
    public List<RectGroup> positionSet = new List<RectGroup>();
    public List<GroupGUITexture> itemStateSet = new List<GroupGUITexture>();
    public List<GroupGUIButton> menuButtonSet = new List<GroupGUIButton>();
    public List<GroupGUIScore> scoreSet = new List<GroupGUIScore>();
    public CalOnGUI calOnGUI;
    private bool createGUI;

    public static GUIManager instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

        //Set to preview GUI in editor
        if (Application.isPlaying == false)
        {
            if (calOnGUI == null && createGUI == false)
            {
                //calOnGUI =  //new CalOnGUI();
                createGUI = true;
            }
            for (int i = 0; i < itemStateSet.Count; i++)
            {
                if (isPreview)
                {
                    itemStateSet[i].guiImage.enabled = true;
                    itemStateSet[i].guiText.gameObject.SetActive(true);
                    if (itemStateSet[i].itemType == Item.TypeItem.ItemMagnet)
                    {
                        ShowGUI(i, 1);
                    }
                    if (itemStateSet[i].itemType == Item.TypeItem.ItemMultiply)
                    {
                        ShowGUI(i, 1);
                    }
                    if (itemStateSet[i].itemType == Item.TypeItem.ItemSprint)
                    {
                        ShowGUI(i, 1);
                    }
                }
                else
                {
                    itemStateSet[i].guiImage.enabled = false;
                    itemStateSet[i].guiText.gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < menuButtonSet.Count; i++)
            {
                if (isPreview)
                {
                    menuButtonSet[i].guiImage.enabled = true;
                    ShowGUIButton(i);
                }
                else
                {
                    menuButtonSet[i].guiImage.enabled = false;
                }
            }

            for (int i = 0; i < scoreSet.Count; i++)
            {
                if (isPreview)
                {
                    scoreSet[i].guiImage.enabled = true;
                    ShowGUIScore(i);
                }
                else
                {
                    scoreSet[i].guiImage.enabled = false;
                }
            }

        }
        else
        {
            if (PatternSystem.instance.loadingComplete == true)
            {
                for (int i = 0; i < guiText.Length; i++)
                {
                    guiText[i].gameObject.SetActive(true);
                    guiText[i].rectTransform.anchoredPosition = new Vector2(GUI_Calculate.RectWithScrren_WidthAndHeight_Sizeheight(new Vector2(positionSet[i].x, positionSet[i].y),
                                                                                                                new Vector2(positionSet[i].sizeX, positionSet[i].sizeY)).x,
                                                GUI_Calculate.RectWithScrren_WidthAndHeight_Sizeheight(new Vector2(positionSet[i].x, positionSet[i].y),
                                                                                                                new Vector2(positionSet[i].sizeX, positionSet[i].sizeY)).y);
                    guiText[i].fontSize = GUI_Calculate.FontSize((int)positionSet[i].sizeX);
                    if (positionSet[i].name == "Distance")
                    {
                        guiText[i].text = "" + (int)GameAttribute.gameAttribute.distance;
                    }
                    else if (positionSet[i].name == "Coin")
                    {
                        guiText[i].text = "" + (int)GameAttribute.gameAttribute.coin;
                    }
                }

                //RectWithScrren_WidthAndHeight_Sizeheight

                for (int i = 0; i < scoreSet.Count; i++)
                {
                    scoreSet[i].guiImage.enabled = true;
                    ShowGUIScore(i);
                }

                try
                {
                    for (int i = 0; i < itemStateSet.Count; i++)
                    {
                        if (itemStateSet[i].itemType == Item.TypeItem.ItemMagnet)
                        {
                            if (Controller.instance.timeMagnet > 0)
                            {
                                itemStateSet[i].guiImage.enabled = true;
                                itemStateSet[i].guiText.gameObject.SetActive(true);
                                ShowGUI(i, Controller.instance.timeMagnet);
                            }
                            else
                            {
                                itemStateSet[i].guiImage.enabled = false;
                                itemStateSet[i].guiText.gameObject.SetActive(false);
                            }
                        }
                        if (itemStateSet[i].itemType == Item.TypeItem.ItemMultiply)
                        {
                            if (Controller.instance.timeMultiply > 0)
                            {
                                itemStateSet[i].guiImage.enabled = true;
                                itemStateSet[i].guiText.gameObject.SetActive(true);
                                ShowGUI(i, Controller.instance.timeMultiply);
                            }
                            else
                            {
                                itemStateSet[i].guiImage.enabled = false;
                                itemStateSet[i].guiText.gameObject.SetActive(false);
                            }
                        }
                        if (itemStateSet[i].itemType == Item.TypeItem.ItemSprint)
                        {
                            if (Controller.instance.timeSprint > 0)
                            {
                                itemStateSet[i].guiImage.enabled = true;
                                itemStateSet[i].guiText.gameObject.SetActive(true);
                                ShowGUI(i, Controller.instance.timeSprint);
                            }
                            else
                            {
                                itemStateSet[i].guiImage.enabled = false;
                                itemStateSet[i].guiText.gameObject.SetActive(false);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }

                for (int i = 0; i < menuButtonSet.Count; i++)
                {
                    ShowGUIButton(i);
                    CheckTypeButtonActive(i);
                    // La lógica de clic ahora se maneja con el evento onClick del Button
                }
            }
            else
            {
                for (int i = 0; i < guiText.Length; i++)
                {
                    guiText[i].text = "";
                }
            }
        }

    }

    //Check hide/active button
    private void CheckTypeButtonActive(int i)
    {
        if (menuButtonSet[i].buttonType == ButtonType.exit)
        {
            if (GameAttribute.gameAttribute.pause == true)
            {
                menuButtonSet[i].guiImage.enabled = true;
            }
            else
            {
                menuButtonSet[i].guiImage.enabled = false;

            }
        }

        if (menuButtonSet[i].buttonType == ButtonType.pause)
        {
            if (GameAttribute.gameAttribute.pause == false)
            {
                menuButtonSet[i].guiImage.enabled = true;
            }
            else
            {
                menuButtonSet[i].guiImage.enabled = false;
            }
        }

        if (menuButtonSet[i].buttonType == ButtonType.restart)
        {
            if (GameAttribute.gameAttribute.pause == true)
            {
                menuButtonSet[i].guiImage.enabled = true;
            }
            else
            {
                menuButtonSet[i].guiImage.enabled = false;
            }
        }

        if (menuButtonSet[i].buttonType == ButtonType.resume)
        {
            if (GameAttribute.gameAttribute.pause == true)
            {
                menuButtonSet[i].guiImage.enabled = true;
            }
            else
            {
                menuButtonSet[i].guiImage.enabled = false;
            }
        }

        if (menuButtonSet[i].buttonType == ButtonType.sumRestart)
        {
            if (GameAttribute.gameAttribute.life <= 0)
            {
                menuButtonSet[i].guiImage.enabled = true;
            }
            else
            {
                menuButtonSet[i].guiImage.enabled = false;
            }
        }

        if (menuButtonSet[i].buttonType == ButtonType.sumExit)
        {
            if (GameAttribute.gameAttribute.life <= 0)
            {
                menuButtonSet[i].guiImage.enabled = true;
            }
            else
            {
                menuButtonSet[i].guiImage.enabled = false;
            }
        }
    }

    //This method use to input command in button
    private void CheckTypeButtonAction(int i)
    {
        if (menuButtonSet[i].guiImage.enabled == true)
        {
            //exit button back to title
            if (menuButtonSet[i].buttonType == ButtonType.exit)
            {
                Application.LoadLevel("TitleScene");
            }

            //pause button
            if (menuButtonSet[i].buttonType == ButtonType.pause)
            {
                if (GameAttribute.gameAttribute.life > 0)
                {
                    GameAttribute.gameAttribute.Pause(true);
                }
            }

            //restart button
            if (menuButtonSet[i].buttonType == ButtonType.restart)
            {
                GameAttribute.gameAttribute.Reset();
            }

            //resume button
            if (menuButtonSet[i].buttonType == ButtonType.resume)
            {
                GameAttribute.gameAttribute.Pause(false);
            }

            //restar button in summary screeen
            if (menuButtonSet[i].buttonType == ButtonType.sumRestart)
            {
                GameAttribute.gameAttribute.Reset();
            }

            //exit button in summary screen
            if (menuButtonSet[i].buttonType == ButtonType.sumExit)
            {
                Application.LoadLevel("TitleScene");
            }
        }

    }

    private void ShowGUI(int i, float time)
    {
        if (itemStateSet[i].guiImage != null)
        {
            // Puedes ajustar la posición usando rectTransform
            itemStateSet[i].guiImage.rectTransform.anchoredPosition = new Vector2(itemStateSet[i].x, itemStateSet[i].y);
            itemStateSet[i].guiImage.rectTransform.sizeDelta = new Vector2(itemStateSet[i].sizeX, itemStateSet[i].sizeY);
        }
        if (itemStateSet[i].guiText != null)
        {
            itemStateSet[i].guiText.color = itemStateSet[i].colorText;
            itemStateSet[i].guiText.rectTransform.anchoredPosition = new Vector2(itemStateSet[i].x, itemStateSet[i].y);
            itemStateSet[i].guiText.text = time.ToString("0") + "s";
        }
    }
    private void ShowGUIButton(int i)
    {
        if (menuButtonSet[i].guiImage != null)
        {
            menuButtonSet[i].guiImage.rectTransform.anchoredPosition = new Vector2(menuButtonSet[i].x, menuButtonSet[i].y);
            menuButtonSet[i].guiImage.rectTransform.sizeDelta = new Vector2(menuButtonSet[i].sizeX, menuButtonSet[i].sizeY);
        }
    }
    private void ShowGUIScore(int i)
    {
        if (scoreSet[i].guiImage != null)
        {
            scoreSet[i].guiImage.rectTransform.anchoredPosition = new Vector2(scoreSet[i].x, scoreSet[i].y);
            scoreSet[i].guiImage.rectTransform.sizeDelta = new Vector2(scoreSet[i].sizeX, scoreSet[i].sizeY);
        }
    }
    public void Reset()
    {
        for (int i = 0; i < menuButtonSet.Count; i++)
        {
            menuButtonSet[i].guiImage.enabled = false;
        }
        for (int i = 0; i < itemStateSet.Count; i++)
        {
            itemStateSet[i].guiImage.enabled = false;
            itemStateSet[i].guiText.gameObject.SetActive(false);
        }
    }
}

