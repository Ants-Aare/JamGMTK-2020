
using UnityEngine;
using System;
using System.Collections;
using GMTKJAM.Items;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public InventoryController inventoryController;
    public InteractionController interactionController;
    public Animator animator;
    public Camera camera;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}