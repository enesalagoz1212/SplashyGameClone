using SplashyGame.Platforms;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

namespace SplashyGame.Managers
{
	public class LevelManager : MonoBehaviour
	{

		public GameObject platformPrefab;
		public GameObject ballObject;

		public float zPos;
		public float xMin;
		public float xMax;
		public int count;

		List<GameObject> spawnPlatform;


		public Color targetColor;
		public float colorDuration = 1f;

		Renderer _platform;

		private void Start()
		{
			spawnPlatform = new List<GameObject>();
			SpawnPrefabs();

			_platform = GetComponent<Renderer>();
			
		}

		private void SpawnPrefabs()
		{
			for (int i = 0; i <= count; i++)
			{
				float randomXPos = Random.Range(xMin, xMax);
				if (i == 0)
				{
					randomXPos = 0;
				}

				Vector3 spawnPosition = new Vector3(randomXPos, 0.0f, i * zPos);
				GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity, transform);

				spawnPlatform.Add(platform);

				Platform platformScript = platform.GetComponent<Platform>();

				if (platformScript != null)
				{
					bool isFirstObject = i == 0;

					bool isColorObjectOpen;
					if (i == 20)
					{
						isColorObjectOpen = true;
					}
					else
					{
						isColorObjectOpen = false;
					}
					// isColorObjectOpen = i == 7; // // // // => Correct way to write

					bool isWhitePlateOpen;
					if (i >= 15 && i <= 19)
					{
						isWhitePlateOpen = true;
					}
					else
					{
						isWhitePlateOpen = false;
					}
					bool isDiamondo;
					if (i == 7 || i == 9 || i == 10 || i == 25 || i == 26)
					{
						isDiamondo = true;
					}
					else
					{
						isDiamondo = false;
					}
					bool isFlag;
					if (i==count)
					{
						isFlag = true;
					}
					else
					{
						isFlag = false;
					}

					platformScript.OnPlatformCreated(isFirstObject, isColorObjectOpen, isWhitePlateOpen, isDiamondo, isFlag);
				}


			}
		}

		public void ScalePlatforms()
		{
			
			GameObject firstPlatform = spawnPlatform[0];

			for (int i = 0; i < spawnPlatform.Count; i++)
			{
				GameObject platform = spawnPlatform[i];

				if (platform.transform.position.z > ballObject.transform.position.z)
				{

					platform.transform.DOScale(new Vector3(1.3f, platform.transform.localScale.y, 1.2f), 0.2f).OnComplete(() =>
					{
						ColorPlatform();
					});
				
				}
			}

		}
		public void ColorPlatform()
		{
			
			GameObject lastPlatform = spawnPlatform[0];
			for (int i = 0; i < spawnPlatform.Count; i++)
			{
				GameObject platform = spawnPlatform[i];
				if (platform.transform.position.z>ballObject.transform.position.z)
				{
					_platform.material.DOColor(targetColor, colorDuration);
				}
			}
		}
		

	}
}