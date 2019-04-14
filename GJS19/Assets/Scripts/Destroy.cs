using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public AnimationClip clip;

    private float upper_bound = 20;
    private GameObject player;

    private Animator animator;
    private bool started_crumb = false;
    private float clip_length;

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

        animator = GetComponent<Animator>();
        clip_length = clip.length;
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
        animator.SetTrigger("TriggerCrumblePlat");

        yield return new WaitForSeconds(clip_length);

        Destroy(gameObject);
    }
}
