using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBordInputHandle : MonoBehaviour, IinputHandle
{
    public Vector2 Getinput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    
}
