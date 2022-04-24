using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PG : MonoBehaviour
{
	public Vector2Int size = new Vector2Int(16, 16);
	public float scale;
	public Vector2 offset;
	public Wave[] waves;

	private void Start()
	{
		string sdf = "45613";
		Debug.Log(sdf.GetHashCode());
	}

	void Update()
    {
		var texture = new Texture2D(size.x, size.y);
		float[,] hMap = NoiseGenerator.Generate(size.x, size.y, scale, waves, offset);
		for (int x = 0; x < size.x; x++)
			for (int y = 0; y < size.y; y++)
			{
				float h = hMap[x, y];
				texture.SetPixel(x, y, new Color(h, h, h, 1));
			}
		texture.Apply();
		var sprite = Sprite.Create(texture, new Rect(0, 0, size.x, size.y), new Vector2(0, 0));
		GetComponent<SpriteRenderer>().sprite = sprite;
	}
}
