using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropBoxScript : MonoBehaviour {

    public delegate void DropBoxEvent();
    public event DropBoxEvent OnItemSelected;



    public List<ComboBoxItem> DataProvider = new List<ComboBoxItem>();
    public Text SelectedItemLabel;
    //public List<GameObject> buttonsList = new List<GameObject>();

    [SerializeField]
    Transform comboBoxPanel;
    [SerializeField]
    GameObject comboBoxButtonPrefab;

    private int _selectedIndex = -1;
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set 
        {
            //print(value + " " + DataProvider.Count);
            if (DataProvider.Count > value && value >= 0)
            {
                _selectedIndex = value;
                //comboBoxPanel.gameObject.SetActive(false);
                SelectedItem = DataProvider[_selectedIndex];
                //SelectedIndex = DataProvider.IndexOf(item);
                SelectedItemLabel.text = SelectedItem.Label;
                if (OnItemSelected != null)
                    OnItemSelected();
            }
        }
    }

    [HideInInspector]
    public ComboBoxItem SelectedItem;

    void Awake()
    {
        //System.Type type = System.Reflection.Assembly.GetExecutingAssembly ().GetType (myTypeName);
    }

    public void AddElement(string Label, object Data)
    {
        GameObject button = (GameObject)Instantiate(comboBoxButtonPrefab);
        button.GetComponentInChildren<Text>().text = Label;
        button.transform.SetParent(comboBoxPanel,false);

        ComboBoxItem item = new ComboBoxItem(Label, Data, button);
        button.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));

        DataProvider.Add(item);
    }
    public void AddElements(List<ComboBoxItem> Elements)
    {
        DataProvider.AddRange(Elements);
    }

    private void SelectItem(ComboBoxItem item)
    {
        comboBoxPanel.gameObject.SetActive(false);
        SelectedItem = item;
        SelectedIndex = DataProvider.IndexOf(item);
        SelectedItemLabel.text = item.Label;
        if (OnItemSelected != null)
            OnItemSelected();
    }
	
}

public class ComboBoxItem
{
    public GameObject Button;
    public string Label;
    public object Data;

    public ComboBoxItem(string Label, object Data)
    {
        this.Label = Label;
        this.Data = Data;
    }

    public ComboBoxItem(string Label, object Data, GameObject Button)
    {
        this.Label = Label;
        this.Data = Data;
        this.Button = Button;
    }
}

