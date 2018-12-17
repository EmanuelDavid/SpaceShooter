using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundries
{
    public float xMin = -5, xMax = 5, zMin = -4, zMax = 10;
}

public class SpaceshipController : MonoBehaviour
{
    public float speed = 20;
    public float tilt = 2.5f;
    public Boundries boundries;
    public Text nrOfShootsText;
    public Transform shotSpan;
    public GameObject projectile;
    public float fireDelta = 0.15F;
    public bool IsMachineGun;

    private int _nrOfShoots = 0;
    private float myTime = 0.0F;
    private Rigidbody _rigidBody;
    private AudioSource _shotSound;
    private Quaternion _calibrationQuaternion;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _shotSound = GetComponent<AudioSource>();
        boundries = new Boundries();
        //CalibrateAccelerometer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Vector3 rawAcceleration = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(rawAcceleration);

        //var position = Input.GetTouch(0).position;
        //Debug.Log(position);

        //var movement = new Vector3(position.x, 0.0f, position.y);

        //_rigidBody.velocity = movement * speed;

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

        var shootType = IsMachineGun ? MachineGun() : NormalShoot();

        if (shootType && (myTime - fireDelta) > 0)
        {
            Instantiate(projectile, shotSpan.position, shotSpan.rotation);
            _shotSound.Play();

            _nrOfShoots++;
            nrOfShootsText.text = "Shoots: " + _nrOfShoots;

            myTime = 0.0F;
        }
    }

    private bool MachineGun()
    {
        if (Input.touchCount > 0)
        {
            return true;
        }
        return false;
    }

    private bool NormalShoot()
    {
        if (MachineGun())
        {
            var touchPase = Input.GetTouch(0).phase;
            if (touchPase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }

    //Used to calibrate the Input.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        _calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = _calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

}
