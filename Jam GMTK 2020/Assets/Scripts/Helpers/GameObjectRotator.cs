using UnityEngine;
using System.Collections.Generic;

public class GameObjectRotator : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private Vector3 speed;

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}