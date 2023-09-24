using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	private AudioSource _audioSource;
	private static MusicPlayer _instance;

	protected void Awake()
	{
		if(_instance != null && _instance != this)
		{
			Destroy(gameObject); // There already is a music player
			return;
		}
		_instance = this;

		Debug.Log("This is the music player");
		_audioSource = GetComponent<AudioSource>();
		_audioSource.Play();
		DontDestroyOnLoad(gameObject);
	}
}