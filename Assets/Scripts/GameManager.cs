using Unity_Essentials.Static;
using UnityEngine;

public class GameManager : Singleton<MonoBehaviour>
{
	public static int IntermezzoIndex = -1;
	public float gravitationalConstant;

	/// <inheritdoc />
	protected override void Awake()
	{

	}
}