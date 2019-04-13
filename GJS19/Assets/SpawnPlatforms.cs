using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public Transform platform_prefab;
    public GameObject left_collider;
    public GameObject right_collider;
    public float scale;

    private float probability = 80.0f; // initially higher chance to spawn platform
    private float left_bound;
    private float right_bound;

    // Start is called before the first frame update
    void Start()
    {
        float width = left_collider.GetComponent<Renderer>().bounds.size.x;
        left_bound = left_collider.transform.position.x + (width / 2);
        right_bound = right_collider.transform.position.x - (width / 2);
    }

    // Update is called once per frame
    void Update()
    {
        float check_probability = Random.Range(0, 1);
        
        if (check_probability >= probability)
        {
            InstantiatePlatform();
            probability = 0.0f;
        }
        else
        {
            probability += scale * Time.deltaTime;
        }
    }

    private void InstantiatePlatform()
    {
        float x = Random.Range(left_bound, right_bound);
        float y = transform.position.y;

        Instantiate(platform_prefab, new Vector2(x, y), Quaternion.identity);
    }
}
