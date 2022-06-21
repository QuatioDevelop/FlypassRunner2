using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (buttonSelected == false)
        {
            /*if (Input.GetKey(KeyCode.JoystickButton0))
            {
                eventSystem.SetSelectedGameObject(selectedObject);
                buttonSelected = true;

            }*/

            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;

        }

    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}