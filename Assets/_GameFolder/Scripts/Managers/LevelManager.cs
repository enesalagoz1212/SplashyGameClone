using System;
using System.Collections.Generic;
using System.Collections;
using SplashyGame.Controllers;
using SplashyGame.Platforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SplashyGame.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		public GameObject platformPrefab;

		public PlayerController playerController;


		public float zPos;
		public float xMin;
		public float xMax;
		public int count;

		private List<Platform> _createdPlatforms = new List<Platform>();

		public Color targetColor;
		
		private float _firstPlatformPositionZ;
		private float _lastPlatformPositionZ;

		private void Awake()
		{
			// If there is an instance, and it's not me, delete myself.
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		private void OnEnable()
		{
			GameManager.OnGameReset += OnGameReset;
		}

		private void OnDisable()
		{
			GameManager.OnGameReset -= OnGameReset;
		}

		private void Start()
		{
			SpawnPrefabs();
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

				Platform platformScript = platform.GetComponent<Platform>();

				_createdPlatforms.Add(platformScript);

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
					if (i == count)
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

			_firstPlatformPositionZ = _createdPlatforms[0].transform.position.z;
			_lastPlatformPositionZ = _createdPlatforms[_createdPlatforms.Count - 1].transform.position.z;
		}

		public void ScalePlatforms()
		{
			for (int i = 0; i < _createdPlatforms.Count; i++)
			{
				Platform platform = _createdPlatforms[i];

				if (platform.transform.position.z > playerController.childTransform.transform.position.z)
				{
					platform.PlatformScalingColoringAnimation(targetColor);
				}
			}
		}

		public float ReturnPlayerProgress()
		{
			var top = (playerController.childTransform.position.z - _firstPlatformPositionZ);
			var bottom = (_lastPlatformPositionZ - _firstPlatformPositionZ);
			return top / bottom;
		}
		
		private void OnGameReset()
		{
			for (var i = 0; i < _createdPlatforms.Count; i++)
			{
				Destroy(_createdPlatforms[i].gameObject);
			}
			_createdPlatforms.Clear();
			SpawnPrefabs();
		}
	}
}