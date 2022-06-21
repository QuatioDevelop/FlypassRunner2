using UnityEngine;

public class ColliderSpawnCheck : MonoBehaviour
{

    public bool isCollision = false;
    public bool isCollisionTemp = false;
    public GameObject headParent;
    public string nameColliderHit;
    public bool checkByName;
    public bool checkByTag;
    public float nextPos;

    private GameObject[] player;

    private int i;
    public void Update()
    {
        if (player == null || player.Length <= 1)
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            //print(player.Length);
        }
        else
        {
            /*print(player.Length);
            for (int i = 0; i < player.Length; i++)
            {
                print("    " + player[i]);
                
            }
             * for (int j = 0; j < player.Length; j++)
            {
                print("    " + player[j]);

            }
            print("** "+GameObject.FindGameObjectsWithTag("Player").Length);*/

            isCollisionTemp = true;
            for (i = 0; i < player.Length; i++)
            {
                if (player[i].transform.position.z < headParent.transform.position.z)
                {
                    isCollisionTemp = false;
                }
            }
            isCollision = isCollisionTemp;
            /*if (player[0].transform.position.z >= headParent.transform.position.z && player[1].transform.position.z >= headParent.transform.position.z)
            {
                isCollision = true;
            }*/
        }
    }
}
