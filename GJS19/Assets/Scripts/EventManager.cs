﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public delegate void NearPlatform();
	public static event NearPlatform IsNearPlatform;
    
    void WhenNearPlatform()
    {
    	if (IsNearPlatform != null)
    		IsNearPlatform();
    }
}
