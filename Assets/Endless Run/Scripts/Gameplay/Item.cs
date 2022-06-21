using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour
{

    public float scoreAdd; //add money if item = coin
    public int decreaseLife; //decrease life if item = obstacle 
    public int itemID; //item id
    public float duration; // duration item
    public float itemEffectValue; // effect value(if item star = speed , if item multiply = multiply number)
    public ItemRotate itemRotate; // rotate item
    public GameObject effectHit; // effect when hit item
    public List<Controller> collides = new List<Controller>();

    [HideInInspector]
    public bool itemActive;

    public enum TypeItem
    {
        Null, Coin, Obstacle, Obstacle_Roll, ItemJump, ItemSprint, ItemMagnet, ItemMultiply
    }

    public TypeItem typeItem;

    [HideInInspector]
    public bool useAbsorb = false;

    public static Item instance;

    void Start()
    {
        instance = this;
    }

    //Set item effect
    public void ItemGet(Controller controller)
    {
        if (GameAttribute.gameAttribute.deleyDetect == false)
        {
            if (typeItem == TypeItem.Coin)
            {
                HitCoin(controller);
                //Play sfx when get coin
                SoundManager.instance.PlayingSound("GetCoin");
            }
            else if (typeItem == TypeItem.Obstacle)
            {
                HitObstacle(controller);
                //Play sfx when get hit
                SoundManager.instance.PlayingSound("HitOBJ");
            }
            else if (typeItem == TypeItem.Obstacle_Roll)
            {
                if (Controller.instance.isRoll == false)
                {
                    HitObstacle(controller);
                    //Play sfx when get hit
                    SoundManager.instance.PlayingSound("HitOBJ");
                }
            }
            else if (typeItem == TypeItem.ItemSprint)
            {
                controller.Sprint(itemEffectValue, duration, controller.PlayerNumber);
                //Play sfx when get item
                SoundManager.instance.PlayingSound("GetItem");
                HideObj();
                initEffect(effectHit, controller);
            }
            else if (typeItem == TypeItem.ItemMagnet)
            {
                controller.Magnet(duration, controller.PlayerNumber);
                //Play sfx when get item
                SoundManager.instance.PlayingSound("GetItem");
                HideObj();
                initEffect(effectHit, controller);
            }
            else if (typeItem == TypeItem.ItemMultiply)
            {
                controller.Multiply(duration, controller.PlayerNumber);
                GameAttribute.gameAttribute.multiplyValue = itemEffectValue;
                //Play sfx when get item
                SoundManager.instance.PlayingSound("GetItem");
                HideObj();
                initEffect(effectHit, controller);
            }
        }
    }

    //Coin method
    private void HitCoin(Controller controller)
    {
        if (Controller.instance.isMultiply == false)
        {
            if (controller.PlayerNumber.Equals(Player.Player1))
            {
                GameAttribute.gameAttribute.coin += scoreAdd;
            }
            else
            {
                GameAttribute.gameAttribute.coinP2 += scoreAdd;
            }
        }
        else
        {
            if (controller.PlayerNumber.Equals(Player.Player1))
            {
                GameAttribute.gameAttribute.coin += (scoreAdd) * GameAttribute.gameAttribute.multiplyValue;
            }
            else
            {
                GameAttribute.gameAttribute.coinP2 += (scoreAdd) * GameAttribute.gameAttribute.multiplyValue;
            }
        }
        initEffect(effectHit, controller);
        HideObj();
    }

    //Obstacle method
    private void HitObstacle(Controller controller)
    {
        if (GameAttribute.gameAttribute.ageless == false)
        {
            if (controller.timeSprint <= 0)
            {
                if (controller.PlayerNumber.Equals(Player.Player1))
                {
                    GameAttribute.gameAttribute.life -= decreaseLife;
                }
                else
                {
                    GameAttribute.gameAttribute.lifeP2 -= decreaseLife;
                }
                GameAttribute.gameAttribute.ActiveShakeCamera();
            }
            else
            {
                HideObj();
                GameAttribute.gameAttribute.ActiveShakeCamera();
            }

        }
    }

    //Spawn effect method
    private void initEffect(GameObject prefab, Controller controller)
    {
        GameObject go = (GameObject)Instantiate(prefab, controller.transform.position, Quaternion.identity);
        go.transform.parent = controller.transform;
        go.layer = controller.gameObject.layer;
        foreach (Transform child in go.transform)
        {
            child.gameObject.layer = controller.gameObject.layer;
        }
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y + 0.5f, go.transform.localPosition.z);
    }

    //Magnet method
    public IEnumerator UseAbsorb(GameObject targetObj)
    {
        bool isLoop = true;
        useAbsorb = true;
        while (isLoop)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetObj.transform.position, GameAttribute.gameAttribute.speed * 2f * Time.smoothDeltaTime);
            if (Vector3.Distance(this.transform.position, targetObj.transform.position) < 0.6f)
            {
                isLoop = false;
                SoundManager.instance.PlayingSound("GetCoin");
                //HitCoin(targetObj.layer.Equals(LayerMask.NameToLayer("Player_1")) ? Player.Player1 : Player.Player2);
                HitCoin(targetObj.GetComponentInParent<Controller>());
            }
            yield return 0;
        }
        Reset();
        StopCoroutine("UseAbsorb");
        yield return 0;
    }

    public void HideObj()
    {
        if (useAbsorb == false)
        {
            //this.transform.parent = null;
            //this.transform.localPosition = new Vector3(-100, -100, -100);
        }
    }

    public void Reset()
    {
        layers.Clear();
        itemActive = false;
        this.transform.position = new Vector3(-100, -100, -100);
        this.transform.parent = null;
        useAbsorb = false;
    }

    private List<string> layers = new List<string>();
    public void ChangeLayer(string targetLayer)
    {
        //if (typeItem != TypeItem.Coin) return;// || typeItem != TypeItem.ItemMagnet || typeItem != TypeItem.ItemMultiply || typeItem != TypeItem.ItemSprint) return;
        //if (typeItem != TypeItem.Coin || typeItem != TypeItem.Obstacle || typeItem != TypeItem.Obstacle_Roll) return;// || typeItem != TypeItem.ItemMagnet || typeItem != TypeItem.ItemMultiply || typeItem != TypeItem.ItemSprint) return;

        SetLayerRecursively(gameObject, LayerMask.NameToLayer(targetLayer));
        /*foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(targetLayer);
            foreach (Transform child2 in child)
            {
                child2.gameObject.layer = LayerMask.NameToLayer(targetLayer);
            }
        }*/

        if(targetLayer != "Item")
        {
            if (!layers.Contains(targetLayer))
                layers.Add(targetLayer);

            if(layers.Count == 2)
            {
                layers.Clear();
                this.transform.parent = null;
                this.transform.localPosition = new Vector3(-100, -100, -100);
            }
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
