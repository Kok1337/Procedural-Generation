using UnityEngine;

[System.Serializable]
public class Wave
{
	public float seed;
	[Range(0f, 0.1f)] public float frequency;
	[Range(-1f, 1f)] public float amplitude;
}