using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dune : MonoBehaviour {

    public bool isFirst;
    SpriteRenderer spriteRenderer;
    
    public void Generate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 pos =
         SpriteScrollUtility.FindNextElementPosition(DunesManager.instance.dunes);
        pos.y = transform.position.y;
        transform.position = pos;
        DunesManager.instance.dunes.Add(transform);
        transform.SetParent(DunesManager.instance.duneCollector);
        name = "Dune";

    }

    private void Update()
    {
        if (isFirst) {
            spriteRenderer.sprite = WorldGraphicsManager.instance.currentGround.StartDune;
        } else
        {
            spriteRenderer.sprite = WorldGraphicsManager.instance.currentGround.Dunes;

        }
    }
}
