using UnityEngine;

public class Inputs
{
	[HideInInspector] public float horizontalIntensity = 0;
	[HideInInspector] public float verticalIntensity = 0;

	public void Check() {
		horizontalIntensity = Input.GetAxisRaw("Horizontal");
		verticalIntensity = Input.GetAxisRaw("Vertical");
	}
}
