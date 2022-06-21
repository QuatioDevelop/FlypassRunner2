using UnityEngine;
using System.Collections;
using System;
using Vectrosity;
using UnityEngine.UI;

public class Configuration : MonoBehaviour {

    public DropBoxScript cameraDevices;
    public GameObject renderObject;
    public int device = 0;
    public Texture2D blackTexture;

    public Material lineMaterial;
    public float textureScale = 4.0f;
    private VectorLine selectionLine;
    private VectorLine selectionLine2;
    public Vector2 originalPos;
    public Vector2 originalPos2;
    public Vector2 sizeRect;
    public Vector2 sizeRect2;

    private int cameraWidth = 640;
    private int cameraHeight = 360;
    private int cameraFPS = 15;

    private WebCamTexture webcamTexture;

    private bool cameraStarted = false;

    public Slider xSlider;
    public Slider x2Slider;
    public Slider ySlider;
    public Slider y2Slider;
    public Slider widthSlider;
    public Slider width2Slider;
    public Slider heightSlider;
    public Slider height2Slider;
    public Slider thresholdSlider;
    public InputField gameTime;

    private int _time = 60;

    private float _threshold = 0.9f;
    public float Threshold
    {
        set
        {
            _threshold = value;
        }
    }

    private float _x = 0;
    public float X_1
    {
        set 
        { 
            _x = value;
            originalPos.x = Camera.main.rect.width * Screen.width * _x;
        }
    }

    private float _x2;
    public float X_2
    {
        set 
        { 
            _x2 = value;
            originalPos2.x = Camera.main.rect.width * Screen.width * _x2;
        }
    }

    private float _y;
    public float Y_1
    {
        set 
        { 
            _y = value;
            float newh = Camera.main.rect.height * Screen.height;
            originalPos.y = newh - newh * _y - newh * _height;
        }
    }

    private float _y2;
    public float Y_2
    {
        set 
        { 
            _y2 = value;
            float newh = Camera.main.rect.height * Screen.height;
            originalPos2.y = newh - newh * _y2 - newh * _height2;
        }
    }

    private float _width;
    public float Width_1
    {
        set 
        { 
            _width = value;
            sizeRect.x = Camera.main.rect.width * Screen.width * _width;
        }
    }

    private float _width2;
    public float Width_2
    {
        set 
        { 
            _width2 = value;
            sizeRect2.x = Camera.main.rect.width * Screen.width * _width2;
        }
    }

    private float _height;
    public float Height
    {
        set 
        { 
            _height = value;
            float newh = Camera.main.rect.height * Screen.height;
            sizeRect.y = newh * _height;
            originalPos.y = newh - newh * _y - newh * _height;
        }
    }

    private float _height2;
    public float Height2
    {
        set 
        { 
            _height2 = value;
            float newh = Camera.main.rect.height * Screen.height;
            sizeRect2.y = newh * _height2;
            originalPos2.y = newh - newh * _y2 - newh * _height2;
        }
    }

	// Use this for initialization
	void Start () {
        webcamTexture = new WebCamTexture();
        cameraStarted = false;

        selectionLine = new VectorLine("Selection", new Vector2[5], lineMaterial, 6.0f, LineType.Continuous, Joins.Fill);
        selectionLine.textureScale = textureScale;
        selectionLine2 = new VectorLine("Selection2", new Vector2[5], lineMaterial, 6.0f, LineType.Continuous, Joins.Fill);
        selectionLine2.textureScale = textureScale;
        VectorLine.SetCanvasCamera(Camera.main);
        VectorLine.canvas.planeDistance = 1;
        //cameraWidth = 640;
        //cameraHeight = 360;

        print(Camera.main.rect.width);
        print(Camera.main.rect.height);

        _x = 0f;
        _x2 = 0.5f;
        _y = 0f;
        _y2 = 0f;
        _width = 0.5f;
        _width2 = 0.5f;
        _height = 1f;
        _height2 = 1f;

        if (PlayerPrefs.HasKey("_x"))
            _x = PlayerPrefs.GetFloat("_x");
        if (PlayerPrefs.HasKey("_x2"))
            _x2 = PlayerPrefs.GetFloat("_x2");
        if (PlayerPrefs.HasKey("_y"))
            _y = PlayerPrefs.GetFloat("_y");
        if (PlayerPrefs.HasKey("_y2"))
            _y2 = PlayerPrefs.GetFloat("_y2");
        if (PlayerPrefs.HasKey("_width"))
            _width = PlayerPrefs.GetFloat("_width");
        if (PlayerPrefs.HasKey("_width2"))
            _width2 = PlayerPrefs.GetFloat("_width2");
        if (PlayerPrefs.HasKey("_height"))
            _height = PlayerPrefs.GetFloat("_height");
        if (PlayerPrefs.HasKey("_height2"))
            _height2 = PlayerPrefs.GetFloat("_height2");
        if (PlayerPrefs.HasKey("_threshold"))
            _threshold = PlayerPrefs.GetFloat("_threshold");
        if (PlayerPrefs.HasKey("device"))
            device = PlayerPrefs.GetInt("device");
        if (PlayerPrefs.HasKey("_time"))
            _time = PlayerPrefs.GetInt("_time");

        //print(_threshold);

        /*originalPos.x = Screen.width * 0f;
        originalPos2.x = Screen.width * 0.5f;
        originalPos.y = Screen.height * 0f;
        originalPos2.y = Screen.height * 0f;
        sizeRect.x = Screen.width * 0.5f;
        sizeRect2.x = Screen.width * 0.5f;
        sizeRect.y = Screen.height * 1f;
        sizeRect2.y = Screen.height * 1f;*/

        xSlider.value = _x;
        x2Slider.value = _x2;
        ySlider.value = _y;
        y2Slider.value = _y2;
        widthSlider.value = _width;
        width2Slider.value = _width2;
        heightSlider.value = _height;
        height2Slider.value = _height2;
        gameTime.text = _time.ToString();

        thresholdSlider.value = _threshold; 

        initWebCam();

        StartCoroutine(check_for_resize());
	}

    void initWebCam()
    {
        if (webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }

        WebCamDevice[] devices = WebCamTexture.devices;

        Debug.Log(" devices.Length - " + devices.Length);

        for (var i = 0; i < devices.Length; i++)
        {
            Debug.Log(" " + i + " - " + devices[i].name);
            cameraDevices.AddElement(devices[i].name, devices[i] as object);
        }

        cameraDevices.OnItemSelected += cameraDevices_OnItemSelected;

        if (device < devices.Length)
        {
            try
            {
                webcamTexture.deviceName = devices[device].name;
                webcamTexture.requestedWidth = cameraWidth;
                webcamTexture.requestedHeight = cameraHeight;
                webcamTexture.requestedFPS = cameraFPS;
                cameraDevices.SelectedIndex = device;
            }
            catch (Exception) { }
        }
        else
        {
            return;
        }

        /*if (renderObject != null)
        {
            renderObject.GetComponent<Renderer>().material.mainTexture = webcamTexture;
        }*/

        try
        {
            webcamTexture.Play();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            return;
        }

        /*if (cameraWidth != webcamTexture.width || cameraHeight != webcamTexture.height)
        {
            Debug.LogWarning("Camera not supporting resolution " + cameraWidth + "*" + cameraHeight + ". Reset to " + webcamTexture.width + "*" + webcamTexture.height + ".");
            cameraWidth = webcamTexture.width;
            cameraHeight = webcamTexture.height;
        }*/

        if (webcamTexture.isPlaying)
            cameraStarted = true;
        else
            renderObject.GetComponent<Renderer>().material.mainTexture = blackTexture;

        Debug.Log(webcamTexture.isPlaying + " " + webcamTexture.deviceName + " - " + cameraStarted + " " + webcamTexture.width + "x" + webcamTexture.height);

        /*if (renderObject != null)
        {
            renderObject.GetComponent<Renderer>().material = CameraMaterial;
        }*/
    }

    private void cameraDevices_OnItemSelected()
    {
        // stop playing
        if (null != webcamTexture)
        {
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Stop();
            }
        }

        // destroy the old texture
        if (null != webcamTexture)
        {
            UnityEngine.Object.DestroyImmediate(webcamTexture, true);
        }

        // use the device name
        webcamTexture = new WebCamTexture(((WebCamDevice)cameraDevices.SelectedItem.Data).name as string);

        // start playing
        webcamTexture.Play();

        device = cameraDevices.SelectedIndex;

        print("PLAYING... " + device);

        // assign the texture
        //CameraMaterial.mainTexture = webcamTexture;

        //CameraMaterial.shader = Shader.Find("Hidden/UChromaKey");
        //CameraMaterial.shader = Shader.Find("Hidden/UChromaKey_mobile");

        if (renderObject != null)
        {
            renderObject.GetComponent<Renderer>().material.mainTexture = webcamTexture;

            //renderObject.GetComponent<Renderer>().material.shader = Shader.Find("Hidden/UChromaKey");
            //renderObject.GetComponent<Renderer>().material = CameraMaterial;
        }
    }

	// Update is called once per frame
    void Update()
    {
        try
        {
            selectionLine.MakeRect(originalPos, originalPos + sizeRect);
            selectionLine.Draw();
            selectionLine2.MakeRect(originalPos2, originalPos2 + sizeRect2);
            selectionLine2.Draw();
           
            selectionLine.textureOffset = -Time.time * 2.0f % 1f;
            selectionLine2.textureOffset = -Time.time * 2.0f % 1f;
        }
        catch{}
    }

    public void stop()
    {
        stay = false;
        webcamTexture.Stop();
    }

    public void SaveConfig()
    {
        VectorLine.Destroy(ref selectionLine);
        VectorLine.Destroy(ref selectionLine2);
        stop();
        /*
        _x        =
        _x2       =
        _y        =
        _y2       =
        _width    =
        _width2   =
        _height   =
        _height2  =
        */
        PlayerPrefs.SetFloat("_x", _x);
        PlayerPrefs.SetFloat("_x2", _x2);
        PlayerPrefs.SetFloat("_y", _y);
        PlayerPrefs.SetFloat("_y2", _y2);
        PlayerPrefs.SetFloat("_width", _width);
        PlayerPrefs.SetFloat("_width2", _width2);
        PlayerPrefs.SetFloat("_height", _height);
        PlayerPrefs.SetFloat("_height2", _height2);
        PlayerPrefs.SetFloat("_threshold", _threshold);
        PlayerPrefs.SetInt("device", device);
        PlayerPrefs.SetInt("_time", int.Parse(gameTime.text));
            
        Application.LoadLevel("Gameplay");
    }

    private int lastWidth;
    private int lastHeight;
    private bool stay = true;

    IEnumerator check_for_resize()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;

        while (stay)
        {
            if (lastWidth != Screen.width || lastHeight != Screen.height)
            {
                calculate_rects();
                lastWidth = Screen.width;
                lastHeight = Screen.height;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void calculate_rects()
    {
        float newh = Camera.main.rect.height * Screen.height;
        originalPos.x = Camera.main.rect.width * Screen.width * _x;
        originalPos2.x = Camera.main.rect.width * Screen.width * _x2;
        originalPos.y = newh - newh * _y - newh * _height;
        originalPos2.y = newh - newh * _y2 - newh * _height2;

        sizeRect.x = Camera.main.rect.width * Screen.width * _width;
        sizeRect2.x = Camera.main.rect.width * Screen.width * _width2;
        sizeRect.y = newh * _height;
        sizeRect2.y = newh * _height2;
    }

    void OnDestroy()
    {
        stay = false;
    }

    void OnApplicationQuit()
    {
        stay = false;
        webcamTexture.Stop();
    }
}
