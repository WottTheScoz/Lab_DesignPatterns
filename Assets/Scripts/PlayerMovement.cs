using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    float hInput;
    string hMoveButton = "Horizontal";

    // Update is called once per frame
    void Update()
    {
        // Gets the current horizontal axis (left/right)
        if(hInput != Input.GetAxisRaw(hMoveButton))
        {
            hInput = Input.GetAxisRaw(hMoveButton);
        }

        if(Input.GetButton(hMoveButton))
        {
            HorizontalMovement();
        }
    }

    void HorizontalMovement()
    {
        transform.position += Vector3.right * hInput * moveSpeed * Time.deltaTime;
    }
}
