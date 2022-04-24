using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	public BiomePreset[] biomes;
	public GameObject tilePrefab;
	public GameObject container;

	[Header("Dimensions")]
	public int width = 50;
	public int height = 50;
	public float scale = 1.0f;
	public Vector2 offset;

	[Header("Height Map")]
	public Wave[] heightWaves;
	public float[,] heightMap;
	[Header("Moisture Map")]
	public Wave[] moistureWaves;
	private float[,] moistureMap;
	[Header("Heat Map")]
	public Wave[] heatWaves;
	private float[,] heatMap;

	private void Start()
	{
		// GenerateMap();
	}

	private void Update()
	{
		GenerateMap();
	}

	private void GenerateMap()
	{
		foreach (Transform child in container.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		heightMap = NoiseGenerator.Generate(width, height, scale, heightWaves, offset);
		moistureMap = NoiseGenerator.Generate(width, height, scale, moistureWaves, offset);
		heatMap = NoiseGenerator.Generate(width, height, scale, heatWaves, offset);

		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
				tile.GetComponent<SpriteRenderer>().sprite = GetBiome(heightMap[x, y], moistureMap[x, y], heatMap[x, y]).GetTileSprite();
				tile.transform.parent = container.transform;
			}
		}
	}

	private BiomePreset GetBiome(float height, float moisture, float heat)
	{
		List<BiomeTempData> biomeTemp = new List<BiomeTempData>();
		foreach (BiomePreset biome in biomes)
		{
			if (biome.MatchCondition(height, moisture, heat))
			{
				biomeTemp.Add(new BiomeTempData(biome));
			}
		}

		float curVal = 0.0f;
		BiomePreset biomeToReturn = null;
		foreach (BiomeTempData biome in biomeTemp)
		{
			if (biomeToReturn == null)
			{
				biomeToReturn = biome.biome;
				curVal = biome.GetDiffValue(height, moisture, heat);
			}
			else
			{
				if (biome.GetDiffValue(height, moisture, heat) < curVal)
				{
					biomeToReturn = biome.biome;
					curVal = biome.GetDiffValue(height, moisture, heat);
				}
			}
		}
		if (biomeToReturn == null)
			biomeToReturn = biomes[0];

		return biomeToReturn;
	}
}
