using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hInput, vInput;
    CharacterController controller;
    #endregion

    #region GroundCheck
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    #endregion

    #region Gravity
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    #endregion

    #region State
    [HideInInspector] public Animator anim;

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    #endregion

    #region AttackCombos
    private int combo1Counter = 0;
    private int combo2Counter = 0;
    private float lastComboTime;
    private float comboResetTime = 2.0f; // 2�� �Ŀ� �޺��� �ʱ�ȭ�ǵ��� ����. ���ϴ´�� ���� ����
    #endregion

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        currentState.UpdateState(this);

        HandleAttackInput();

        anim.SetFloat("hInput", hInput);
        anim.SetFloat("vInput", vInput);
    }

    void HandleAttackInput()
    {
        bool leftClick = Input.GetMouseButtonDown(0);
        bool rightClick = Input.GetMouseButtonDown(1);

        // ���ÿ� ���� ���
        if (leftClick && rightClick)
        {
            // ������ ��� ���� �ִϸ��̼��� �����Ű����
            anim.SetTrigger("StabAttack"); // �� Ʈ���Ÿ� �ִϸ����Ϳ� �߰��ϼ���
            ResetCombo();
            return;
        }

        // ��Ŭ��: �޺�1 ����
        if (leftClick)
        {
            combo1Counter++;
            if (combo1Counter > 3) combo1Counter = 1;

            // �ִϸ����Ϳ� �޺�1�� ������ ���� �ִϸ��̼��� �����Ű����
            anim.SetInteger("LClickCount", combo1Counter);

            lastComboTime = Time.time;
        }

        // ��Ŭ��: �޺�2 ����
        if (rightClick)
        {
            combo2Counter++;
            if (combo2Counter > 4) combo2Counter = 1;

            // �ִϸ����Ϳ� �޺�2�� ������ ���� �ִϸ��̼��� �����Ű����
            anim.SetInteger("RClickCount", combo2Counter);

            lastComboTime = Time.time;
        }

        // �޺� �ʱ�ȭ
        if (Time.time - lastComboTime > comboResetTime)
        {
            ResetCombo();
        }
    }

    void ResetCombo()
    {
        combo1Counter = 0;
        combo2Counter = 0;
        anim.SetInteger("LClickCount", 0);
        anim.SetInteger("RClickCount", 0);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hInput;

        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool isGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y * groundYOffset, transform.position.z);

        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Gravity()
    {
        if (!isGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if(velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }*/
}
