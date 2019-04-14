using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private GameObject player;
    private float upper_bound = 15;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        height = Mathf.Floor(gameObject.GetComponent<Renderer>().bounds.size.y);

        if (gameObject.transform.root.name == "Left Screen")
        {
            player = GameObject.FindGameObjectWithTag("Player1");
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.transform.position.y - player.transform.position.y) >= upper_bound)
        {
            float new_y = gameObject.transform.position.y - height * 3;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, new_y);
        }
    }
}
