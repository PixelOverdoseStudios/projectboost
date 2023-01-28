using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    private BoxCollider hitBox;

    void Start()
    {
        hitBox = GetComponent<BoxCollider>();
    }

    void Update()
    {
        TurnOffCollision();
    }

    void TurnOffCollision()
    {
        if(Input.GetKeyDown(KeyCode.C) && hitBox == true)
        {
            hitBox.enabled = false;
        }
        if(Input.GetKeyDown (KeyCode.C) && hitBox != true)
        {
            hitBox.enabled = true;
        }
    }



}
