using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeLabel : MonoBehaviour {

    public Text Label;

    private float _value;

    public float Value
    {
        get { return _value; }
        set
        {
            _value = value;
            Label.text = _value.ToString();
        }
    }
}
