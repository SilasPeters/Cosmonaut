using Unity_Essentials.Static;
using UnityEngine;

public class SpaceShip : Singleton<MonoBehaviour>
{
    public GameObject homePlanet;
    public float launchForce;
    public bool Launched { get; private set; }

    private Rigidbody _rigidbody;
    private GameObject _pointer;

    protected override void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _pointer   = GameObject.Find("Pointer");
    }

    void Update()
    {
        // Don't take input when the intermezzo is playing
        if (Singleton<Intermezzo>.Instance.IsPLaying) return;

        if (!Launched)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow))
            {
                homePlanet.transform.Rotate(0, -1, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                homePlanet.transform.Rotate(0, 1, 0);
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
        }
    }

    private void Launch()
    {
        _rigidbody.AddForce(transform.forward * launchForce, ForceMode.VelocityChange);
        _pointer.SetActive(false);
    }
}
