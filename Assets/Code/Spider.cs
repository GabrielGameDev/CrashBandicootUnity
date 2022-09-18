using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float speed = 5;
    public LayerMask wallLayer;
    int direction = 1;
    Rigidbody rb;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.right * direction, 1, wallLayer))
            direction *= -1;

        rb.MovePosition(transform.position + transform.right * speed * direction * Time.deltaTime);
        
    }

    
}
