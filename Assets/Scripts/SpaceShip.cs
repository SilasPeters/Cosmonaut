using UnityEngine;
using Submodules.Unity_Essentials.Static;

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
        _pointer   = this.gameobject.children["Pointer"];
    }

    void Update()
    {
        if (!Launched)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                homePlanet.transform.Rotate(0, -1, 0);
            if (Input.GetKey(KeyCode.DownArrow))
                homePlanet.transform.Rotate(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
            Launched = true;
        }
    }

    private void Launch()
    {
        _rigidbody.AddForce(transform.forward * launchForce, ForceMode.VelocityChange);
        _pointer.SetActive(false);
    }
}
