
using UnityEngine;
using System;
using System.Collections;
using GMTKJAM.Items;

public class WhaleManager : MonoBehaviour
{
    public static WhaleManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}