using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Cutlass;
    private bool downAndAttack = false;
    public bool can_attack;
    public float cooldown = 0.72f;
    public Collider swordBox;
    public bool mouseDown = false;
    [SerializeField] private GameObject bloodps = null;
     public float attackRange = 3f;
    public float attackDelay = 0.4f;
    public float attackDamage = 10f;
    public LayerMask attackLayer;
    public Camera fpsCam;





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
            CutlassSwing();
        }

        if(downAndAttack == true)
            Debug.Log(downAndAttack);
    }

    public void CutlassSwing()
    {
        // Disabling attack
        can_attack = false;

        // Animation
        Animator attack_anim = Cutlass.GetComponent<Animator>();
        attack_anim.SetTrigger("Melee");
        Invoke(nameof(AttackRaycast), attackDelay);
        
        // Cooldown
        StartCoroutine(ResetCooldown());

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
