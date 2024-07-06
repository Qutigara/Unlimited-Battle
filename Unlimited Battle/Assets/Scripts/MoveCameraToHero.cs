using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToHero : MonoBehaviour
{

    private Transform hero;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            moveCameraToHero();
        }
    }

    public void moveCameraToHero()
    {
        Vector3 temp = transform.position;
        temp.x = hero.position.x + 0.9f;
        temp.z = hero.position.z - 10.5f;

        transform.position = temp;
    }


}
