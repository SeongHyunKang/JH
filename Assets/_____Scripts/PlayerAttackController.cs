using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    public bool isPiercing;

    public void Update()
    {
        Pierce();
    }

    public void Pierce()
    {
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            playerAnim.SetBool("isPiercing", true);
        }
        else
        {
            playerAnim.SetBool("isPiercing", false);
        }
    }
}