using UnityEngine;

public class ColliderFinishCheck : MonoBehaviour
{

    public GameObject headParent;

    void OnTriggerEnter(Collider col)
    {
        //print("============================== OnTriggerEnter ============================" + col.tag +"  "+ LayerMask.LayerToName(col.gameObject.layer));
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Player_1")) && col.tag.Equals("Player"))
        {
            GameAttribute.gameAttribute.isPlaying = false;
            GameAttribute.gameAttribute.isTimeOver = true;
        }
        else if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Player_2")) && col.tag.Equals("Player"))
        {
            GameAttribute.gameAttribute.isPlaying = false;
            GameAttribute.gameAttribute.isTimeOver = true;
        }
    }
}
