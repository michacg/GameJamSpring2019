using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;

	private float offsetY;

    void Start()
    {
        offsetY = transform.position.y - player.transform.position.y;
    }


    void Update()
    {
    	Vector2 newPosition = transform.position;
        newPosition.y       = player.transform.position.y + offsetY;
        transform.position  = newPosition;
    }
}
