using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    //Animation References
    [SerializeField] private Animator playerAnim;
    private CharacterController controller;

    //Piercing Parameter
    private bool isPierceAnimationPlaying = false;
    public bool isPiercing { get; private set; }

    //Attack Parameters
    public bool isAttacking;
    private float timeSinceLastCombo;
    public int currentCombo = 0;


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
                currentCombo = 1;
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
                currentCombo = 1;
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

    //this will be used at animation event
    public void ResetCombo1()
    {
        isAttacking = false;
    }
}