using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public float spinningSpeed;

    void Update()
    {
        transform.Rotate(0, spinningSpeed * Time.deltaTime, 0);
    }
}
