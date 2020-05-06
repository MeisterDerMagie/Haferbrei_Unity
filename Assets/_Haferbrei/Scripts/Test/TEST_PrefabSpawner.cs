using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TEST_PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int amount;
    public Vector2 area;
    
    
    [Button]
    public void Spawn()
    {
        for (int i = 0; i < amount; i++)
        {
            var xPos = Random.Range(-area.x, area.x);
            var yPos = Random.Range(-area.y, area.y);

            Instantiate(prefab, new Vector3(xPos, yPos), Quaternion.identity);
        }
    }
}
