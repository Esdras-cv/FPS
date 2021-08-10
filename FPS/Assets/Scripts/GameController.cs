using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
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

    private int health = 5;
    private int mission = 1;
    // Start is called before the first frame update
    void Start()
    {
        missaoConcluida.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mission);
        switch(mission)
        {
            case 1: missaoTxt.text = "Destrua todas as réplicas do Tesseract: " + tesseractCount + " de 10";
            if(tesseractCount == 10)
            {
                tesseractCount = 0;
                missaoTxt.enabled = false;
                missaoConcluida.enabled = true; 
                portal.SetActive(true);           
                Invoke("NextMission", 3f);          
            }
            break;
            case 2: missaoTxt.text = "Atravesse o portal";
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
        missaoTxt.enabled = false;
        missaoConcluida.enabled = true; 
        portal.SetActive(true);           
        SceneManager.LoadScene("Fase2");
    }
}
