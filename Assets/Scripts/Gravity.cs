using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
	private const float GravitationalConstant = 1;

	private Rigidbody _rigidbody;
	/// <summary> All other objects that are currently in the sphere of influence of this object. </summary>
	private readonly List<Gravity> _attractedObjects = new();

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void OnTriggerEnter(Collider other)
	{
		Gravity gravityObject = other.GetComponent<Gravity>();
		if (gravityObject == null) return;
		_attractedObjects.Add(gravityObject);
	}

	private void OnTriggerExit(Collider other)
	{
		Gravity gravityObject = other.GetComponent<Gravity>();
		if (gravityObject == null) return;
		_attractedObjects.Remove(gravityObject);
	}

	private void FixedUpdate()
	{
		foreach (Gravity gravityObject in _attractedObjects)
		{
			gravityObject.Attract(_rigidbody);
		}
	}

	private void Attract(Rigidbody attractedRigidbody)
	{
		// Calculate the direction vector from this object to the attracted object
		Vector3 direction = transform.position - attractedRigidbody.transform.position;
		float   distance  = direction.magnitude;

		// F = G * m1 * m2 / r^2 (Newton's law of universal gravitation)
		// Source: https://en.wikipedia.org/wiki/Newton%27s_law_of_universal_gravitation
		float forceMagnitude = GravitationalConstant * _rigidbody.mass * attractedRigidbody.mass / Mathf.Pow(distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;
		attractedRigidbody.AddForce(force);
	}
}