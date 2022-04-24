using UnityEngine;

public class NoiseGenerator
{
	public static float[,] Generate(int width, int height, float scale, Wave[] waves, Vector2 offset)
	{
		float[,] noiseMap = new float[width, height];
		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				float samplePosX = x * scale + offset.x;
				float samplePosY = y * scale + offset.y;
				float normalization = 0.0f;
				foreach (Wave wave in waves)
				{
					noiseMap[x, y] += wave.amplitude * Mathf.PerlinNoise(samplePosX * wave.frequency + wave.seed, samplePosY * wave.frequency + wave.seed);
					normalization += wave.amplitude;
				}
				noiseMap[x, y] /= normalization;
			}
		}
		return noiseMap;
	}
}
