using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using Vectrosity;
using System.Collections.Generic;
//using UnityEditor;

public class OpticalFlowDataStr
{
    public bool isActive;					 
    public float dir;					     
    public float xForce;					 
    public float yForce;					 
    public float force;						 
};

public class OpticalFlow : MonoBehaviour
{

    private const float toDegree = 180 / Mathf.PI;

    [HideInInspector]
    public int cameraWidth = 640;
    [HideInInspector]
    public int cameraHeight = 360;
    private int cameraFPS = 15;
    private WebCamTexture webcamTexture;
    private Color32[] image;    //imagen actual de la camara
    private Color32[] imageprev;    //imagen previa de la camara
    public GameObject renderObject;
    public int device = 0;

    public GameObject arrow_P1;
    public GameObject arrow_P2;

    private OpticalFlowDataStr opticalFlowDataStr_P1;
    private Vector3 rotEuler_P1;
    private Vector3 locScale_P1;
    private OpticalFlowDataStr opticalFlowDataStr_P2;
    private Vector3 rotEuler_P2;
    private Vector3 locScale_P2;

    private bool cameraStarted = false;

    private float _x = 0;
    private float _x2 = 0.5f;
    private float _y = 0;
    private float _y2 = 0;
    private float _width = 0.5f;
    private float _width2 = 0.5f;
    private float _height = 1;
    private float _height2 = 1;
    private float _threshold = 1.2f;

    private float x1;
    private float y1;
    private float w1;
    private float h1;

    private float x2;
    private float y2;
    private float w2;
    private float h2;

    //private VectorLine line;
    //private List<Color> lineColors;
    public Material lineMaterial;

    public bool CameraStarted
    {
        get { return cameraStarted; }
    }

    public Texture2D blackTexture;

    void Start()
    {
        cameraWidth = 640;
        cameraHeight = 360;

        if (PlayerPrefs.HasKey("device"))
            device = PlayerPrefs.GetInt("device");

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

        x1 = cameraWidth - _x * cameraWidth - _width * cameraWidth;
        //y1 = _y * cameraHeight;
        y1 = cameraHeight - cameraHeight * _y - cameraHeight * _height; 
        w1 = _width * cameraWidth;
        h1 = _height * cameraHeight;

        x2 = cameraWidth - _x2 * cameraWidth - _width2 * cameraWidth;
        //y2 = _y2 * cameraHeight;
        y2 = cameraHeight - cameraHeight * _y2 - cameraHeight * _height2;
        w2 = _width2 * cameraWidth;
        h2 = _height2 * cameraHeight;

        webcamTexture = new WebCamTexture();
        cameraStarted = false;


        initWebCam();

        rotEuler_P1 = arrow_P1.transform.eulerAngles;
        locScale_P1 = arrow_P1.transform.localScale;

        rotEuler_P2 = arrow_P2.transform.eulerAngles;
        locScale_P2 = arrow_P2.transform.localScale;

        //lineColors = new List<Color>();
        //line = new VectorLine("Line", new Vector2[0], lineMaterial, 2, LineType.Discrete, Joins.None);
        //line.drawTransform = transform;

        image = new Color32[cameraWidth * cameraHeight];
        imageprev = new Color32[cameraWidth * cameraHeight];
    }

    void initWebCam()
    {
        if (webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }

        WebCamDevice[] devices = WebCamTexture.devices;

        for (var i = 0; i < devices.Length; i++)
            Debug.Log(" " + i + " - " + devices[i].name);

        if (device < devices.Length)
        {
            try
            {
                webcamTexture.deviceName = devices[device].name;
                webcamTexture.requestedWidth = cameraWidth;
                webcamTexture.requestedHeight = cameraHeight;
                webcamTexture.requestedFPS = cameraFPS;
            }
            catch (Exception) { }
        }
        else
        {
            return;
        }

        if (renderObject != null)
        {
            renderObject.GetComponent<Renderer>().material.mainTexture = webcamTexture;
        }

        /*if (renderObjectDebug != null)
        {
            renderObjectDebug.GetComponent<Renderer>().material = matdebug;
        }*/

        try
        {
            //if (!webcamTexture.isPlaying)
                webcamTexture.Play();
        }
        catch (Exception e) 
        {
            Debug.LogWarning(e.Message);
            return; 
        }
        
        if (cameraWidth != webcamTexture.width || cameraHeight != webcamTexture.height)
        {
            Debug.LogWarning("Camera not supporting resolution " + cameraWidth + "*" + cameraHeight + ". Reset to " + webcamTexture.width + "*" + webcamTexture.height + ".");
            cameraWidth = webcamTexture.width;
            cameraHeight = webcamTexture.height;
        }

        /*if (!isDebug)
        {
            renderObjectDebug.GetComponent<Renderer>().enabled = false;
        }*/

        if (webcamTexture.isPlaying)
            cameraStarted = true;
        else
            GetComponent<Renderer>().material.mainTexture = blackTexture;
        Debug.Log(webcamTexture.isPlaying + " " + webcamTexture.deviceName + " - " + cameraStarted + " " + cameraWidth + "x" + cameraHeight);
        Debug.Log("**** "+webcamTexture.isPlaying + " " + webcamTexture.deviceName + " - " + cameraStarted + " " + webcamTexture.width + "x" + webcamTexture.height);
    }

    /*void KeyboardInputs()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            isDebug = !isDebug;

            if (isDebug)
            {
                renderObjectDebug.renderer.enabled = true;
            }
            else
            {
                renderObjectDebug.renderer.enabled = false;
            }
        }
    }*/

    public OpticalFlowDataStr processOpticalFlow(int xPos, int yPos, int zoneWidth, int zoneHeight, float forceThreshold = 0.5f)
    {
        OpticalFlowDataStr pdiDataStr = new OpticalFlowDataStr();
        //print("xPos:: " + xPos + " yPos:: " + yPos + " zoneWidth:: " + zoneWidth + " zoneHeight:: " + zoneHeight);

        if (cameraStarted)
        {
            //variables para limitar zonas de sensado al interior de la imagen
            int minX = xPos;
            int maxX = minX + zoneWidth;
            int minY = yPos;
            int maxY = minY + zoneHeight;

            //Debug.Log("*****::" + " minX: " + minX + " maxX: " + maxX + " zoneWidth: " + zoneWidth + " zoneHeight: " + zoneHeight + " cameraWidth: " + cameraWidth + " cameraHeight: " + cameraHeight);

            if (minX < 0)
            {
                minX = 0;
            }
            if (maxX >= cameraWidth)
            {
                maxX = cameraWidth - 1;
            }
            if (minY < 0)
            {
                minY = 0;
            }
            if (maxY >= cameraHeight)
            {
                maxY = cameraHeight - 1;
            }

            //Debug.LogError("*****::" + " minX: " + minX + " maxX: " + maxX + " zoneWidth: " + zoneWidth + " zoneHeight: " + zoneHeight + " cameraWidth: " + cameraWidth + " cameraHeight: " + cameraHeight);
            //line.points2.Clear();
            //lineColors.Clear();

            int winSize = 8;
            int winStep = winSize * 2 + 1;
                
            int i, j, k, l;
            int imagePosition;
                
            int gradX, gradY, gradT;
            float A2, A1B2, B1, C1, C2;
            float u, v, uu, vv;
            int n;

            int wmax = maxX - winSize - 1;
            int hmax = maxY - winSize - 1;
            minX += winSize;
            minY += winSize;
  
            uu = vv = n = 0;

            //int lin = 0;

            //print("         minX:: " + minX + " minY:: " + minY + " wmax:: " + wmax + " hmax:: " + hmax + " image:: " + image.Length);
            
                /*Boolean m = true;
                int x = minX;
                int y = minY;*/

                try
                {
                    for (i = minY + 1; i < hmax; i += winStep)
                    { // y
                        for (j = minX + 1; j < wmax; j += winStep)
                        { // x
                            /*while (true)
                            {
                                if (m) x += winStep; else x -= winStep;*/

                            A2 = 0;
                            A1B2 = 0;
                            B1 = 0;
                            C1 = 0;
                            C2 = 0;

                            // = y * cameraWidth + x;

                            for (k = -winSize; k <= winSize; k++)
                            { // y
                                for (l = -winSize; l <= winSize; l++)
                                { // x
                                    imagePosition = (i + k) * cameraWidth + j + l;

                                    gradX = (image[imagePosition - 1].r) - (image[imagePosition + 1].r);
                                    gradY = (image[imagePosition - cameraWidth].r) - (image[imagePosition + cameraWidth].r);
                                    gradT = (imageprev[imagePosition].r) - (image[imagePosition].r);

                                    A2 += gradX * gradX;
                                    A1B2 += gradX * gradY;
                                    B1 += gradY * gradY;
                                    C2 += gradX * gradT;
                                    C1 += gradY * gradT;
                                }
                            }


                            float delta = (A1B2 * A1B2 - A2 * B1);

                            if (delta != 0)
                            {
                                /* system is not singular - solving by Kramer method */
                                float deltaX;
                                float deltaY;
                                float Idelta = 8 / delta;

                                deltaX = -(C1 * A1B2 - C2 * B1);
                                deltaY = -(A1B2 * C2 - A2 * C1);

                                u = deltaX * Idelta;
                                v = deltaY * Idelta;

                            }
                            else
                            {
                                /* singular system - find optical flow in gradient direction */
                                float Norm = (A1B2 + A2) * (A1B2 + A2) + (B1 + A1B2) * (B1 + A1B2);

                                if (Norm != 0)
                                {
                                    float IGradNorm = 8 / Norm;
                                    float temp = -(C1 + C2) * IGradNorm;

                                    u = (A1B2 + A2) * temp;
                                    v = (B1 + A1B2) * temp;

                                }
                                else
                                {
                                    u = v = 0;
                                }
                            }

                            if (-winStep < u && u < winStep && -winStep < v && v < winStep)
                            {
                                uu += u;
                                vv += v;
                                n++;
                                /*line.points2.Add(new Vector2(j, i));
                                line.points2.Add(new Vector2(j + u * 1, i + v * 1));
                                lineColors.Add(EditorGUIUtility.HSVToRGB((float)(Math.Atan2(v, u) * toDegree) / 360f, 1f, 1f));
                                line.Draw();
                                line.SetColors(lineColors);*/
                                //print("j:: " + j + " i:: " + i + " u:: " + (j + u * 3) + " v:: " + (i + v * 3));
                                /*line.points2[lin] = new Vector2(j, i);
                                lin++;
                                line.points2[lin] = new Vector2(j + u * 3, i + v * 3);*/
                            }

                            /*if (x == maxX || x == minX)
                            {
                                if (y++ == maxY)
                                    break;
                                m = !m;
                            }*/
                        }
                    }
                }
                catch (Exception e) { Debug.LogWarning("ERROR::" + e.Message); }

                uu /= n;
                vv /= -n;
                
                double a = Math.Atan2(vv, uu) * toDegree;

                pdiDataStr.dir = (float)a;
                pdiDataStr.xForce = uu * 1;
                pdiDataStr.yForce = vv * 1;
                pdiDataStr.force = (float)(Math.Sqrt(uu * uu + vv * vv) * 1f);
                pdiDataStr.isActive = pdiDataStr.force > forceThreshold;

            //print("xFor:: " + pdiDataStr.xForce + " yFor:: " + pdiDataStr.yForce + " For:: " + pdiDataStr.force);

            //print("Dir:: "+a);
        }
        else
        {
            pdiDataStr.isActive = false;
            pdiDataStr.dir = 0;
            pdiDataStr.xForce = 0;
            pdiDataStr.yForce = 0;
            pdiDataStr.force = 0;
        }

        return pdiDataStr;
    }

    void PDI()
    {
        if (webcamTexture.didUpdateThisFrame)   //Condicion para determinar si hay un nuevo frame disponible
        {
            image.CopyTo(imageprev, 0);     //Copia del frame anterior de la camara
            webcamTexture.GetPixels32(image);    //Obtener el frame actual de la camara
            //image = webcamTexture.GetPixels32();    //Obtener el frame actual de la camara

            /*if (isDebug)    //mostrar imagen con pixeles de movimiento para depuracion
            {
                if (!renderObjectDebug.GetComponent<Renderer>().enabled)
                {
                    renderObjectDebug.GetComponent<Renderer>().enabled = true;
                }

                TextureDebug.SetPixels(imageDebug, 0);
                TextureDebug.Apply(false);
                matdebug.mainTexture = TextureDebug;

            }
            else
            {
                if (renderObjectDebug.GetComponent<Renderer>().enabled)
                {
                    renderObjectDebug.GetComponent<Renderer>().enabled = false;
                }
            }*/
        }
    }

    void Update()
    {
        PDI();
        //opticalFlowDataStr_P1 = processOpticalFlow(0, 0, cameraWidth/2, cameraHeight, _threshold);
        //opticalFlowDataStr_P1 = processOpticalFlow(24 , 19 , 281 , 216, _threshold);

        opticalFlowDataStr_P1 = processOpticalFlow((int)x1, (int)y1, (int)w1, (int)h1, _threshold);
        opticalFlowDataStr_P2 = processOpticalFlow((int)x2, (int)y2, (int)w2, (int)h2, _threshold);
        if (opticalFlowDataStr_P1.isActive)
        {
            rotEuler_P1.z = opticalFlowDataStr_P1.dir;
            locScale_P1.x = opticalFlowDataStr_P1.force;

            arrow_P1.transform.eulerAngles = rotEuler_P1;
            arrow_P1.transform.localScale = locScale_P1;
        }
        else
        {
            //print("xFor:: " + pdiDataStr0.xForce + " yFor:: " + pdiDataStr0.yForce + " For:: " + pdiDataStr0.force);
            locScale_P1.x = 1;
            arrow_P1.transform.localScale = locScale_P1;
        }

        if (opticalFlowDataStr_P2.isActive)
        {
            rotEuler_P2.z = opticalFlowDataStr_P2.dir;
            locScale_P2.x = opticalFlowDataStr_P2.force;

            arrow_P2.transform.eulerAngles = rotEuler_P2;
            arrow_P2.transform.localScale = locScale_P2;
        }
        else
        {
            //print("xFor:: " + pdiDataStr0.xForce + " yFor:: " + pdiDataStr0.yForce + " For:: " + pdiDataStr0.force);
            locScale_P2.x = 1;
            arrow_P2.transform.localScale = locScale_P2;
        }
        //KeyboardInputs();
    }

    public void stop()
    {
        webcamTexture.Stop();
    }

    void OnApplicationQuit()
    {
        print("OpticalFlow::::OnApplicationQuit");
        webcamTexture.Stop();
    }

    void OnDisable()
    {

    }
}
