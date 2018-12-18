using UnityEngine;

public class DistroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    private GameController _gameController;

    private void Start()
    {
        GameObject searchedGameController = GameObject.FindWithTag("GameController");
        if(searchedGameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        else
        {
            _gameController = searchedGameController.GetComponent<GameController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        _gameController.IncreaseAsteroidDestroied();

        Instantiate(explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            _gameController.GameOver();
        }



        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
