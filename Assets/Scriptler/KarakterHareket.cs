using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour

{
    public Animator animator;
    public Transform cam;
    public Rigidbody rb;
    public float speed;
    float x, z;
    Vector3 move = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, cam.transform.rotation, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");
      
        float anim = new Vector2(yatay, dikey).magnitude;
        animator.SetFloat("speed", anim);
        
        Vector3 move = new Vector3(yatay, 0, dikey) * Time.deltaTime * speed;
        rb.MovePosition(transform.position + transform.TransformDirection(move));


    }
}
