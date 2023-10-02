using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HitResponder : MonoBehaviour, IHitResponder
{
    [SerializeField] private bool m_attack;
    [SerializeField] private int m_damage = 10;
    [SerializeField] private Comp_Hitbox _hitbox;
    [SerializeField] private WeaponController cutlass;
    private Comp_Hitbox[] hitboxStates = new Comp_Hitbox[100];
    int runningIndex = 0;

    int IHitResponder.Damage { get => m_damage; }

    private void Start()
    {
        _hitbox.hitResponder = this;
    }

    private void Update()
    {
        if (runningIndex >= hitboxStates.Length)
            runningIndex = 0;

        hitboxStates[runningIndex++] = _hitbox.DeepCopy();

        if (cutlass.getDownAndAttack())
            m_attack = true;

        if (m_attack)
        {
            for (int i = 0; i < hitboxStates.Length; i++)
            {
                if (hitboxStates[i] == null)
                    break;

                // Need a cooldown for check hit bc attack dmg is done there, must have same cooldown as animation
                if (hitboxStates[i].CheckHit())
                {
                    m_attack = false;
                    break;
                }
            }   
        }
        m_attack = false;
    }

    bool IHitResponder.CheckHit(HitData data)
    {
        return true;
    }

    void IHitResponder.Response(HitData data)
    {

    }

}
