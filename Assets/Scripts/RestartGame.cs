using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene

        GameObject restartButtonSearched = GameObject.FindWithTag("RestartButton");
        if (restartButtonSearched == null)
        {
            Debug.Log("Cannot find 'RestartButton' script");
        }
        else
        {
            restartButtonSearched.SetActive(false);
        }
    }
}
