using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PatternSystem : MonoBehaviour
{
    //Building
    public enum StateBuilding
    {
        Build_1, Build_2, Build_3, Build_4, Null
    }
    [System.Serializable]
    public class SetBuilding
    {

        public int[] stateBuilding_Left = new int[8];
        public int[] stateBuilding_Right = new int[8];
    }

    [System.Serializable]
    public class SetBuildingAmount
    {
        public int[] stateBuilding_Left = new int[4];
        public int[] stateBuilding_Right = new int[4];
    }

    //Item
    [System.Serializable]
    public class SetItem
    {
        public int[] itemType_Left = new int[31];
        public int[] itemType_Right = new int[31];
    }
    [System.Serializable]
    public class FloorItemSlot
    {
        public bool[] floor_Slot_Left, floor_Slot_Right;
    }

    //Floor
    [System.Serializable]
    public class Floor
    {
        public bool[] floor_Slot_Left, floor_Slot_Right;
    }

    [System.Serializable]
    public class QueueFloor
    {
        public Floor floorClass;
        public FloorItemSlot floorItemSlotClass;
        public GameObject floorObj;
        public List<Building> getBuilding = new List<Building>();
        public List<Item> getItem = new List<Item>();
    }

    [System.Serializable]
    public class LevelItem
    {
        [HideInInspector]
        public string level = "Pattern";
        public List<SetItem> patternItem = new List<SetItem>();
    }

    [System.Serializable]
    public class Item_Type
    {
        public List<Item> itemList = new List<Item>();
    }

    [System.Serializable]
    public class SetFloatItemType
    {
        public List<int> item = new List<int>();
    }

    [HideInInspector]
    public List<Vector3> defaultPosBuilding_Left = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> defaultPosBuilding_Right = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> defaultPosItem_Left = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> defaultPosItem_Right = new List<Vector3>();

    //Prefab
    public List<GameObject> building_Pref = new List<GameObject>();
    public List<GameObject> item_Pref = new List<GameObject>();
    public GameObject spawnObj_Pref;
    public GameObject finishObj_Pref;
    public GameObject floor_Pref;

    [HideInInspector]
    public List<SetBuilding> patternBuilding = new List<SetBuilding>();
    [HideInInspector]
    public List<SetItem> patternItem = new List<SetItem>();
    private int[] amountBuildingSpawn;
    private int[] amountItemSpawn;
    private int amountFloorSpawn = 8;
    private float nextPosFloor = 32;
    public bool loadingComplete;
    public float loadingPercent;

    public static PatternSystem instance;

    //GameObject
    private List<GameObject> building_Obj = new List<GameObject>();
    private List<GameObject> item_Obj = new List<GameObject>();
    private List<GameObject> floor_Obj = new List<GameObject>();

    //Building
    private List<Building> building_Script = new List<Building>();
    private int[] maxAmountBuilding;

    //Type Item
    private List<Item_Type> item_Type_Script = new List<Item_Type>();
    private List<int> amount_Item_Pattern_Left = new List<int>();
    private List<int> amount_Item_Pattern_Right = new List<int>();


    //private List<Item> item_Script_Use = new List<Item>();
    private List<Floor> floor_Slot = new List<Floor>();
    private List<FloorItemSlot> floor_item_Slot = new List<FloorItemSlot>();
    private List<QueueFloor> queneFloor = new List<QueueFloor>();

    private GameObject spawnObj_Obj;
    private GameObject finishObj_Obj;
    private ColliderSpawnCheck colSpawnCheck;
    private ColliderFinishCheck colFinishCheck;

    //variable value in game
    private int randomPattern;
    private int randomItem;
    private Vector3 posFloorLast;

    //Defalut
    private Vector3 posStart = new Vector3(-100, -100, -100);
    private Vector3 angleLeft = new Vector3(0, 180, 0);
    private Vector3 angleRight = new Vector3(0, 0, 0);

    void Start()
    {
        instance = this;
        StartCoroutine(CalAmountItem());
    }

    private List<SetFloatItemType> _itemType = new List<SetFloatItemType>();
    private SetFloatItemType itemTypeMax;
    IEnumerator CalAmountItem()
    {
        //25%
        ConvertPatternToItemTpyeSet();
        itemTypeMax = new SetFloatItemType();
        int i = 0;
        while (i < item_Pref.Count)
        {
            itemTypeMax.item.Add(0);
            i++;
        }
        i = 0;
        loadingPercent = 1;
        while (i < _itemType.Count)
        {
            int j = 0;
            while (j < _itemType[i].item.Count)
            {
                if (_itemType[i].item[j] > itemTypeMax.item[j])
                {
                    itemTypeMax.item[j] = _itemType[i].item[j];
                }
                j++;
            }
            i++;
        }
        i = 0;
        loadingPercent = 3;
        amountItemSpawn = new int[itemTypeMax.item.Count];
        while (i < amountItemSpawn.Length)
        {
            amountItemSpawn[i] = itemTypeMax.item[i] * amountFloorSpawn;
            amountItemSpawn[i]++;
            i++;
        }
        yield return 0;
        loadingPercent = 5;
        StartCoroutine(CalAmountBuilding());
    }

    private void ConvertPatternToItemTpyeSet()
    {
        int i = 0;
        while (i < patternItem.Count)
        {
            _itemType.Add(new SetFloatItemType());
            int j = 0;
            while (j < item_Pref.Count)
            {
                _itemType[i].item.Add(0);
                j++;
            }
            i++;
        }
        i = 0;
        while (i < patternItem.Count)
        {
            int j = 0;
            //Left
            while (j < patternItem[i].itemType_Left.Length)
            {
                int k = 0;
                while (k < item_Pref.Count)
                {
                    if (patternItem[i].itemType_Left[j] == k + 1)
                    {
                        _itemType[i].item[k] += 1;
                    }

                    k++;
                }
                j++;
            }
            j = 0;
            //Right
            while (j < patternItem[i].itemType_Right.Length)
            {
                int k = 0;
                while (k < item_Pref.Count)
                {
                    if (patternItem[i].itemType_Right[j] == k + 1)
                    {
                        _itemType[i].item[k] += 1;
                    }

                    k++;
                }
                j++;
            }
            i++;
        }
    }

    //Building 
    private List<SetFloatItemType> _buildingType = new List<SetFloatItemType>();
    private SetFloatItemType buildTypeMax;
    IEnumerator CalAmountBuilding()
    {
        //50%
        ConvertPatternToBuildingTpyeSet();
        buildTypeMax = new SetFloatItemType();
        int i = 0;
        while (i < building_Pref.Count)
        {
            buildTypeMax.item.Add(0);
            i++;
        }
        i = 0;
        loadingPercent = 7;
        while (i < _buildingType.Count)
        {
            int j = 0;
            while (j < _buildingType[i].item.Count)
            {
                if (_buildingType[i].item[j] > buildTypeMax.item[j])
                {
                    buildTypeMax.item[j] = _buildingType[i].item[j];
                }
                j++;
            }
            i++;
        }
        i = 0;
        loadingPercent = 9;
        amountBuildingSpawn = new int[buildTypeMax.item.Count];
        while (i < amountBuildingSpawn.Length)
        {
            amountBuildingSpawn[i] = buildTypeMax.item[i] * amountFloorSpawn;
            amountBuildingSpawn[i]++;
            i++;
        }
        yield return 0;
        loadingPercent = 10;
        StartCoroutine(InitBuilding());
    }

    private void ConvertPatternToBuildingTpyeSet()
    {
        int i = 0;
        while (i < patternBuilding.Count)
        {
            _buildingType.Add(new SetFloatItemType());
            int j = 0;
            while (j < building_Pref.Count)
            {
                _buildingType[i].item.Add(0);
                j++;
            }
            i++;
        }
        i = 0;
        while (i < patternBuilding.Count)
        {
            int j = 0;
            //Left
            while (j < patternBuilding[i].stateBuilding_Left.Length)
            {
                int k = 0;
                while (k < building_Pref.Count)
                {
                    if (patternBuilding[i].stateBuilding_Left[j] == k + 1)
                    {
                        _buildingType[i].item[k] += 1;
                    }

                    k++;
                }
                j++;
            }
            j = 0;
            //Right
            while (j < patternBuilding[i].stateBuilding_Right.Length)
            {
                int k = 0;
                while (k < building_Pref.Count)
                {
                    if (patternBuilding[i].stateBuilding_Right[j] == k + 1)
                    {
                        _buildingType[i].item[k] += 1;
                    }

                    k++;
                }
                j++;
            }
            i++;
        }
    }

    IEnumerator InitBuilding()
    {
        //75%
        int i = 0;
        while (i < building_Pref.Count)
        {
            int j = 0;
            while (j < amountBuildingSpawn[i])
            {
                GameObject go = (GameObject)Instantiate(building_Pref[i], posStart, Quaternion.identity);
                go.name = "Building[" + i + "][" + j + "]";
                building_Obj.Add(go);
                Building building = go.GetComponent<Building>();
                building.buildIndex = i;
                building_Script.Add(building);
                j++;
                yield return 0;
            }
            i++;
            yield return 0;
        }
        loadingPercent = 30;
        i = 0;
        while (i < item_Pref.Count)
        {
            int j = 0;
            item_Type_Script.Add(new Item_Type());
            amount_Item_Pattern_Left.Add(0);
            amount_Item_Pattern_Right.Add(0);
            while (j < amountItemSpawn[i])
            {
                GameObject go = (GameObject)Instantiate(item_Pref[i], posStart, Quaternion.identity);
                go.name = "Item[" + i + "][" + j + "]";
                item_Obj.Add(go);
                item_Type_Script[i].itemList.Add(go.GetComponent<Item>());
                j++;
                yield return 0;
            }
            i++;
            yield return 0;
        }
        i = 0;
        loadingPercent = 70;
        while (i < amountFloorSpawn)
        {
            GameObject go = (GameObject)Instantiate(floor_Pref, posStart, Quaternion.identity);
            go.name = "Floor[" + i + "]";
            floor_Obj.Add(go);
            floor_Slot.Add(new Floor());
            floor_Slot[i].floor_Slot_Left = new bool[defaultPosBuilding_Left.Count];
            floor_Slot[i].floor_Slot_Right = new bool[defaultPosBuilding_Right.Count];
            floor_item_Slot.Add(new FloorItemSlot());
            floor_item_Slot[i].floor_Slot_Left = new bool[defaultPosItem_Left.Count];
            floor_item_Slot[i].floor_Slot_Right = new bool[defaultPosItem_Right.Count];
            QueueFloor qFloor = new QueueFloor();
            qFloor.floorObj = floor_Obj[i];
            qFloor.floorClass = floor_Slot[i];
            qFloor.floorItemSlotClass = floor_item_Slot[i];
            queneFloor.Add(qFloor);
            i++;
            yield return 0;
        }
        loadingPercent = 100;
        spawnObj_Obj = (GameObject)Instantiate(spawnObj_Pref, posStart, Quaternion.identity);
        colSpawnCheck = spawnObj_Obj.GetComponentInChildren<ColliderSpawnCheck>();
        colSpawnCheck.headParent = spawnObj_Obj;
        finishObj_Obj = (GameObject)Instantiate(finishObj_Pref, posStart, Quaternion.identity);
        colFinishCheck = finishObj_Obj.GetComponentInChildren<ColliderFinishCheck>();
        colFinishCheck.headParent = finishObj_Obj;
        StartCoroutine(SetPosStarter());
    }

    IEnumerator SetPosStarter()
    {
        //100%
        Vector3 pos = Vector3.zero;
        pos.z = nextPosFloor;
        int i = 0;
        while (i < floor_Obj.Count)
        {
            AddBuildingToFloor(queneFloor[i]);
            queneFloor[i].floorObj.transform.position = pos;
            pos.z += nextPosFloor;
            i++;
            yield return 0;
        }
        posFloorLast = pos;
        pos.z -= nextPosFloor * 1.0f;
        colFinishCheck.headParent.transform.position = pos;
        pos = Vector3.zero;
        pos.z += nextPosFloor * 2;
        colSpawnCheck.headParent.transform.position = pos;
        yield return new WaitForSeconds(1);
        loadingComplete = true;
        StartCoroutine(WaitCheckFloor());
        yield return 0;
    }

    IEnumerator WaitCheckFloor()
    {
        while (colSpawnCheck.isCollision == false)
        {
            yield return 0;
        }
        colSpawnCheck.isCollision = false;
        StartCoroutine(SetPosFloor());
        yield return 0;
    }

    IEnumerator SetPosFloor()
    {
        Vector3 pos = Vector3.zero;
        pos.z = colFinishCheck.headParent.transform.position.z;
        pos.z += nextPosFloor;
        colFinishCheck.headParent.transform.position = pos;
        pos.z = colSpawnCheck.headParent.transform.position.z;
        pos.z += nextPosFloor;
        colSpawnCheck.headParent.transform.position = pos;
        colSpawnCheck.nextPos = colSpawnCheck.headParent.transform.position.z;
        int i = 0;
        while (i < queneFloor[0].floorClass.floor_Slot_Left.Length)
        {
            queneFloor[0].floorClass.floor_Slot_Left[i] = false;
            queneFloor[0].floorClass.floor_Slot_Right[i] = false;
            i++;
            //yield return 0;
        }
        i = 0;
        while (i < queneFloor[0].floorItemSlotClass.floor_Slot_Left.Length)
        {
            queneFloor[0].floorItemSlotClass.floor_Slot_Left[i] = false;
            queneFloor[0].floorItemSlotClass.floor_Slot_Right[i] = false;
            i++;
            //yield return 0;
        }
        i = 0;
        int itemCount = queneFloor[0].getItem.Count;
        while (i < itemCount)
        {
            queneFloor[0].getItem[0].itemActive = false;
            queneFloor[0].getItem[0].transform.parent = null;
            queneFloor[0].getItem[0].transform.position = posStart;
            ReturnItemWithType(queneFloor[0].getItem[0]);
            queneFloor[0].getItem.RemoveRange(0, 1);
            i++;
            //yield return 0;
        }
        i = 0;
        int buildingCount = queneFloor[0].getBuilding.Count;
        while (i < buildingCount)
        {
            queneFloor[0].getBuilding[0].transform.parent = null;
            queneFloor[0].getBuilding[0].transform.position = posStart;
            queneFloor[0].getBuilding[0].buildingActive = false;
            queneFloor[0].getBuilding.RemoveRange(0, 1);
            i++;
            //yield return 0;
        }

        StartCoroutine(AddBuilding());
        yield return 0;
    }

    IEnumerator AddBuilding()
    {
        QueueFloor qFloor = queneFloor[0];
        queneFloor.RemoveRange(0, 1);
        int i = 0;
        randomPattern = Random.Range(0, patternBuilding.Count);
        randomItem = Random.Range(0, patternItem.Count);
        //print("AddBuilding::Items::"+randomItem);
        while (i < building_Script.Count)
        {
            int j = 0;
            while (j < patternBuilding[randomPattern].stateBuilding_Left.Length)
            {
                CheckAddBuilding_Left(i, j, qFloor);
                j++;
            }
            j = 0;
            while (j < patternBuilding[randomPattern].stateBuilding_Right.Length)
            {
                CheckAddBuilding_Right(i, j, qFloor);
                j++;
            }
            i++;
        }
        //yield return 0;
        i = 0;
        CheckTypeItemFormAdd(qFloor, i);
        //yield return 0;
        qFloor.floorObj.transform.position = posFloorLast;
        posFloorLast.z += nextPosFloor;
        queneFloor.Add(qFloor);
        StartCoroutine(WaitCheckFloor());
        yield return 0;
    }

    public void Reset()
    {
        Vector3 pos = Vector3.zero;
        nextPosFloor = 32;
        pos.z += nextPosFloor;
        colSpawnCheck.headParent.transform.position = pos;
        colSpawnCheck.nextPos = colSpawnCheck.headParent.transform.position.z;
        int y = 0;
        while (y < queneFloor.Count)
        {
            int i = 0;
            while (i < queneFloor[y].floorClass.floor_Slot_Left.Length)
            {
                queneFloor[y].floorClass.floor_Slot_Left[i] = false;
                queneFloor[y].floorClass.floor_Slot_Right[i] = false;
                i++;
            }
            i = 0;
            int itemCount = queneFloor[y].getItem.Count;
            while (i < itemCount)
            {
                queneFloor[y].getItem[0].itemActive = false;
                queneFloor[y].getItem[0].transform.parent = null;
                queneFloor[y].getItem[0].transform.position = posStart;
                ReturnItemWithType(queneFloor[y].getItem[0]);
                queneFloor[y].getItem.RemoveRange(0, 1);
                i++;
            }
            i = 0;
            int buildingCount = queneFloor[y].getBuilding.Count;
            while (i < buildingCount)
            {
                queneFloor[y].getBuilding[0].transform.parent = null;
                queneFloor[y].getBuilding[0].transform.position = posStart;
                queneFloor[y].getBuilding[0].buildingActive = false;
                queneFloor[y].getBuilding.RemoveRange(0, 1);
                i++;
            }
            i = 0;
            while (i < queneFloor[y].floorItemSlotClass.floor_Slot_Left.Length)
            {
                queneFloor[y].floorItemSlotClass.floor_Slot_Left[i] = false;
                queneFloor[y].floorItemSlotClass.floor_Slot_Right[i] = false;
                i++;
            }
            y++;
        }
        posFloorLast.z = 32;
        StopAllCoroutines();
        StartCoroutine(SetPosStarter());
    }

    // Function Call
    #region
    void AddBuildingToFloor(QueueFloor floor)
    {
        int i = 0;
        randomPattern = Random.Range(0, patternBuilding.Count);
        randomItem = Random.Range(0, patternItem.Count);
        //print("AddBuildingToFloor::Items::" + randomItem);
        while (i < building_Script.Count)
        {
            int j = 0;
            while (j < patternBuilding[randomPattern].stateBuilding_Left.Length)
            {
                CheckAddBuilding_Left(i, j, floor);
                j++;
            }
            j = 0;
            while (j < patternBuilding[randomPattern].stateBuilding_Right.Length)
            {
                CheckAddBuilding_Right(i, j, floor);
                j++;

            }
            i++;
        }
        i = 0;
        CheckTypeItemFormAdd(floor, i);
    }

    void ReturnItemWithType(Item _item)
    {
        int i = 0;
        while (i < amountItemSpawn.Length)
        {
            ReturnItem(_item, i + 1);
            i++;
        }
        i = 0;
        while (i < amount_Item_Pattern_Right.Count)
        {
            amount_Item_Pattern_Left[i] = 0;
            amount_Item_Pattern_Right[i] = 0;
            i++;
        }
    }

    void ReturnItem(Item _item, int itemID)
    {
        if (_item.itemID == itemID)
        {
            item_Type_Script[itemID - 1].itemList.Add(_item);
        }
    }

    void CheckTypeItemFormAdd(QueueFloor floor, int i)
    {
        while (i < patternItem[randomItem].itemType_Left.Length)
        {
            int j = 0;
            while (j < amount_Item_Pattern_Left.Count)
            {
                if (patternItem[randomItem].itemType_Left[i] == j + 1)
                {
                    amount_Item_Pattern_Left[j] += 1;
                }
                j++;
            }
            i++;
        }
        i = 0;

        while (i < patternItem[randomItem].itemType_Right.Length)
        {
            int j = 0;
            while (j < amount_Item_Pattern_Right.Count)
            {
                if (patternItem[randomItem].itemType_Right[i] == j + 1)
                {
                    amount_Item_Pattern_Right[j] += 1;
                }
                j++;
            }
            i++;
        }
        i = 0;

        //Add Item To Floor Left	
        while (i < patternItem[randomItem].itemType_Left.Length)
        {
            int s = 0;
            while (s < amountItemSpawn.Length)
            {
                AddItemWihtType_Left(floor, i, s + 1);
                s++;
            }
            i++;
        }
        i = 0;

        //Add Item To Floor Right
        while (i < patternItem[randomItem].itemType_Right.Length)
        {
            int s = 0;
            while (s < amountItemSpawn.Length)
            {
                AddItemWihtType_Right(floor, i, s + 1);
                s++;
            }
            i++;
        }
        i = 0;
    }

    void AddItemWihtType_Left(QueueFloor floor, int slotIndex, int type)
    {
        if (patternItem[randomItem].itemType_Left[slotIndex] == type)
        {
            int j = 0;
            while (j < amount_Item_Pattern_Left[type - 1])
            {
                if (j < item_Type_Script[type - 1].itemList.Count)
                {
                    if (item_Type_Script[type - 1].itemList[j].itemActive == false
                        && floor.floorItemSlotClass.floor_Slot_Left[slotIndex] == false)
                    {
                        SetPosItem_Left_For_Type(slotIndex, type - 1, j, floor);
                        j = 0;
                    }
                }

                j++;
            }
        }
    }
    void AddItemWihtType_Right(QueueFloor floor, int slotIndex, int type)
    {
        if (patternItem[randomItem].itemType_Right[slotIndex] == type)
        {
            int j = 0;
            while (j < amount_Item_Pattern_Right[type - 1])
            {
                if (j < item_Type_Script[type - 1].itemList.Count)
                {
                    if (item_Type_Script[type - 1].itemList[j].itemActive == false
                        && floor.floorItemSlotClass.floor_Slot_Right[slotIndex] == false)
                    {
                        SetPosItem_Right_For_Type(slotIndex, type - 1, j, floor);
                        j = 0;
                    }
                }
                j++;
            }
        }
    }

    void CheckAddBuilding_Left(int i, int j, QueueFloor floor)
    {
        int index = 0;

        while (index < building_Pref.Count)
        {
            if (patternBuilding[randomPattern].stateBuilding_Left[j] == index + 1 && floor.floorClass.floor_Slot_Left[j] == false)
            {
                if (building_Script[i].buildingActive == false && building_Script[i].buildIndex == index)
                {
                    SetPosBuilding_Left(i, j, floor);
                    index = building_Pref.Count;
                }
            }
            index++;
        }
    }

    void CheckAddBuilding_Right(int i, int j, QueueFloor floor)
    {

        int index = 0;

        while (index < building_Pref.Count)
        {
            if (patternBuilding[randomPattern].stateBuilding_Right[j] == index + 1 && floor.floorClass.floor_Slot_Right[j] == false)
            {
                if (building_Script[i].buildingActive == false && building_Script[i].buildIndex == index)
                {
                    SetPosBuilding_Right(i, j, floor);
                    index = building_Pref.Count;
                }
            }
            index++;
        }

    }

    void SetPosBuilding_Left(int i, int j, QueueFloor floor)
    {
        building_Script[i].transform.parent = floor.floorObj.transform;
        building_Script[i].transform.localPosition = defaultPosBuilding_Left[j];
        building_Script[i].transform.eulerAngles = angleLeft;
        building_Script[i].buildingActive = true;
        floor.floorClass.floor_Slot_Left[j] = true;
        floor.getBuilding.Add(building_Script[i]);
    }

    void SetPosBuilding_Right(int i, int j, QueueFloor floor)
    {
        building_Script[i].transform.parent = floor.floorObj.transform;
        building_Script[i].transform.localPosition = defaultPosBuilding_Right[j];
        building_Script[i].transform.eulerAngles = angleRight;
        building_Script[i].buildingActive = true;
        floor.floorClass.floor_Slot_Right[j] = true;
        floor.getBuilding.Add(building_Script[i]);
    }

    void SetPosItem_Left_For_Type(int i, int j, int countItem, QueueFloor floor)
    {
        item_Type_Script[j].itemList[countItem].transform.parent = floor.floorObj.transform;
        item_Type_Script[j].itemList[countItem].transform.localPosition = defaultPosItem_Left[i];
        item_Type_Script[j].itemList[countItem].itemActive = true;
        item_Type_Script[j].itemList[countItem].collides.Clear();
        item_Type_Script[j].itemList[countItem].ChangeLayer("Item");
        floor.floorItemSlotClass.floor_Slot_Left[i] = true;
        floor.getItem.Add(item_Type_Script[j].itemList[countItem]);
        item_Type_Script[j].itemList.RemoveRange(countItem, 1);
    }

    void SetPosItem_Right_For_Type(int i, int j, int countItem, QueueFloor floor)
    {
        item_Type_Script[j].itemList[countItem].transform.parent = floor.floorObj.transform;
        item_Type_Script[j].itemList[countItem].transform.localPosition = defaultPosItem_Right[i];
        item_Type_Script[j].itemList[countItem].itemActive = true;
        item_Type_Script[j].itemList[countItem].collides.Clear();
        item_Type_Script[j].itemList[countItem].ChangeLayer("Item");
        floor.floorItemSlotClass.floor_Slot_Right[i] = true;
        floor.getItem.Add(item_Type_Script[j].itemList[countItem]);
        item_Type_Script[j].itemList.RemoveRange(countItem, 1);
    }


    #endregion
}
