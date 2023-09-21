using System.Collections;
using TMPro;
using Unity_Essentials.Static;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity_Essentials.Static.HighLevelFunctions;

public class Intermezzo : Singleton<MonoBehaviour>
{
	public string[] intermezzo;
	public Image background;
	public TextMeshProUGUI loreText;
	public TextMeshProUGUI pressKeyText;

	public bool IsPLaying { get; private set; }

	private static bool _skipNextIntermezzo;

	public static bool SkipNextIntermezzo
	{
		get
		{
			var val = _skipNextIntermezzo;
			_skipNextIntermezzo = false;
			return val;
		}
		set => _skipNextIntermezzo = value;
	}

	/// <inheritdoc />
	protected override void Awake()
	{
		IsPLaying = true;
	}

	private void Start()
	{
		if (SkipNextIntermezzo)
		{
			background.color = Color.clear;
			IsPLaying        = false;
			return;
		}

		StartCoroutine(PlayIntermezzo());
	}

	public IEnumerator EndOfLevel()
	{
		const float fadeOutDuration = 1;

		yield return Lerp(fadeOutDuration, progress =>
		{
			background.color = new Color(1, 1, 1, progress);
		});

		CustomSceneManager.LoadNextScene();
	}

	private IEnumerator PlayIntermezzo()
	{
		yield return new WaitForSeconds(1);
		foreach (var line in intermezzo)
		{
			loreText.text = line.Replace(";", "\n\n");
			yield return ShowText(2);
			yield return ShowPressKeyPrompt(.5f);
			yield return new WaitUntil(() => Input.anyKeyDown);
			yield return HidePressKeyPrompt(.3f);
			yield return HideText(1);
		}
		yield return StartLevel(2);
		IsPLaying = false;
	}

	private IEnumerator ShowText(float duration) =>
		Lerp(duration, progress =>
		{
			loreText.color = new Color(0, 0, 0, progress);
		});

	private IEnumerator HideText(float duration) =>
		Lerp(duration, progress =>
		{
			loreText.color = new Color(0, 0, 0, 1 - progress);
		});

	private IEnumerator ShowPressKeyPrompt(float duration) =>
		Lerp(duration, progress =>
		{
			pressKeyText.color = new Color(0, 0, 0, progress);
		});

	private IEnumerator HidePressKeyPrompt(float duration) =>
		Lerp(duration, progress =>
		{
			pressKeyText.color = new Color(0, 0, 0, 1 - progress);
		});

	private IEnumerator StartLevel(float duration)
		=> Lerp(duration, progress =>
		{
			background.color = new Color(1, 1, 1, 1 - progress);
		});
}