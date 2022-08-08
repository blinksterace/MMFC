using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Skelly : MonoBehaviour, ITargetable, IHurtResponder
{
    [SerializeField] private bool m_targetable = true;
    [SerializeField] private Transform m_targetTransform;
    [SerializeField] private Rigidbody m_skelly;
    [SerializeField] private GameObject bloodps = null;
    public int m_health = 100;
    int damage = 0;
    int IHurtResponder.Damage { get => damage; }

    private List<Comp_Hurtbox> m_hurtboxes = new List<Comp_Hurtbox>();

    bool ITargetable.Targetable { get => m_targetable; }
    Transform ITargetable.targetTransform { get => m_targetTransform; }
    
    private void Start()
    {
        m_hurtboxes = new List<Comp_Hurtbox>(GetComponentsInChildren<Comp_Hurtbox>());
        foreach (Comp_Hurtbox _hurtbox in m_hurtboxes)
            _hurtbox.hurtResponder = this;
    }

    bool IHurtResponder.CheckHit(HitData data)
    {
        return true;
    }
    void IHurtResponder.Response(HitData data)
    {
        // Debug.Log("Damage taken");
        m_health -= damage;
        Debug.Log(damage.ToString());
        Instantiate(bloodps, data.hitPoint, Quaternion.FromToRotation(Vector3.up, data.hitNormal));

        if (m_health <= 0)
            Destroy(this.gameObject);
    }

    void IHurtResponder.SetDamage(HitData data)
    {
        damage = data.damage;
    }

    public int GetHealth()
    {
        return m_health;
    }
}
