using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SplashyGame.Platforms
{
	public class Platform : MonoBehaviour
	{
		
		private Renderer platformRenderer;

		public GameObject Prefab;
		

		public float zPos = 5.0f;
		public float xMin = -4f;
		public float xMax = 4f;
		public int count = 30;

		void Start()
		{
			SpawnPrefabs();
			platformRenderer = Prefab.GetComponent<Renderer>();
			
		}

		public void SpawnPrefabs()
		{
			for (int i = 1; i < count; i++)
			{
				float randomXPos = Random.Range(xMin, xMax);
				Vector3 spawnPosition = new Vector3(randomXPos, 0.0f, i * zPos);
				Instantiate(Prefab, spawnPosition, Quaternion.identity);
				//if (i==14)
				//{
				//	GameObject cube = Prefab.transform.Find("Cube").gameObject;
				//	cube.SetActive(true);

				//}
				//else
				//{
				//	GameObject cube =Prefab.transform.Find("Cube").gameObject;
				//    cube.SetActive(false);
				//}
				if (i == 7)
				{
					GameObject _colour = Prefab.transform.Find("color").gameObject;
				
					_colour.SetActive(true);
				}
				else
				{
					GameObject _colour = Prefab.transform.Find("color").gameObject;
					_colour.SetActive(false);
				}
				if (i == 15 ||  i==16 || i==17 || i==18 || i==19 )
				{
					GameObject whitePlate = Prefab.transform.Find("WhitePlate").gameObject;
					whitePlate.SetActive(true);

				}
				else
				{
					GameObject whitePlate = Prefab.transform.Find("WhitePlate").gameObject;
					whitePlate.SetActive(false);
				}
				
			}
		}
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Ball"))
			{
				platformRenderer.material.DOColor(Color.red, 0.2f);
				Debug.Log("Red");
			}
		}

		

	}

}
