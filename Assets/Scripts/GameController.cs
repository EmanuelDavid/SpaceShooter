using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 asteroidFrom;
    public float startWait;
    public float waveWait;
    public float spawnWait;
    public int hazardCount;
    public Text gameOverText;
    public Text nrAsteroidsDead;

    private GameObject _restartButton;
    private bool _gameOver;
    private int _noAsteroidsDestroyed = 0;

    private void Start()
    {
        gameOverText.text = "";
        StartCoroutine(SpawnWaves());

        _restartButton = GameObject.FindWithTag("RestartButton");
        if (_restartButton == null)
        {
            Debug.Log("Cannot find 'RestartButton' script");
        }
        else
        {
            _restartButton.SetActive(false);
        }
    }


    void Update()
    {
        if (_gameOver)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-asteroidFrom.x, asteroidFrom.x), asteroidFrom.y, asteroidFrom.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (_gameOver)
            {
                _restartButton.SetActive(true);
                break;
            }
        }
    }

    public void GameOver()
    {
        _gameOver = true;
        gameOverText.text = "Game Over!";
    }

    public void IncreaseAsteroidDestroied()
    {
        _noAsteroidsDestroyed++;
        nrAsteroidsDead.text = "Asteroids destroyed: " + _noAsteroidsDestroyed;
    }
}
