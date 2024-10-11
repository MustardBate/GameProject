using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    private bool isUIHidden = false;
    [Header("Pause Menu")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject popUpMenu;
    [SerializeField] private GameObject returnPopUp;
    [SerializeField] private GameObject titlePopUp;

    [Space(15)]
    [Header("Settings")]
    [SerializeField] private Image BulletFrame;
    [SerializeField] private GameObject BulletCount;
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject PlayerStats;
    [SerializeField] private GameObject MoneyCount;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
        {
            if (isPaused)
            {
                canvas.SetActive(true);
                Time.timeScale = 0;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
                GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>().enabled = false;
                isPaused = false;
            }

            else 
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(true);
        popUpMenu.SetActive(false);
        returnPopUp.SetActive(false);
        titlePopUp.SetActive(false);
        optionMenu.SetActive(false);
        canvas.SetActive(false);
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>().enabled = true;
        isPaused = true;
    }


    public void HideUI()
    {
        if(!isUIHidden)
        {
            BulletFrame.enabled = false;
            BulletCount.GetComponent<CanvasGroup>().alpha = 0;
            HealthBar.GetComponent<CanvasGroup>().alpha = 0;
            PlayerStats.GetComponent<CanvasGroup>().alpha = 0;
            MoneyCount.GetComponent<CanvasGroup>().alpha = 0;
            isUIHidden = true;
        }

        else 
        {
            BulletFrame.enabled = true;
            BulletCount.GetComponent<CanvasGroup>().alpha = 1;
            HealthBar.GetComponent<CanvasGroup>().alpha = 1;
            PlayerStats.GetComponent<CanvasGroup>().alpha = .45f;
            MoneyCount.GetComponent<CanvasGroup>().alpha = 1;
            isUIHidden = false;
        }
    }
}
