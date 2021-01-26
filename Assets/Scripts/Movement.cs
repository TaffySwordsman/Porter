using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

//Wall climb puts player up and over
//Preserve momentum for a bit after momentum stops
	//Lerp between 

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;

    [Space]
    [Header("Stats")]
	[Space]
	public float speed = 10;
	[Range(0, .3f)]
	public float moveSmooth = 0.1f;
	private Vector2 moveVel;

	[Space]
	public float jumpForce = 50;
	[SerializeField] private float fallMultiplier = 2.5f;
	[SerializeField] private float lowJumpMultiplier = 2f;
	[SerializeField] private float crouchFallMultiplier = 2f;

	[Space]
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallJumped;
    public bool wallSlide;
	public bool crouching;
    private bool groundTouch;
    public int side = 1;

	[Space]
	[Header("Polish")]
	public ParticleSystem jumpParticle;
	public ParticleSystem wallJumpParticle;
	public ParticleSystem slideParticle;

	// Start is called before the first frame update
	void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        //anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            //if(side != coll.wallSide)
            //    anim.Flip(side*-1);
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallSlide = false;
        }

        if (coll.onGround)
        {
            wallJumped = false;
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

		if (Input.GetButtonDown("Jump"))
		{
			//anim.SetTrigger("jump");

			if (coll.onGround)
				Jump(Vector2.up, false);
			if (coll.onWall && !coll.onGround)
				WallJump();
		}

		if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallSlide || !canMove)
            return;

        if(x > 0)
        {
            side = 1;
            //anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
			//anim.Flip(side);
		}


    }

	private void FixedUpdate()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		float xRaw = Input.GetAxisRaw("Horizontal");
		float yRaw = Input.GetAxisRaw("Vertical");
		Vector2 dir = new Vector2(x, y);

		Move(dir);

		//Jump gravity alteration
		if (rb.velocity.y < 0)
		{
			rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
		}
		else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
		{
			rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
		}

		//Wall sliding
		if (coll.onWall)
		{
			if (x != 0)
			{
				wallSlide = true;
				WallSlide();
			}
		}

		//Crouch check
		crouching = Input.GetButton("Crouch");

		//Crouch fall modifier
		if (crouching)
		{
			rb.velocity += Vector2.up * Physics.gravity.y * (crouchFallMultiplier - 1) * Time.fixedDeltaTime;
		}
	}

	public void Kill()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Respawn>().RespawnPlayer();
	}

	void GroundTouch()
    {
		//hasDashed = false;
		//side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        //if (coll.onGround)
            //hasDashed = false;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            //anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

		//Extend walljump depending on if the player is holding a direction
		if (Input.GetAxisRaw("Horizontal") != 0)
			Jump((Vector2.up / 1.2f + wallDir / 1.45f), true);
		else
			Jump((Vector2.up / 1.2f + wallDir / 1.5f), true);

		wallJumped = true;
    }

	private void WallSlide()
    {
        //if(coll.wallSide != side)
        // anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((Input.GetAxisRaw("Horizontal") == 1 && coll.onRightWall) || (Input.GetAxisRaw("Horizontal") == -1 && coll.onLeftWall))
        {
            pushingWall = true;
        }

        float push = pushingWall ? 0 : rb.velocity.x;

		if (rb.velocity.y < 1 && pushingWall)
			rb.velocity = new Vector2(push, -slideSpeed);

		if (rb.velocity.y > 1 && pushingWall)
			rb.velocity += new Vector2(push, -.25f);

	}

	int ParticleSide()
	{
		int particleSide = coll.onRightWall ? 1 : -1;
		return particleSide;
	}

	void WallParticle(float vertical)
	{
		var main = slideParticle.main;

		if (wallSlide && rb.velocity.y < 0)
		{
			slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
			slideParticle.Play();
			//main.startColor = new Color(152, 152, 152);

		}
		else
		{
			slideParticle.Stop();
			//main.startColor = Color.clear;
		}
	}

	private void Move(Vector2 dir)
    {
        if (!canMove)
            return;

        if (!wallJumped)
        {
			rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(dir.x * speed, rb.velocity.y), ref moveVel, moveSmooth);
		}
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.fixedDeltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
		slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
		ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }

	IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

	public void Recoil(Quaternion rot, float force)
	{
		if (!coll.onGround && !Input.GetButton("Jump"))
		{
			Vector2 direction = rot * Vector2.left * force;
			rb.velocity += direction;
		}
		else if(Input.GetButton("Jump"))
		{
			Vector2 direction = rot * Vector2.left * (force / fallMultiplier);
			rb.AddForce(direction, ForceMode2D.Impulse);
		}
	}

	public void Teleport(Vector3 location)
	{
		transform.position = location;
		rb.velocity = new Vector2(0, 0);
	}

}
