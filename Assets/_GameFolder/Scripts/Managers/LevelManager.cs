using SplashyGame.Platforms;
using UnityEngine;
using DG.Tweening;

namespace SplashyGame.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject platformPrefab;
        
        public float zPos;
        public float xMin;
        public float xMax;
        public int count;
        
        private void Start()
        {
            SpawnPrefabs();
        }

        private void SpawnPrefabs()
        {
            for (int i = 0; i < count; i++)
            {
                float randomXPos = Random.Range(xMin, xMax);
                if (i == 0)
                {
                    randomXPos = 0;
                }
                
                Vector3 spawnPosition = new Vector3(randomXPos, 0.0f, i * zPos);
                GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity, transform);
                Platform platformScript = platform.GetComponent<Platform>();
                if (platformScript != null)
                {
                    bool isFirstObject = i == 0;

                    bool isColorObjectOpen;
                    if (i == 7)
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

                    platformScript.OnPlatformCreated(isFirstObject, isColorObjectOpen, isWhitePlateOpen);
                }
            }
        }
    }
}