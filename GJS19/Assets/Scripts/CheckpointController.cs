﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject monster;

    public Image player1_chain;
    public Image player2_chain;

    public Image player1_icon;
    public Image player2_icon;

    public Image monster_icon;

    private RectTransform player1_icon_rt;
    private RectTransform player2_icon_rt;
    private RectTransform monster_icon_rt;
    private float player1_icon_start;
    private float player2_icon_start;
    private float monster_icon_start;
    private float screen_height;
    private float total_distance;

    // Start is called before the first frame update
    void Start()
    {
        player1_icon_rt = player1_icon.GetComponent<RectTransform>();
        player2_icon_rt = player2_icon.GetComponent<RectTransform>();
        monster_icon_rt = monster_icon.GetComponent<RectTransform>();

        player1_icon_start = player1_icon_rt.anchoredPosition.y;
        player2_icon_start = player2_icon_rt.anchoredPosition.y;
        monster_icon_start = monster_icon_rt.anchoredPosition.y;

        screen_height = player1_chain.GetComponent<RectTransform>().sizeDelta.y;

        total_distance = Mathf.Abs(player1.transform.position.y - gameObject.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        /* Change chain fillAmount */
        float player1_distance = Mathf.Abs(player1.transform.position.y - gameObject.transform.position.y);
        float player2_distance = Mathf.Abs(player2.transform.position.y - gameObject.transform.position.y);

        player1_chain.fillAmount = (total_distance - player1_distance) / total_distance;
        player2_chain.fillAmount = (total_distance - player2_distance) / total_distance;

        /* Change player 1 icon position */
        float player1_icon_x = player1_icon_rt.anchoredPosition.x;
        float player1_icon_y = player1_icon_start - ((total_distance - player1_distance) / total_distance) * screen_height;
        // Debug.Log("Distance = " + player1_distance / total_distance + "; New y position = " + player1_icon_y);
        player1_icon_rt.anchoredPosition = new Vector2(player1_icon_x, player1_icon_y);

        /* Change player 2 icon position */
        float player2_icon_x = player2_icon_rt.anchoredPosition.x;
        float player2_icon_y = player2_icon_start - ((total_distance - player2_distance) / total_distance) * screen_height;
        // Debug.Log("Distance = " + player2_distance / total_distance + "; New y position = " + player2_icon_y);
        player2_icon_rt.anchoredPosition = new Vector2(player2_icon_x, player2_icon_y);

        /* Change monster icon position */
        float monster_distance = Mathf.Abs(monster.transform.position.y - gameObject.transform.position.y);
        float monster_icon_x = monster_icon_rt.anchoredPosition.x;
        float monster_icon_y = monster_icon_start - ((total_distance - monster_distance) / total_distance) * screen_height;
        // Debug.Log("Distance = " + player2_distance / total_distance + "; New y position = " + player2_icon_y);
        monster_icon_rt.anchoredPosition = new Vector2(monster_icon_x, monster_icon_y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player 1"))
        {
            SceneManager.LoadScene("Player2Win", LoadSceneMode.Single);
        }
        else if (collision.gameObject.name.Equals("Player 2"))
        {
            SceneManager.LoadScene("Player1Win", LoadSceneMode.Single);
        }
    }
}
