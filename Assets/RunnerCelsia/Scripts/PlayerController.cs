using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float x;
    public float y;

    public static float speedX;

    public GameObject player;

    public PatternSystem patternSystem;

    private XmlDocument xDoc;
    private float sensibilidad = 1;

    void ReadXML()
    {

        xDoc = new XmlDocument();
        string path1 = @"" + Application.dataPath + "/Config/config.xml";
        print("ReadXML:: " + path1);

        try
        {
            xDoc.Load(path1);
            XmlNodeList nodes = xDoc.GetElementsByTagName("Config");

            XmlNodeList nodes0 = nodes.Item(0).ChildNodes;
            Debug.Log("name: " + nodes0[0].Name + " value: " + nodes0[0].InnerText);

            sensibilidad = float.Parse(nodes0[0].InnerText);
        }
        catch (System.Exception e)
        {
            Debug.Log("Archivo XML no Encontrado " + e.Message);

            string file = "<CelsiaRunner>\n"
                        + " <Config>\n"
                        + "     <sensibilidad>" + sensibilidad + "</sensibilidad>\n"
                        + " </Config>\n"
                        + "</CelsiaRunner>";

            //Si no existe creamos en directorio donde vamos a guardar el archivo
            if (!Directory.Exists(Application.dataPath + "/Config/"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Config/");
            }

            using (StreamWriter sw = new StreamWriter(Application.dataPath + "/Config/config.xml"))
            {

                sw.Write(file);

                sw.Close();

                Debug.Log("Archivo Generado");
            }
        }

    }

    private void Awake()
    {
        ReadXML();
    }

    private void Start()
    {
        speedX = 6;
        sensibilidad = CanvasOpciones.sens;
    }

    void Update()
    {
        if(patternSystem.loadingComplete)
            x = (Input.GetAxis("Horizontal") * sensibilidad) ;

        if (x != 0)
        {
            player.transform.Translate(0, 0, ((x * speedX) * Time.deltaTime));
        }

        player.transform.eulerAngles = new Vector3(0,((x * y)/4) + 90, 0);
        
        if(player.transform.position.y != 0.055f)
        {
            player.transform.position = new Vector3(player.transform.position.x, 0.055f, player.transform.position.z);
        }

        if (player.transform.position.x < -1.6f)
            player.transform.position = new Vector3(-1.6f, player.transform.position.y, player.transform.position.z);

        if (player.transform.position.x > 1.6f)
            player.transform.position = new Vector3(1.6f, player.transform.position.y, player.transform.position.z);

    }
}
