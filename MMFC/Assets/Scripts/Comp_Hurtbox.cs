using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Hurtbox : MonoBehaviour, IHurtBox
{
    [SerializeField] private bool m_active = true;
    [SerializeField] private GameObject m_owner = null;
    private IHurtResponder m_hurtResponder;

    public bool Active { get => m_active; }

    public GameObject Owner { get => m_owner; }
    
    public Transform Transform { get => transform; }

    public IHurtResponder hurtResponder { get => m_hurtResponder; set => m_hurtResponder = value; } // public?

    public bool CheckHit(HitData hitData)
    {
        if (m_hurtResponder == null)
        {
            Debug.Log("No Responder");
        }
        return true;
    }
}

