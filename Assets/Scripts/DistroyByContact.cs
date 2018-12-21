using UnityEngine;

public class DistroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject boltExplosion;
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
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        _gameController.IncreaseAsteroidDestroied();

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

            _gameController.GameOver();
        }

        if (other.CompareTag("SpaceShipBolt"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
