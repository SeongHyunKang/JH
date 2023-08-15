using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    #region Animation References
    [SerializeField] private Animator playerAnim;
    private CharacterController controller;
    #endregion

    #region Piercing Parameter
    private bool isPierceAnimationPlaying = false;
    public bool isPiercing { get; private set; }
    #endregion

    #region Attack Parameter
    public bool isAttacking;
    private float timeSinceLastCombo;
    public int currentCombo = 0;
    #endregion

    #region Roll Parameter
    private float timeSinceLastRoll = -10.0f;
    private const float rollCooldown = 2.0f;
    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        timeSinceLastCombo += Time.deltaTime;

        Combo1();
        Combo3();
        Pierce();
        Roll();
    }

    public void Pierce()
    {
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1) && !isPierceAnimationPlaying)
        {
            playerAnim.SetBool("isPiercing", true);
            StartPierceAnimation();
        }
        else if (!Input.GetMouseButton(0) || !Input.GetMouseButton(1))
        {
            playerAnim.SetBool("isPiercing", false);
            EndPierceAnimation();
        }
    }

    private void StartPierceAnimation()
    {
        isPierceAnimationPlaying = true;
    }

    private void EndPierceAnimation()
    {
        isPierceAnimationPlaying = false;
    }

    private void Combo1()
    {
        if(Input.GetMouseButton(0) && timeSinceLastCombo > 0.8f)
        {
            currentCombo++;
            isAttacking = true;

            if (currentCombo > 3)
            {
                //currentCombo = 1;
            }

            //Reset
            if (timeSinceLastCombo > 1f)
            {
                currentCombo = 1;
            }

            //Call Combo1 Triggers
            playerAnim.SetTrigger("combo01_" + currentCombo);

            //Reset Timer
            timeSinceLastCombo = 0;
        }        
    }

    private void Combo3()
    {
        if (Input.GetMouseButton(1) && timeSinceLastCombo > 0.8f)
        {
            currentCombo++;
            isAttacking = true;

            if (currentCombo > 4)
            {
                //currentCombo = 1;
            }

            //Reset
            if (timeSinceLastCombo > 1f)
            {
                currentCombo = 1;
            }

            //Call Combo3 Triggers
            playerAnim.SetTrigger("combo03_" + currentCombo);

            //Reset Timer
            timeSinceLastCombo = 0;
        }
    }

    private void Roll()
    {
        if (Time.time - timeSinceLastRoll < rollCooldown)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("FRoll");
            timeSinceLastRoll = Time.time;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("BRoll");
            timeSinceLastRoll = Time.time;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("LRoll");
            timeSinceLastRoll = Time.time;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("RRoll");
            timeSinceLastRoll = Time.time;
        }
    }

    //this will be used at animation event
    public void ResetCombo()
    {
        isAttacking = false;
    }
}