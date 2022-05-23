using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///This is a simple script made to keep the camera tracking the player's position, without making it a child of the player
///This will prevent issues with deleting or changing player objects as part of animations or whatever.

public class SimpleCameraBind : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ///Since the camera transform is at the center of the camera, doing this keeps it centered
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        
    }
}
