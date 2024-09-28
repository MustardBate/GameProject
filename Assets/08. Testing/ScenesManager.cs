using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private Animator sceneTransition;

    public void MoveToNextScene()
    {
        StartCoroutine(NextScene());
    }

    public void ReturnToHub()
    {
        StartCoroutine(ReturnMainHub());
    }

    public void ReturnToTitleScreen()
    {
        StartCoroutine(ReturnTitle());
    }

    public void GameOver()
    {
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
