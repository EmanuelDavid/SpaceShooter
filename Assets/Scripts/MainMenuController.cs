using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        GameObject searchedGameController = GameObject.FindWithTag("GameController");
        if (searchedGameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        else
        {
            _gameController = searchedGameController.GetComponent<GameController>();
        }
    }

    public void Restart()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
   //     _gameController.GameRunning = true;
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void ActivateGyroscope()
    {
        _gameController.ActivateGyroscope();
    }

    public void DeactivateGyroscope()
    {
        _gameController.DeactivateGyroscope();
    }

    public void HideMainMenu()
    {
        _gameController.HideMainMenu();
    }
}
