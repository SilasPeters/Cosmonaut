using Unity_Essentials.Static;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	private AudioSource _audioSource;

	protected void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_audioSource.Play();
		DontDestroyOnLoad(gameObject);
	}
}