using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour, IHurtResponder
{
    public GameObject Cutlass;
    public bool can_attack;
    public float cooldown = 1.0f;
    public Collider swordBox;
    public bool mouseDown = false;
    [SerializeField] private GameObject bloodps = null;
    int damage = 0;
    int IHurtResponder.Damage { get => damage; }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("can_attack: " + can_attack);
        mouseDown = Input.GetMouseButtonDown(0);
        if (mouseDown)
        {
            if (can_attack)
            {
                CutlassSwing();
            }
        }
    }

    public void CutlassSwing()
    {
        // Disabling attack
        can_attack = false;

        // Animation
        Animator attack_anim = Cutlass.GetComponent<Animator>();
        attack_anim.SetTrigger("Melee");

        // ??
        
        // LaunchAttack(swordBox);

        // Cooldown
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

    bool IHurtResponder.CheckHit(HitData data)
    {
        return true;
    }
    void IHurtResponder.Response(HitData data)
    {
        // 
    }
    void IHurtResponder.SetDamage(HitData data)
    {
        damage = data.damage;
    }

    // Might need OnCollisionEnter instead...
    private void OnTriggerEnter(Collider other)
    {
        if (can_attack)
        {
            // Debug.Log("Damage taken");

            //Instantiate(bloodps, other.gameObject.GetComponent<Comp_Skelly>().data.hitPoint, Quaternion.FromToRotation(Vector3.up, other.gameObject.GetComponent<Comp_Skelly>().GetDamage()));
            // Debug.Log(data.damage.ToString());

        }
    }

}
