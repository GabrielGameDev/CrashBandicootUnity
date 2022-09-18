using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5;
    public float turnSpeed = 30;
    public float jumpForce = 12;
    public float groundCheckRadius = 1;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public GameObject jumpAttack, headAttack;

    public float attackRotateSpeed = 100;
    public float attackTime = 0.5f;
    public float attackInterval = 0.75f;
    public GameObject attackCollider;
    float attackTimer;
    bool isAttacking;
    bool canAttack = true;

    public AudioClip[] stepSounds;

    Animator animator;
    Rigidbody rb;
    AudioSource audioSource;
    Vector3 direction;

    Transform mainCam;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        bool onGround = false;
        Ray ray = new Ray(groundCheck.position, Vector3.down);
        if(Physics.SphereCast(ray, groundCheckRadius, groundCheckRadius, groundLayer))
		{
            
            onGround = true;
		}

		if (Input.GetButtonDown("Jump") && onGround && !isAttacking)
		{
            Jump();            
		}

        animator.SetBool("grounded", onGround);

        if(!onGround && rb.velocity.y < 0)
		{
            jumpAttack.SetActive(true);
		}
		else
		{
            jumpAttack.SetActive(false);
		}

        if (!onGround && rb.velocity.y > 0)
        {
            headAttack.SetActive(true);
        }
        else
        {
            headAttack.SetActive(false);
        }


        if (Input.GetButtonDown("Fire1") && onGround && canAttack)
		{
            Invoke("FinishAttack", attackInterval);
            canAttack = false;
            isAttacking = true;
		}
		if (isAttacking)
		{
            Attack();
		}
    }

	void FinishAttack()
	{
        canAttack = true;
	}

	private void FixedUpdate()
	{
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction.Set(horizontal, 0, vertical);

        direction = Quaternion.Euler(0, mainCam.eulerAngles.y, 0) * direction;
        
        direction.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, direction, turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(Quaternion.LookRotation(desiredForward));

        bool running = false;
        if(horizontal != 0 || vertical != 0)
		{
            running = true;
		}

        animator.SetBool("run", running);

        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

    }


    void Attack()
	{
        attackCollider.SetActive(true);
        transform.Rotate(Vector3.up, attackRotateSpeed * Time.deltaTime);
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackTime)
		{
            isAttacking = false;
            attackTimer = 0;
            attackCollider.SetActive(false);
		}
	}

    public void Jump()
	{
        animator.SetTrigger("jump");
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Step()
	{
        int random = Random.Range(0, stepSounds.Length);
        audioSource.PlayOneShot(stepSounds[random]);
	}
}
