using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private Animator sceneTransition;

    public void MoveToNextScene()
    {
        StartCoroutine(NextScene());
    }

    public void ReturnToTitleScreen()
    {
        StartCoroutine(ReturnTitle());
    }

    public void ReturnToGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
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
}
