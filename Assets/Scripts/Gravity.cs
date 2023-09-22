using System.Collections.Generic;
using Unity_Essentials.Static;
using UnityEngine;

public class Gravity : MonoBehaviour
{
	private static float GravitationalConstant => Singleton<GameManager>.Instance.gravitationalConstant;

	private Rigidbody _rigidbody;
	private readonly List<Gravity> _attractedObjects = new();

	public virtual void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_attractedObjects.AddRange(FindObjectsOfType<Gravity>());
		_attractedObjects.Remove(GetComponent<Gravity>());
	}

	private void FixedUpdate()
	{
		if (!Singleton<SpaceShip>.Instance.Launched) return;

		foreach (var gravityObject in _attractedObjects)
			gravityObject.Attract(_rigidbody);
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