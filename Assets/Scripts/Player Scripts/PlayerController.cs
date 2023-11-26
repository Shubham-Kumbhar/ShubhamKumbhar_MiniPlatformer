using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //Defining All required Variables 
    [Header("Player")]
    [SerializeField] private float _playerSpeed;
    private float dir;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private bool _isFacingRight;
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    private bool _canMove;
    public bool CanMove { get { return _canMove; } set { _canMove = value; } }
    [SerializeField] private LayerMask _ground;



    [Header("Jump")]
    [SerializeField] private float _jumpPower;
    private bool _isJumping;
    public bool IsJumping { get { return _isJumping; } }



    [Header("Dash")]
    [SerializeField] private float _radius;
    [SerializeField] private float _dashPower;
    [SerializeField] private float _dashTime;
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private Color _canUseColour;
    private Color _currentColour;
    private float _dashTimeRest;
    [SerializeField] GameObject dashableObj, Arrow;
    private bool nearTodashableObj, isChosingDir, _isDashing;
    public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
    Vector3 dashDir;


    [Header("Wall Sliding")]
    private bool _isWallSliding;
    public bool IsWallSliding { get { return _isWallSliding; } set { _isWallSliding = value; } }
    [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private Transform _wallcheck;
    [SerializeField] private LayerMask _wallLayer;


    [Header("Wall Sliding")]
    [SerializeField] private float _DeathTimer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _dashTimeRest = _dashTime;

    }
    //subscribe  Events 
    private void OnEnable()
    {
        ActionManager.Jump += Jump;
        ActionManager.Move += Move;
        ActionManager.Dash += Dash;
        ActionManager.WallSliding += WallSliding;
        ActionManager.Death += Death;
    }
    //Unsubscribe Events 
    private void OnDisable()
    {
        ActionManager.Jump -= Jump;
        ActionManager.Move -= Move;
        ActionManager.Dash -= Dash;
        ActionManager.WallSliding -= WallSliding;
        ActionManager.Death -= Death;

    }
    // Update is called once per frame
    void Update()
    {
        DashableObjectCheck();
    }
    private void FixedUpdate()
    {
        //check Player Interacton from wall or Ground  
        _isGrounded = isGroudned();
        _isWallSliding = isWall();
        Idle();
        if (!IsDashing)Flip();
    }

    //set the player Facing Direction
    void Flip()
    {
        if (_isFacingRight && dir > 0 || !_isFacingRight && dir < 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    //check for Idle State
    void Idle()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) _canMove = true;
        else if (Input.GetAxis("Horizontal") == 0) _canMove = false;
    }

    // Allow Player To move based of Inputs 
    void Move()
    {
        dir = Input.GetAxis("Horizontal") * _playerSpeed;
        _rb.velocity = new Vector2(dir * Time.fixedDeltaTime, _rb.velocity.y);
    }
    //Allow player Jump Based of inputs 
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isJumping = true;
            _rb.AddForce(transform.up * _jumpPower);
        }
    }
    //check If player is on ground or not 
    bool isGroudned()
    {
        float jumpCheckHieght = 0.2f;
        //creats A box Under the Player and check for collision
        RaycastHit2D hit2D = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size,0f,Vector2.down,jumpCheckHieght,_ground);
        return hit2D;
    }
    void DashableObjectCheck()
    {
        //Shoots a Ray Around the player and checks collided Object is dashable 
        RaycastHit2D[] Rays = Physics2D.CircleCastAll(transform.position, _radius, Vector3.forward);
        foreach (RaycastHit2D ray in Rays)
        {
            nearTodashableObj = false;
            if (ray.collider.tag == "Dashable")
            {
                nearTodashableObj = true;
                dashableObj = ray.collider.transform.gameObject;
                //change colour of object 
                _currentColour = dashableObj.GetComponent<SpriteRenderer>().color;
                dashableObj.GetComponent<SpriteRenderer>().color = _canUseColour;
                break;
            }
            else
            {
                //set Back Object's colour to orignal Object colour
                if (dashableObj != null)
                    dashableObj.GetComponent<SpriteRenderer>().color = _currentColour;
            }
        }
        DashDirectionSelection();
    }

    //set Dash Direction
    void DashDirectionSelection()
    {
        if (nearTodashableObj)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Stop Time 
                Time.timeScale = 0;
                //set Direction
                Arrow.SetActive(true);
                Arrow.transform.position = dashableObj.transform.position;
                isChosingDir = true;

            }
            else if (isChosingDir && Input.GetMouseButtonUp(0))
            {
                // Resume Time
                Time.timeScale = 1;
                isChosingDir = false;
                _isDashing = true;
                _rb.velocity = Vector2.zero;
                //Change player Positon to collided Object
                transform.position = dashableObj.transform.position + _offSet;
                // check If Dahsable object have obstracle component and invoke onUSed Function
                if (dashableObj.GetComponent<Obstracle>() != null) dashableObj.GetComponent<Obstracle>().OnUsed();
                //set direction to Dash
                dashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                dashDir.z = 0;
                // Make sure Facing correct Posion
                if (_isFacingRight && dashDir.x > 0 || !_isFacingRight && dashDir.x < 0f)
                {
                    _isFacingRight = !_isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;
                }
                dashDir = dashDir.normalized;
                // Disable Arrow
                Arrow.SetActive(false);
            }
        }
    }
    //performs a Dash
    void Dash()
    {
        if (_isDashing)
        {
            if (_dashTime > 0)
            {
                //add Dash Velocty
                _dashTime -= Time.deltaTime;
                _rb.velocity = dashDir * _dashPower * Time.deltaTime;
            }
            else
            {
                //Stop Dash
                _isDashing = false;
                _dashTime = _dashTimeRest;
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
            }
        }
    }
    //Check if Player is Faceing Wall
    bool isWall()
    {
        return Physics2D.OverlapCircle(_wallcheck.position, 0.2f, _wallLayer);
    }
    void WallSliding()
    {
        if (isWall() && !_isGrounded && !_canMove)
        {
            //reduce Speed of Falling
            _isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -_wallSlidingSpeed, float.MaxValue));

        }
        else
        {
            _isWallSliding = false;
        }
    }

    //Disable player
    void Death()
    {
        gameObject.SetActive(false);
    }
}