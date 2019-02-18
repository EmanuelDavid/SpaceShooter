using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundries
{
    public float xMin = -5, xMax = 5, zMin = -4, zMax = 10;
}

public class SpaceshipController : MonoBehaviour
{
    public float speed = 30;
    public float tilt = 2.5f;
    public Boundries boundries;
    private Text _nrOfShootsText;
    public Transform shotSpan;
    public GameObject projectile;
    public float fireDelta = 0.15F;
    public bool IsMachineGun;

    private ShootAreea _shootArea;
    private int _nrOfShoots = 0;
    private float myTime = 0.0F;
    private Rigidbody _rigidBody;
    //move to game controller
    private AudioSource _shotSound;
    private Quaternion _calibrationQuaternion;
    private GameController _gameController;
    private bool _gyroActivatedFirstTime;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _shotSound = GetComponent<AudioSource>();
        boundries = new Boundries();

        var gameObjectNoShootText = GameObject.FindWithTag("NoShootsText");
        if (gameObjectNoShootText == null)
        {
            Debug.Log("Cannot find 'shoots ' gameObject");
        }
        else
        {
            _nrOfShootsText = gameObjectNoShootText.GetComponent<Text>();
        }


        var gameController = GameObject.FindWithTag("GameController");
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' gameObject");
        }
        else
        {
            _gameController = gameController.GetComponent<GameController>();
            _shootArea = _gameController.ShootArea;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_gameController.IsGyroscopControlled) {
            Vector3 rawAcceleration = Input.acceleration;
            Vector3 acceleration = FixAcceleration(rawAcceleration);

            float moveHorizontal = acceleration.x;
            float moveVertical = acceleration.y;

            var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            _rigidBody.velocity = movement * speed;
        }

        _rigidBody.position = new Vector3
            (
            Mathf.Clamp(_rigidBody.position.x, boundries.xMin, boundries.xMax),
            0.0f,
            Mathf.Clamp(_rigidBody.position.z, boundries.zMin, boundries.zMax)
            );

        _rigidBody.rotation = Quaternion.Euler(0, 0, _rigidBody.velocity.x * -tilt);
    }

    private void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (_shootArea.CanFire() && (myTime - fireDelta) > 0)
        {
            Instantiate(projectile, shotSpan.position, shotSpan.rotation);
            _shotSound.Play();

            _nrOfShoots++;
            _nrOfShootsText.text = "Shoots: " + _nrOfShoots;

            myTime = 0.0F;
        }
    }

    //Get the 'calibrated' value from the Input
    private Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = _gameController.CalibratedDeviceQuaternion * acceleration;
        return fixedAcceleration;
    }

}
