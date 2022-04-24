using UnityEngine;

[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Biome Preset")]
public class BiomePreset : ScriptableObject
{
	public Sprite[] tiles;
	[Range(0, 1f)] public float minHeight;
	[Range(0, 1f)] public float minMoisture;
	[Range(0, 1f)] public float minHeat;

	public Sprite GetTileSprite()
	{
		return tiles[Random.Range(0, tiles.Length)];
	}

	public bool MatchCondition(float height, float moisture, float heat)
	{
		return height >= minHeight && moisture >= minMoisture && heat >= minHeat;
	}
}
