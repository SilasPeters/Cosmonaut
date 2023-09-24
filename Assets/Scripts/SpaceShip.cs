using Unity_Essentials.Static;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : Singleton<MonoBehaviour>
{
    public float launchForce;
    public float thrustSpeed;
    public float amountOfFuel;
    public float sensitivity;
    public bool Launched { get; private set; }
    public Image fuelIndicator;

    private float _fuelUsed;
    private Vector3 _originalIndicatorScale;
    private Rigidbody _rigidbody;
    private GameObject _pointer;
    private Transform _pivotTransform;

    private ParticleSystem _thrusterLeft;
    private ParticleSystem _thrusterRight;
    private AudioSource _thrustSfx;
    private AudioSource _launchSfx;

    protected override void Awake()
    {
        _rigidbody              = GetComponent<Rigidbody>();
        _pointer                = GameObject.Find("Pointer");
        _pivotTransform         = transform.parent.transform;
        _originalIndicatorScale = fuelIndicator.rectTransform.localScale;

        _thrusterLeft = GameObject.Find("Thruster Left").GetComponent<ParticleSystem>();
        _thrusterRight = GameObject.Find("Thruster Right").GetComponent<ParticleSystem>();

        _thrustSfx = GameObject.Find("ThrustSFX").GetComponent<AudioSource>();
        _launchSfx = GameObject.Find("LaunchSFX").GetComponent<AudioSource>();
    }

    void Update()
    {
        // Don't take input when the intermezzo is playing
        if (Singleton<Intermezzo>.Instance.IsPLaying) return;

        if (!Launched)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow))
            {
                _pivotTransform.Rotate(0, -sensitivity, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                _pivotTransform.Rotate(0, sensitivity, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Launch();
                Launched = true;
            }
        }
        else // Spaceship is launched
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Intermezzo.SkipNextIntermezzo = true;
                CustomSceneManager.ReloadScene();
            }

            if (Input.GetKey(KeyCode.LeftArrow) && _fuelUsed < amountOfFuel)
                Thrust(KeyCode.LeftArrow);
            if (Input.GetKey(KeyCode.RightArrow) && _fuelUsed < amountOfFuel)
                Thrust(KeyCode.RightArrow);
        }
    }

    private void Launch()
    {
        _rigidbody.AddForce(transform.forward * launchForce, ForceMode.VelocityChange);
        _pointer.SetActive(false);
        _launchSfx.Play();
    }

    private void Thrust(KeyCode key)
    {
        Vector3 speed;
        switch (key)
        {
            case KeyCode.LeftArrow:
                speed = transform.right * -thrustSpeed;
                if (!_thrusterRight.isPlaying)
                    _thrusterRight.Play();
                break;
            case KeyCode.RightArrow:
                speed = transform.right * thrustSpeed;
                if (!_thrusterLeft.isPlaying)
                    _thrusterLeft.Play();
                break;
            default:
                throw new System.NotImplementedException();
        }

        _rigidbody.AddForce(speed, ForceMode.Acceleration);
        _fuelUsed += Time.deltaTime;
        fuelIndicator.rectTransform.localScale = new Vector3(
             ((amountOfFuel - _fuelUsed) / amountOfFuel),
            _originalIndicatorScale.y,
            _originalIndicatorScale.z);

        if (!_thrustSfx.isPlaying)
            _thrustSfx.Play();
    }
}
