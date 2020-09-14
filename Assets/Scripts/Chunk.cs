using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public void DebugBounds(float chunkSize)
	{
		float x = transform.position.x;
		float y = transform.position.y;

		Debug.DrawLine(
			new Vector3(x * chunkSize - chunkSize / 2, 0, y * chunkSize - chunkSize / 2),
			new Vector3(x * chunkSize + chunkSize / 2, 0, y * chunkSize - chunkSize / 2)
		, Color.red, Mathf.Infinity);
		Debug.DrawLine(
			new Vector3(x * chunkSize + chunkSize / 2, 0, y * chunkSize - chunkSize / 2),
			new Vector3(x * chunkSize + chunkSize / 2, 0, y * chunkSize + chunkSize / 2)
		, Color.red, Mathf.Infinity);
		Debug.DrawLine(
			new Vector3(x * chunkSize - chunkSize / 2, 0, y * chunkSize + chunkSize / 2),
			new Vector3(x * chunkSize + chunkSize / 2, 0, y * chunkSize + chunkSize / 2)
		, Color.red, Mathf.Infinity);
		Debug.DrawLine(
			new Vector3(x * chunkSize - chunkSize / 2, 0, y * chunkSize - chunkSize / 2),
			new Vector3(x * chunkSize - chunkSize / 2, 0, y * chunkSize + chunkSize / 2)
		, Color.red, Mathf.Infinity);
	}
}
