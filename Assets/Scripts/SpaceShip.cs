using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public GameObject homePlanet;
    public float launchForce;

    private Rigidbody _rigidbody;
    private bool _launched;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!_launched)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("Left");
                homePlanet.transform.Rotate(0, 0, 1);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("Right");
                homePlanet.transform.Rotate(0, 0, -1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            Launch();
            _launched = true;
        }
    }

    private void Launch()
    {
        _rigidbody.AddForce(transform.up * launchForce, ForceMode.Acceleration);
    }
}
