using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public Image player1_chain;
    public Image player2_chain;

    public Image player1_icon;
    public Image player2_icon;

    private RectTransform player1_icon_rt;
    private RectTransform player2_icon_rt;
    private float player1_icon_start;
    private float player2_icon_start;
    private float screen_height;
    private float total_distance;

    // Start is called before the first frame update
    void Start()
    {
        player1_icon_rt = player1_icon.GetComponent<RectTransform>();
        player2_icon_rt = player2_icon.GetComponent<RectTransform>();

        player1_icon_start = player1_icon_rt.anchoredPosition.y;
        player2_icon_start = player2_icon_rt.anchoredPosition.y;

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
        Debug.Log("Distance = " + player1_distance / total_distance + "; New y position = " + player1_icon_y);
        player1_icon_rt.anchoredPosition = new Vector2(player1_icon_x, player1_icon_y);

        /* Change player 2 icon position */
        float player2_icon_x = player2_icon_rt.anchoredPosition.x;
        float player2_icon_y = player2_icon_start - ((total_distance - player2_distance) / total_distance) * screen_height;
        Debug.Log("Distance = " + player2_distance / total_distance + "; New y position = " + player2_icon_y);
        player2_icon_rt.anchoredPosition = new Vector2(player2_icon_x, player2_icon_y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " is the ultimate loser haha they suck");
    }
}
