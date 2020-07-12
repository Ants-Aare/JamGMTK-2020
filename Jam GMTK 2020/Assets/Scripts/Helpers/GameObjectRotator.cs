using UnityEngine;
using System.Collections.Generic;

public class GameObjectRotator : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private Vector3 speed;
    [SerializeField]
    private float multiplier = 1f;

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime * multiplier);
    }
}