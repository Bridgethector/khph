using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    SpriteRenderer spriteRenderer;
   public Vector2 randomMovementSpeed = new Vector2(-1,-10);
    public Vector2 randomScale = new Vector2(0.5f, 1.5f);
    public Vector2 randomAlpha = new Vector2(0.2f, 0.65f);
    public float ySpawnVariation = 0.5f;

    float movementSpeed;
	// Use this for initialization
	void Start () {

        spriteRenderer = GetComponent<SpriteRenderer>();
        RandomizeCloud();
	}

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.x += movementSpeed*Time.deltaTime *BallManager.instance.rigid.velocity.x/10;
        transform.position = pos;
        if (transform.localPosition.x < -10)
        {
            CloudManager.instance.RemoveCloud(this.transform);
           
        }
    }



    // Update is called once per frame
    void RandomizeCloud()
    {
        //randomizeColor
        Color currentColor = spriteRenderer.color;
        currentColor.a = Random.Range(randomAlpha.x, randomAlpha.y);
        spriteRenderer.color = currentColor;

        //randomizeScale
        Vector3 scale = transform.localScale;
        scale = scale * Random.Range(randomScale.x, randomScale.y);
        transform.localScale = scale;

        //randomize speed
        movementSpeed = Random.Range(randomMovementSpeed.x, randomMovementSpeed.y);

        //randomize Y pos
        Vector3 position = transform.position;
        position.y += Random.Range(-ySpawnVariation, ySpawnVariation);
        transform.position = position;


        // randomize Layer Order
        spriteRenderer.sortingOrder = Random.Range(-10, -7);
    }
}
