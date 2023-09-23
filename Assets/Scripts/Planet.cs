using System.Linq;
using UnityEngine;

public class Planet : Gravity
{
	public override void Start()
	{
		base.Start();

		// Set the mass proportional to the diameter of the object
		var sphereColliders = GetComponents<SphereCollider>();
		var sphereCollider  = sphereColliders.First(x => !x.isTrigger);
		var rigidbody       = GetComponent<Rigidbody>();
		rigidbody.mass = sphereCollider.radius * transform.localScale.x * 2; // This assumes that the scale is universal to all dimensions
	}
}