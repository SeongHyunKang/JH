using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    private CharacterController controller;
    private bool isPierceAnimationPlaying = false;

    public bool isPiercing { get; private set; }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
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
}