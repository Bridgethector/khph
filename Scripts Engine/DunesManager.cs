using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunesManager : MonoBehaviour {
    public GameObject[] dunesResources;

    public GameObject FirstDune;
    public List<Transform> dunes = new List<Transform>();

    public static DunesManager instance;
    public Transform duneCollector;
    public float yVariation;
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        SpawnDune(8);
       // SpawnGroundPattern(10);
	}
    bool firstDunePool = false;

    void SpawnDune(int number=1) {
        for (int i = 0; i < number; i++) {
            GameObject randomDune = dunesResources[Random.Range(0, dunesResources.Length)];
            if(dunes.Count == 0)
            {
                randomDune = FirstDune;
            }
            GameObject newDune =(GameObject) Instantiate(randomDune);
            Vector3 pos = newDune.transform.position;
            pos.y = yVariation;
            newDune.transform.position = pos;
            newDune.GetComponent<Dune>().Generate();
         
        }

    }
   

    void PoolDune()
    {
        if (!firstDunePool)
        {
            dunes.RemoveAt(0);
            firstDunePool = true;
        }

        Transform tempDune = dunes[0];

        dunes.RemoveAt(0);
        tempDune.position = SpriteScrollUtility.FindNextElementPosition(DunesManager.instance.dunes);
        dunes.Add(tempDune);

    }



    private void Update()
    {
        DunePooling();
    }

   
    void DunePooling() {
        if (dunes.Count > 0)
        {
            Transform firstDune = dunes[0];
            if (firstDune.transform.position.x - Camera.main.transform.position.x < -50)
            {
                PoolDune();

            }

        }
    }




}
