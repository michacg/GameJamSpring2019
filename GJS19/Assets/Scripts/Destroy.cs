using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private float upper_bound = 20;
    private GameObject player;

    private void Start()
    {
        if (gameObject.name.EndsWith("Player1(Clone)"))
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
        if ((gameObject.transform.position.y -  player.transform.position.y) >= upper_bound)
        {
            Destroy(gameObject);
        }
    }
}
