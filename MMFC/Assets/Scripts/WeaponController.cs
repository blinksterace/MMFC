using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Cutlass;
    private bool downAndAttack = false;
    public bool can_attack;
    public float cooldown = 1.0f;
    public Collider swordBox;
    public bool mouseDown = false;
    [SerializeField] private GameObject bloodps = null;
     public float attackRange = 3f;
    public float attackDelay = 0.4f;
    public float attackDamage = 10f;
    public LayerMask attackLayer;
    public Camera fpsCam;
    private int clickCount = 0;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("can_attack: " + can_attack);
        downAndAttack = Input.GetMouseButtonDown(1) && can_attack;
        if (downAndAttack)
        {
           Debug.Log("clickcount:" + clickCount);     
            CutlassSwing();
            StartCoroutine(WaitForClick());
            clickCount++;
            
        }
    }


    private IEnumerator WaitForClick()
    {
        float timer = 0;
        while (timer < .2f)
        {
            // If the player clicks again within .2 second, increment clickCount and break the loop
            if (Input.GetMouseButtonDown(1))
            {
                CutlassSwing();
                break;
            }

            // Increment the timer by the time since the last frame
            timer += Time.deltaTime;

            // Yield control back to the Unity engine until the next frame
            yield return null;
        }

        // If the player didn't click again within .2 second, reset clickCount to 0
        if (timer >= .2f )
        {
            clickCount = 0;
            ResetCooldown();
            yield break;
        } 
    }
    public void CutlassSwing()
    {
        Debug.Log("Click Count: " + clickCount);
        // Disabling attack
        //can_attack = false;

        // Animation
        Animator attack_anim = Cutlass.GetComponent<Animator>();

        // Perform attack based on clickCount
        switch (clickCount)
        {
            case 0:
                // First attack
                attack_anim.SetTrigger("Melee");
                Invoke(nameof(AttackRaycast), attackDelay);
                StartCoroutine(ResetCooldown());
                break;
            case 1:
                // Second attack
                 attack_anim.SetTrigger("SecondMelee");
                Invoke(nameof(AttackRaycast), attackDelay);
                StartCoroutine(ResetCooldown());
                clickCount = 0;
                break;
            /*case 3:
                // Third attack
                attack_anim.SetTrigger("Melee");
                Invoke(nameof(AttackRaycast), attackDelay);
                clickCount = 0; // Reset the combo
                break;
            */
            default:
                // Reset the combo if clickCount is not 1, 2, or 3
                //clickCount = 0;
                break;
        }
            // Cooldown


    }

    void AttackRaycast()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, attackRange, attackLayer))
        {
            Debug.Log(hit.transform.name);

            SkeleTarget target = hit.transform.GetComponent<SkeleTarget>();
            if (target != null)
            {
                target.TakeDamage(attackDamage);
                Debug.Log("damage taken");
            }
        }
    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        Animator attack_anim = Cutlass.GetComponent<Animator>();
        attack_anim.SetTrigger("Idle");
        can_attack = true;
    }


    private void LaunchAttack(Collider col)
    {

    }

    

    // Might need OnCollisionEnter instead...
    private void OnTriggerEnter(Collider other)
    {
        if (can_attack)
        {
             Debug.Log("Damage taken");

           

        }
    }

    public bool getDownAndAttack()
    {
        return downAndAttack;
    }

}
