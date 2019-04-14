using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float crumble_time = 3;

    private float upper_bound = 20;
    private GameObject player;

    private bool started_crumb = false;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!started_crumb && collision.gameObject.tag.StartsWith("Player"))
        {
            StartCoroutine(Crumble());
        }
    }

    IEnumerator Crumble()
    {
        started_crumb = true;
        Debug.Log("Detected platform and " + player + " collision");

        yield return new WaitForSeconds(crumble_time);

        Destroy(gameObject);
    }
}
