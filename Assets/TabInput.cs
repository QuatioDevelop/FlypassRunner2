using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabInput : MonoBehaviour
{

    public TMP_InputField nombre;
    public TMP_InputField cedula;
    public TMP_InputField correo;

    int indexinput = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            indexinput++;
            if (indexinput > 2)
                indexinput = 0;

            SelectInput();
        }
    }

    public void SelectInput()
    {
        switch (indexinput)
        {
            case 0:
                nombre.Select();
                break;

            case 1:
                cedula.Select();
                break;

            case 2:
                correo.Select();
                break;

            default:
                break;
        }
    }

    public void NameSelected()
    {
        indexinput = 0;
    }

    public void DocSelected()
    {
        indexinput = 1;
    }

    public void EmailSelected()
    {
        indexinput = 2;
    }
}
