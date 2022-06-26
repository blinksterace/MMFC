using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Skelly : MonoBehaviour, ITargetable, IHurtResponder
{
    [SerializeField] private bool m_targetable = true;
    [SerializeField] private Transform m_targetTransform;
    [SerializeField] private Rigidbody m_skelly;

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
        Debug.Log("hurt Response");
    }
}
