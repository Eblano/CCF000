using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnObjectsRandom : MonoBehaviour
{
    //[HideInInspector]
    public List<Transform> SpawnPoints = new List<Transform>();
    public List<Transform> PrefabsObjects = new List<Transform>();

    public int CountObjInScene = 0;

	void Start ()
    {
	    for(int i=0; i<transform.childCount; i++)
        {
            SpawnPoints.Add(transform.GetChild(i));
        }

        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        if (CountObjInScene < 4)
        {
            if (SpawnPoints.Count > 0)
            {
                int rand = Random.Range(0, SpawnPoints.Count);
                int randObj = Random.Range(0, PrefabsObjects.Count);

                Transform object1 = Instantiate(PrefabsObjects[randObj], SpawnPoints[rand].position + new Vector3(0, 0.5f, 0), Quaternion.identity) as Transform;
                object1.GetComponent<Object>().PointSpawn = SpawnPoints[rand];
                SpawnPoints.Remove(SpawnPoints[rand]);
                CountObjInScene += 1;
            }
        }
        yield return new WaitForSeconds(10);
        StartCoroutine(SpawnObject());
    }
}
