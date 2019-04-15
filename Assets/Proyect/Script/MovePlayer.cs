using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public int speed;
    // Update is called once per frame
    void Update()
    {
        Vector3 direcction = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        transform.GetComponent<Rigidbody>().velocity += direcction;
        if(direcction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direcction);
    }
}
