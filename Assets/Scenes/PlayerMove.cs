using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform tr;

    [SerializeField]
    float playerSpeed = 1;

    private bool isJumping = false;
    private bool isClimbing = false;
    public bool canClimb = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveInput = Vector3.zero;

        float x = 0;

        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            x = Input.GetAxis("Horizontal");
        }

        moveInput.x = x;

        Vector3 tempTr = tr.position + moveInput;

        if (DetectWall(tempTr))
        {
            tr.Translate(moveInput * Time.deltaTime * playerSpeed);
        }
    }

    void Jump()
    {
        if (isJumping)
            return;

        if(isClimbing) 
        { 
            
        }
        else
        {

        }
    }

    void Climb()
    {

    }

    bool DetectWall(Vector3 tr)
    {
        

        return false;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "CLIMBWALL")
        {
            canClimb = true;
        }
    }
}
