using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    public float delay;
    void Start()
    {

    }

    void Update()
    {
        delay += Time.deltaTime;
        if(delay >= 1.5f)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject, 4f);
            delay = 0;
        }
    }
}
