using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Monster monster;

    [SerializeField]
    public int[] percent;       //확률
    public GameObject[] Item;       //아이템 종류
    void Start()
    {
        monster = GetComponent<Monster>();
    }

    void Update()
    {

    }

    public void Drop()
    {
        if (monster.isDie == true)
        {
            for(int i = 0;i < Item.Length; i++)
            {
                if (Random.Range(1,100) <= percent[i])
                {
                    Instantiate(Item[i], monster.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
