using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour {
    public GameObject cloudPrefab;
    public static CloudManager instance;
    Transform lastSpawnedCloud;
    public Vector2 randomXOffset = new Vector2(+0.25f, +3f);
    public float cloudSpawnPositionY = 4;
    public List<Transform> cloudList = new List<Transform>();
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        SpawnCloud(10);
	}

    public void RemoveCloud(Transform cloudToRemove)
    {
        Transform tempCloud = cloudToRemove;
        cloudList.Remove(cloudToRemove);

        Vector3 pos = lastSpawnedCloud.position;
        pos.x += Random.Range(randomXOffset.x, randomXOffset.y);
        tempCloud.transform.position = pos;

    
    lastSpawnedCloud = tempCloud.transform;
        cloudList.Add(tempCloud);
    }
    Transform getTheLastCloud()
    {
        Transform lastCloud = lastSpawnedCloud;
        foreach(Transform cloud in cloudList)
        {
            if (cloud.position.x > lastCloud.position.x)
            {
                lastCloud = cloud;
            }

        }
        return lastCloud;
    }
	
	public void SpawnCloud(int val = 1)
    {

        if (lastSpawnedCloud != null)
        {
            lastSpawnedCloud = getTheLastCloud();
        }
        for (int i = 0; i < val; i++)
        {
            GameObject newCloud = (GameObject)Instantiate(cloudPrefab);
            newCloud.transform.SetParent(this.transform);

            if (lastSpawnedCloud == null)
            {
                Vector3 pos = Vector3.zero;
                pos.y += cloudSpawnPositionY;
                newCloud.transform.localPosition = pos;
            }
            else
            {
                Vector3 pos = lastSpawnedCloud.position;
                pos.x += Random.Range(randomXOffset.x, randomXOffset.y);
                newCloud.transform.position = pos;

            }
            lastSpawnedCloud = newCloud.transform;

            cloudList.Add(newCloud.transform);

        }

    }
}
