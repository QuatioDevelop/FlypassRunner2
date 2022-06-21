using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.name);
        if (col.tag == "Item")
        {
            if (col.GetComponent<Item>().itemRotate != null)
            {
                col.GetComponent<Item>().itemRotate.PlayCoin();
            }
        }
    }
}
