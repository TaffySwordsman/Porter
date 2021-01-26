using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private bool m_AirControl = false;                         //Whether or not a player can steer while jumping;
	[Range(0, 1)]
	[SerializeField] private float airControlAmount = 1f;
	[SerializeField] private float m_JumpForce = 5f;                          //Amount of force added when the player jumps.
	[SerializeField] private float fallMultiplier = 2.5f;
	[SerializeField] private float lowJumpMultiplier = 2f;
	[SerializeField] private float walkSpeed = 3f;
	[SerializeField] private float runSpeed = 6f;
	[Range(0, 1)] [SerializeField] private float m_CrouchedSpeed = .36f;          //Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .1f;   //How much to smooth out the movement
	[Range(0, .3f)] [SerializeField] private float m_AnimationSmoothing = .1f;  //How much to smooth out the animations

	public LayerMask m_WhatIsGround;                          //A mask determining what is ground to the character
	public Transform m_GroundCheck;                           //A position marking where to check if the player is grounded.
	public Transform m_CeilingCheck;                          //A position marking where to check for ceilings
	public Collider2D m_CrouchDisableCollider;                //A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; //Radius of the overlap circle to determine if grounded
	bool m_Grounded;            //Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; //Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  //For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	private Animator anim;
	float animSpeed;
	float inputDir;

	bool running = false;
	bool jump = false;
	bool crouch = false;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		airControlAmount = (m_AirControl) ? airControlAmount : 0f;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	//Update is called once per frame
	void Update()
	{
		inputDir = Input.GetAxisRaw("Horizontal");
		running = Input.GetButton("Run");
		crouch = Input.GetButton("Crouch");
		if (Input.GetKeyDown(KeyCode.W))
			jump = true;

		//Jump gravity alteration
		if(m_Rigidbody2D.velocity.y < 0)
		{
			m_Rigidbody2D.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if(m_Rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
		{
			m_Rigidbody2D.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}

		//Set animation speed
		float animSpeed = Mathf.Abs((running) ? m_Rigidbody2D.velocity.x / runSpeed : m_Rigidbody2D.velocity.x / walkSpeed * .5f);
		anim.SetFloat("Speed", animSpeed, m_AnimationSmoothing, Time.deltaTime);

		//Set animation booleans
		anim.SetBool("inAir", !m_Grounded && m_Rigidbody2D.velocity.y < 0f);
		anim.SetBool("Jump", !m_Grounded && m_Rigidbody2D.velocity.y > 0f);
		anim.SetBool("Grounded", m_Grounded);

	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		//The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		//Move player
		Move(inputDir, crouch, jump);
		jump = false;
	}


	public void Move(float move, bool crouch, bool jump)
	{
		//If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			//If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				//Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchedSpeed;

				//Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				//Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			//Calculate velocity
			Vector3 targetVelocity = new Vector2(((running) ? runSpeed : walkSpeed) * move, m_Rigidbody2D.velocity.y);

			//Velocity smoothing
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, GetSmoothedTime(m_MovementSmoothing));

			//If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				//...flip the player.
				Flip();
			}
			//Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				//...flip the player.
				Flip();
			}
		}
		//If the player should jump...
		if (m_Grounded && jump)
		{
			Jump();
		}
	}

	//Player jump control
	void Jump()
	{
		//Add a vertical force to the player.
		m_Grounded = false;
		m_Rigidbody2D.velocity = m_Rigidbody2D.velocity + Vector2.up * m_JumpForce;
		//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
	}

	//Air control modification
	float GetSmoothedTime(float smoothTime)
	{
		if (m_Grounded)
			return smoothTime;

		if (airControlAmount == 0)
			return float.MaxValue;

		return smoothTime / airControlAmount;
	}

	//Player direction control
	private void Flip()
	{
		//Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		//Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
		transform.localScale = theScale;
	}
}