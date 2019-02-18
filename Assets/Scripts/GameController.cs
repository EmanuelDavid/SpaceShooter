using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 asteroidFrom;
    public float startWait;
    public float waveWait;
    public float spawnWait;
    public int hazardCount;
    public Text nrAsteroidsDead;
    public GameObject mainMenu;
    public GameObject PlayerShip;
    public bool IsGyroscopControlled;
    public ShootAreea ShootArea;
    public GameObject CurrentPlayerShip;
    //try to make it protecte, or private
    public Quaternion CalibratedDeviceQuaternion;


    public GameObject gameOverText;
    private bool _gameOver;
    private bool _canShowMenu;
    private int _noAsteroidsDestroyed = 0;

    private void Update()
    {
        if (_canShowMenu)
        {
            mainMenu.SetActive(true);
        }
    }

    IEnumerator HideGameOverTextCountdown()
    {
        float duration = 5f;

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        gameOverText.SetActive(false);
        _canShowMenu = true;
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-asteroidFrom.x, asteroidFrom.x), asteroidFrom.y, asteroidFrom.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (_gameOver)
            {
                yield break;
            }
        }
    }

    public void GameOver()
    {
        _gameOver = true;
        gameOverText.SetActive(true);

        StartCoroutine(HideGameOverTextCountdown());
    }

    public void StartGame()
    {
        HideMainMenu();
        _gameOver = false;

        CurrentPlayerShip = Instantiate(PlayerShip, new Vector3(0,0,0), Quaternion.identity);
        StartCoroutine(SpawnWaves());
    }

    public void IncreaseAsteroidDestroied()
    {
        _noAsteroidsDestroyed++;
        nrAsteroidsDead.text = "Asteroids destroyed: " + _noAsteroidsDestroyed;
    }

    public void HideMainMenu()
    {
        _canShowMenu = false;
    }

    //Used to calibrate the Input.acceleration input
    private void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        CalibratedDeviceQuaternion =  Quaternion.Inverse(rotateQuaternion);
    }

    public void ActivateGyroscope()
    {
        IsGyroscopControlled = true;
        CalibrateAccelerometer();
    }

    public void DeactivateGyroscope()
    {
        IsGyroscopControlled = false;
    }

}
