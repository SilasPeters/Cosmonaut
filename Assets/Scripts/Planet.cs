using System;
using System.Collections;
using Unity_Essentials.Static;
using UnityEngine;
using UnityEngine.UI;

public class Planet : Gravity
{
	public bool isDestination;

	private SphereCollider _collider;
	private Image _fadeOutImage;

	public override void Start()
	{
		_collider     = GetComponent<SphereCollider>();
		_fadeOutImage = GameObject.Find("White background").GetComponent<Image>();
		base.Start();
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