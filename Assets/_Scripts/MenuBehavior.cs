using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public GameObject mmGO;
    public GameObject contGO;
    public GameObject tipsGO;

    public void GoToControls()
    {
        mmGO.SetActive(false);
        contGO.SetActive(true);
    }

    public void GoToTips()
    {
        contGO.SetActive(false);
        tipsGO.SetActive(true);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
