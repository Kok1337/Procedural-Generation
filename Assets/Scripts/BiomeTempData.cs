public class BiomeTempData
{
	public BiomePreset biome;

	public BiomeTempData(BiomePreset preset)
	{
		biome = preset;
	}

	public float GetDiffValue(float height, float moisture, float heat)
	{
		return (height - biome.minHeight) + (moisture - biome.minMoisture) + (heat - biome.minHeat);
	}
}