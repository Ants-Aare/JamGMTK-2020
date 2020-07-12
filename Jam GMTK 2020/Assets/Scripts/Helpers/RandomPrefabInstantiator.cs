using UnityEngine;

public class RandomPrefabInstantiator : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private bool instantiateOnStart = true;

    [Header("References")]
    [SerializeField]
    private GameObject[] prefabs = null;

    void Start()
    {
        if(instantiateOnStart)
            InstantiateRandom();
    }

    public void InstantiateRandom()
    {
        if(prefabs.Length > 0)
            Instantiate(prefabs[Random.Range(0,prefabs.Length)], this.transform).transform.localPosition = Vector3.zero;
    }    
}