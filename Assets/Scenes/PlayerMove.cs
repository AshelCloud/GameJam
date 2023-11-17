using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform tr;
    BoxCollider2D boxCollider;

    [SerializeField]
    float playerSpeed = 1;

    [SerializeField]
    float perceptionRange = 1f;

    private bool isJumping = false;
    private bool isClimbing = false;
    public bool canClimb = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        boxCollider= GetComponent<BoxCollider2D>();
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

        if (CheckHitWall(moveInput))
        {
            Debug.Log("�� ����");
            moveInput = Vector3.zero;
        }

        tr.Translate(moveInput * Time.deltaTime * playerSpeed);
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

    bool CheckHitWall(Vector3 movement)
    {
        // �����ӿ� ���� ���� ���͸� ���� ���ͷ� ��ȯ���ش�.
        movement = transform.TransformDirection(movement);
        // scope�� ray �浹�� Ȯ���� ������ ������ �� �ִ�.
        float scope = 1f;

        // �÷��̾��� �Ӹ�, ����, �� �� 3�������� ray�� ���.
        List<Vector3> rayPositions = new List<Vector3>();
        rayPositions.Add(transform.position + Vector3.up * 0.1f);
        rayPositions.Add(transform.position + Vector3.up * boxCollider.size.y * 0.5f);
        rayPositions.Add(transform.position + Vector3.up * boxCollider.size.y);

        // ������� ���� ray�� ȭ�鿡 �׸���.
        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        // ray�� ���� �浹�� Ȯ���Ѵ�.
        foreach (Vector3 pos in rayPositions)
        {
            if (Physics.Raycast(pos, movement, out RaycastHit hit, scope))
            {
                if (hit.collider.CompareTag("WALL"))
                    return true;
            }
        }
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
