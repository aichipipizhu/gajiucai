using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator Anim;
    public float HorizontalSpeed;
    public float VerticalSpeed;
    public float MovementSharpness;

    public float DashingCooldownTime=3f; //Dash冷却时间
    float DashCDTimeCount;
    public float DashingTime =1f;
    float DashTimeCount;
    public float DashSpeedMultiplier = 2f;
    float speedModifier = 1f;

    Vector3 characterMovementVelocity { get; set; }

    public bool IsDashing { get; private set; }

    PlayerInputHandler m_playerInputHandler;
    
    [HideInInspector]
   public Vector3 movementInput;

    float xScale;
    float yScale;
    bool isRuning;
    private void Awake()
    {
        m_playerInputHandler = GetComponent<PlayerInputHandler>();
        
    }
    void Start()
    {
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        Anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        MovementHandler();
    }

    private void FixedUpdate() {
        if (IsDashing && DashTimeCount<DashingTime)
        {
            speedModifier = DashSpeedMultiplier;
            DashTimeCount += Time.deltaTime;
            Pool.instance.GetFormPool();
        }
    }

    void MovementHandler()
    {

        Dash();
        movementInput = m_playerInputHandler.GetMoveInput();
        movementInput = movementInput.normalized;
        if(movementInput.x!=0||movementInput.y!=0)
            Anim.SetBool("Runing",true);
        else
            Anim.SetBool("Runing",false);
            
        if(movementInput.x<-0.1f)
            transform.localScale=new Vector3(xScale,yScale,1);
        else if(movementInput.x>0.1f)
            transform.localScale=new Vector3(-xScale,yScale,1);
        
        Vector3 targetVelocity = new Vector3(movementInput.x*HorizontalSpeed,movementInput.y*VerticalSpeed,0f)*speedModifier;
        characterMovementVelocity = Vector3.Lerp(characterMovementVelocity,targetVelocity,MovementSharpness*Time.deltaTime);
        GroundCheck();
        
        transform.position += characterMovementVelocity * Time.deltaTime;
        
    }

    void Dash()
    {
        if (m_playerInputHandler.GetDashDown() && DashCDTimeCount>=DashingCooldownTime)
        {
            DashTimeCount = 0f;
            IsDashing = true;
        }
        if (DashTimeCount >= DashingTime)
        {
            DashCDTimeCount = 0f;
            speedModifier = 1f;
            IsDashing = false;
            DashTimeCount = 0f;
        }
        if (!IsDashing && DashCDTimeCount<DashingCooldownTime)
        {
            DashCDTimeCount += Time.deltaTime;
        }
    }

    void GroundCheck()
    {
        Vector3 startPoint = transform.position;

        Vector3 upPoint = startPoint + new Vector3(0f,1f,0f);
        Vector3 downPoint = startPoint + new Vector3(0f,-1f,0f);
        Vector3 leftPoint = startPoint + new Vector3(-1f,0f,0f);
        Vector3 rightPoint = startPoint + new Vector3(1f,0f,0f);

        RaycastHit2D rayUp = Physics2D.Raycast(upPoint, Vector3.forward, 0.1f, 1 << LayerMask.NameToLayer ("Ground"));
        
        RaycastHit2D rayDown = Physics2D.Raycast(downPoint, Vector3.forward, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
        
        RaycastHit2D rayLeft = Physics2D.Raycast(leftPoint, Vector3.forward, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
        
        RaycastHit2D rayRight = Physics2D.Raycast(rightPoint, Vector3.forward, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
        

        if (rayUp.collider == null)
        {
            characterMovementVelocity =new Vector3( characterMovementVelocity.x,Mathf.Clamp(characterMovementVelocity.y, Mathf.NegativeInfinity, 0f),0f);
        }
        if (rayDown.collider == null)
        {
            characterMovementVelocity = new Vector3(characterMovementVelocity.x, Mathf.Clamp(characterMovementVelocity.y, 0f , Mathf.Infinity), 0f);
        }
        if (rayLeft.collider == null)
        {
            characterMovementVelocity = new Vector3(Mathf.Clamp(characterMovementVelocity.x,0f,Mathf.Infinity),characterMovementVelocity.y,0f);
        }
        if (rayRight.collider == null)
        {
            characterMovementVelocity = new Vector3(Mathf.Clamp(characterMovementVelocity.x,Mathf.NegativeInfinity,0f),characterMovementVelocity.y,0f);
        }
    }
    
    
}
