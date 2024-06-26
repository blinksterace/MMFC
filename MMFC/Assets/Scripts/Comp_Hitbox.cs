using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Hitbox : MonoBehaviour, IHitDetector
{
    [SerializeField] private BoxCollider m_collider;
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private HurtboxMask m_hurtboxMask = HurtboxMask.Enemy;
    private float m_thickness = 0.025f;
    private IHitResponder m_hitResponder;

    public IHitResponder hitResponder { get => m_hitResponder; set => m_hitResponder = value; }
    
    public bool CheckHit()
    {
        Vector3 _scaledSize = new Vector3(
            m_collider.size.x * transform.lossyScale.x,
            m_collider.size.y * transform.lossyScale.y,
            m_collider.size.z * transform.lossyScale.z);


        float _distance = _scaledSize.y - m_thickness;
        Vector3 _direction = transform.up;
        Vector3 _center = transform.TransformPoint(m_collider.center);
        Vector3 _start = _center - _direction * (_distance / 2);
        Vector3 _halfExtents = new Vector3(_scaledSize.x, m_thickness, _scaledSize.z) / 2;
        Quaternion _orientation = transform.rotation;

        HitData _hitdata = null;
        IHurtBox _hurtbox = null;
        RaycastHit[] _hits = Physics.BoxCastAll(_start, _halfExtents, _direction, _orientation, _distance, m_layerMask);
        foreach (RaycastHit _hit in _hits)
        {
            _hurtbox = _hit.collider.GetComponent<IHurtBox>();
            if (_hurtbox != null)
            {
                if (_hurtbox.Active) // cooldown
                {
                    if (m_hurtboxMask.HasFlag((HurtboxMask)_hurtbox.Type))
                    {
                        _hitdata = new HitData
                        {
                            damage = m_hitResponder == null ? 0 : m_hitResponder.Damage,
                            hitPoint = _hit.point == Vector3.zero ? _center : _hit.point,
                            hitNormal = _hit.normal,
                            hurtbox = _hurtbox,
                            hitDetector = this,
                        };

                        if (_hitdata.Validate())
                        {
                            _hitdata.hitDetector.hitResponder?.Response(_hitdata);
                            // Damage is taken here
                            _hitdata.hurtbox.hurtResponder?.SetDamage(_hitdata);
                            _hitdata.hurtbox.hurtResponder?.Response(_hitdata);
                            return true;
                        }

                        // return true;
                    }
                }
            }
        }
        return false;
    }


    public Comp_Hitbox(BoxCollider m_collider, LayerMask m_layerMask, HurtboxMask m_hurtboxMask, float m_thickness, IHitResponder m_hitResponder)
    {
        this.m_collider = m_collider;
        this.m_layerMask = m_layerMask;
        this.m_hurtboxMask = m_hurtboxMask;
        this.m_thickness = m_thickness;
        this.m_hitResponder = m_hitResponder;
    }

    public Comp_Hitbox DeepCopy()
    {
        return new Comp_Hitbox(this.m_collider, this.m_layerMask, this.m_hurtboxMask, this.m_thickness, this.m_hitResponder);
    }
}