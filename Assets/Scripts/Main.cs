using UnityEngine;

public class Main : MonoBehaviour
{
	[SerializeField] Camera mainCamera;
	[SerializeField] Light mainLight;
	Settings settings;

	private void Awake() {
		settings = FindObjectOfType<Settings>();
	}

	public Settings Camera { get { return settings; } }
	public Settings Light { get { return settings; } }
	public Settings Settings { get { return settings; } }
}
