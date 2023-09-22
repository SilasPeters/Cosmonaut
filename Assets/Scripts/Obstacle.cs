using System;
using System.Linq;
using Unity_Essentials.Static;
using UnityEngine;

public class Obstacle : Gravity
{
	public bool isDestination;

	public override void Start()
	{
		base.Start();

		// Set the mass proportional to the diameter of the object
		var sphereColliders = GetComponents<SphereCollider>();
		var sphereCollider  = sphereColliders.First(x => !x.isTrigger);
		var rigidbody       = GetComponent<Rigidbody>();
		rigidbody.mass = sphereCollider.radius * transform.localScale.x * 2; // This assumes that the scale is universal to all dimensions

		print($"Gameobject: {gameObject.name} has mass: {rigidbody.mass}");
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name != "SpaceShip") return;

		if (isDestination)
		{
			StartCoroutine(Singleton<Intermezzo>.Instance.EndOfLevel());
		}
		else
		{
			throw new NotImplementedException("Player died");
		}
	}
}