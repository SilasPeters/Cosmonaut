using System.Collections;
using Unity_Essentials.Static;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
	public bool isDestination;

	private Image _deathScreen;

	private void Start()
	{
		_deathScreen = GameObject.Find("Death").GetComponent<Image>();
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