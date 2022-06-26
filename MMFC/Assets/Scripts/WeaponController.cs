using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Cutlass;
    public bool can_attack;
    public float cooldown = 1.0f;
    public Collider swordBox;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (can_attack)
            {
                CutlassSwing();
            }
        }
    }

    public void CutlassSwing()
    {
        can_attack = false;
        Animator attack_anim = Cutlass.GetComponent<Animator>();
        attack_anim.SetTrigger("Melee");
        LaunchAttack(swordBox);
        StartCoroutine(ResetCooldown());

    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        can_attack = true;
    }


    private void LaunchAttack(Collider col)
    {

    }

}
