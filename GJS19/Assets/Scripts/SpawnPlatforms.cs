using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public Transform platform_prefab;
    public GameObject left_collider;
    public GameObject right_collider;
    public float lower_bound = 10;
    public float upper_bound = 20;

    public GameObject player;

    private float probability = 0.8f; // initially higher chance to spawn platform
    private float left_bound;
    private float right_bound;

    private float scale;
    private float player_speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        float width = left_collider.GetComponent<Renderer>().bounds.size.x;
        left_bound = left_collider.transform.position.x + (width / 2);
        right_bound = right_collider.transform.position.x - (width / 2);

        scale = 1 / upper_bound; // probability increases by (1/gap) every unit passed
    }

    // Update is called once per frame
    void Update()
    {
        player_speed = player.GetComponent<Rigidbody2D>().velocity.y;
        if (player_speed == 0.0f)
        {
            return;
        }

        float check_probability = Random.Range(0f, 1f);
        // Debug.Log("Generated Probability = " + check_probability + "; Threshold = " + probability);
        
        if (check_probability <= probability)
        {
            InstantiatePlatform();
            probability = -scale * lower_bound;
        }
        else
        {
            probability += scale * (Mathf.Abs(player_speed) * Time.deltaTime);
        }
    }

    private void InstantiatePlatform()
    {
        float x = Random.Range(left_bound, right_bound);
        float y = transform.position.y;

        Instantiate(platform_prefab, new Vector2(x, y), Quaternion.identity);
    }
}
