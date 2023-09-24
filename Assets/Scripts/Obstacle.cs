using System.Collections;
using Unity_Essentials.Static;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
	public bool isDestination;

	private Image _deathScreen;
	/// <summary>
	/// Prevents colission logic from being called multiple times when a ship bounces off and on a planet.
	/// The ship should only collide once - logically speaking.
	/// </summary>
	private bool _shipCollided;

	private void Start()
	{
		_deathScreen = GameObject.Find("Death").GetComponent<Image>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (_shipCollided || other.gameObject.name != "SpaceShip") return;
		_shipCollided = true; // Prevent this logic from being called multiple times

		if (isDestination)
		{
			StartCoroutine(Singleton<Intermezzo>.Instance.EndOfLevel());
		}
		else
		{
			StartCoroutine(PlayerDeath());
		}
	}

	private IEnumerator PlayerDeath()
	{
		yield return HighLevelFunctions.Lerp(.3f, progress =>
		{
			_deathScreen.color = new Color(0, 0, 0, progress);
		});
		Intermezzo.SkipNextIntermezzo = true;
		CustomSceneManager.ReloadScene();
	}
}