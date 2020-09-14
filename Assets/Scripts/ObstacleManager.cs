using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
	[HideInInspector] public static List<Obstacle> OBSTACLES = new List<Obstacle>();

	public GameObject obstaclePrefab;
	public GameObject chunkPrefab;
	public float chunkSize = 10;
	public float chunkFillSize = 10;

	private Transform layer;
	private List<GameObject> chunks = new List<GameObject>();

	private void Start()
	{
		layer = transform;
		GenerateChunk(1, 0);
	}

	private void GenerateChunk(int x, int y)
	{
		GameObject chunk = Instantiate<GameObject>(chunkPrefab, layer);
		chunks.Add(chunk);

		chunk.transform.position = new Vector3(x * chunkSize, y * chunkSize);
		chunk.GetComponent<Chunk>().DebugBounds(chunkSize);

		FillChunk(chunks[0], 5);
	}

	private void FillChunk(GameObject chunk, float definition)
	{
		float caseSize = chunkSize / definition;

		for (int x = 0; x < definition; x++)
		{
			for (int y = 0; y < definition; y++)
			{
				if (Random.value < 0.5f)
				{
					float posX = -chunkSize / 2 + x * caseSize + caseSize / 2;
					float posY = -chunkSize / 2 + y * caseSize + caseSize / 2;
					GameObject ob = GenerateObstacle(chunk);
					ob.transform.position = new Vector3(chunk.transform.position.x + posX, 0, chunk.transform.position.y + posY);
				}
			}
		}
	}

	private GameObject GenerateObstacle(GameObject chunk)
	{
		return Instantiate(obstaclePrefab, chunk.transform);
	}
}
