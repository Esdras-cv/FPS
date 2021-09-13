using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class GameController : MonoBehaviour, ISaveable
{
    public Text missaoTxt;
    public Text missaoConcluida;
    public Image healthI;
    public Image healthII;
    public Image healthIII;
    public Image healthIV;
    public Image healthV;
    public int tesseractCount = 0;
    public GameObject portal;
    public GameObject escada;

    private int health = 5;
    private int mission = 1;
    private string scene = "Fase1";
    private static GameController instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }               
    }

    // Start is called before the first frame update
    void Start()
    {
        missaoConcluida.enabled = false;
        portal.SetActive(false);
        escada.SetActive(false);
        LoadJsonData(instance);
    }

    // Update is called once per frame
    void Update()
    {        
        Debug.Log(mission);
        Debug.Log(scene);

        switch(mission)
        {
            case 1: missaoTxt.text = "Destrua todas as réplicas do Tesseract: " + tesseractCount + " de 10";
                if(tesseractCount == 10)
                {
                    tesseractCount = 0;
                    missaoTxt.enabled = false;
                    missaoConcluida.enabled = true;           
                    Invoke("NextMission", 2f);          
                }
            break;
            case 2: missaoTxt.text = "Atravesse o portal no topo";
                portal.SetActive(true);
            break;
            case 3: missaoTxt.text = "Destrua todas as réplicas do Tesseract: " + tesseractCount + " de 10";
                portal.SetActive(false);

                if(tesseractCount == 10)
                {
                    tesseractCount = 0;
                    missaoTxt.enabled = false;
                    missaoConcluida.enabled = true; 
                          
                    Invoke("NextMission", 2f);          
                }
            break;
            case 4: missaoTxt.text = "Atravesse o portal no topo";
                portal.SetActive(true);
                escada.SetActive(true);
            break;
            default: missaoTxt.text = "MISSÃO";
            break;
        }        
    }

    public void NextMission()
    {
        mission++;
        missaoConcluida.enabled = false;
        missaoTxt.enabled = true;
    }

    public void OnFall()
    {
        health--;

        switch(health)
        {
            case 0: healthI.enabled = false;
            break;
            case 1: healthII.enabled = false;
            healthI.color = new Color(255, 0, 0, 255);
            break;
            case 2: healthIII.enabled = false;
            healthI.color = new Color(255, 155, 0, 255);
            healthII.color = new Color(255, 155, 0, 255);
            break;
            case 3: healthIV.enabled = false;
            healthI.color = new Color(255, 255, 0, 255);
            healthII.color = new Color(255, 255, 0, 255);
            healthIII.color = new Color(255, 255, 0, 255);
            break;
            case 4: healthV.enabled = false;
            healthI.color = new Color(155, 255, 0, 255);
            healthII.color = new Color(155, 255, 0, 255);
            healthIII.color = new Color(155, 255, 0, 255);
            healthIV.color = new Color(155, 255, 0, 255);
            break;
        }
    }
    public void NextFase()
    {
        if(mission == 4 || scene == "Fase2")
        {
            mission = 1;
            scene = "Fase1";

        }
        else
        {
            NextMission(); 
            scene = "Fase2";
        }

        portal.SetActive(false);
        escada.SetActive(false);

        SaveJsonData(instance);
        LoadJsonData(instance);
    }

    void OnDestroy()
    {
        SaveJsonData(instance);
    }

    public static void SaveJsonData(GameController gameController)
    {
        SaveData saveData = new SaveData();
        gameController.PopulateSaveData(saveData);

        if(FileManager.WriteToFile("SaveData.dat", saveData.ToJson()))
        {
            Debug.Log("Jogo salvo");
        }
    }

    public static void LoadJsonData(GameController gameController)
    {
        if(FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData saveData = new SaveData();
            saveData.LoadFromJson(json);

            gameController.LoadFromSaveData(saveData);
            Debug.Log("Jogo carregado");
        }
        else
        {
            SaveJsonData(instance);
        }
    }

    public void PopulateSaveData(SaveData saveData)
    {
        saveData.mission = mission;
        saveData.scene = scene;
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        mission = saveData.mission;
        scene = saveData.scene;

        SceneManager.LoadScene(scene);
    }
}
