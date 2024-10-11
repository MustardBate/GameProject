using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private Animator sceneTransition;

    public void MoveToNextScene()
    {
        Time.timeScale = 1;
        StartCoroutine(NextScene());
    }

    public void ReturnToHub()
    {
        Time.timeScale = 1;
        StartCoroutine(ReturnMainHub());
    }

    public void ReturnToTitleScreen()
    {
        Time.timeScale = 1;
        StartCoroutine(ReturnTitle());
    }

    public void GameOver()
    {
        Time.timeScale = 1;
        StartCoroutine(DeathScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private IEnumerator NextScene()
    {
        sceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator ReturnTitle()
    {
        sceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Title Screen");
    }

    private IEnumerator ReturnMainHub()
    {
        sceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Main Hub");
    }

    private IEnumerator DeathScene()
    {
        sceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Game Over");
    }
}
