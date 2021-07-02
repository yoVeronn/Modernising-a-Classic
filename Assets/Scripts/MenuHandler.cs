using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject brefinsMenu;

    private void Awake()
    {
        mainMenu.SetActive(true);
        brefinsMenu.SetActive(false);

        Time.timeScale = 1.0f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BreifingMenu()
    {
        mainMenu.SetActive(false);
        brefinsMenu.SetActive(true);
    }

    public void BreifingMenuBack()
    {
        mainMenu.SetActive(true);
        brefinsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
