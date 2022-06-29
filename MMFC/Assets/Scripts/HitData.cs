using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitData 
{
    public int damage;
    public Vector3 hitPoint;
    public Vector3 hitNormal;
    public IHurtBox hurtbox;
    public IHitDetector hitDetector;

    public bool Validate()
    {
        if (hurtbox != null)
            if (hurtbox.CheckHit(this))
                if (hurtbox.hurtResponder == null || hurtbox.hurtResponder.CheckHit(this))
                    if (hitDetector.hitResponder == null || hitDetector.hitResponder.CheckHit(this))
                        return true;


        return false;
        
    }
}

public interface  IHitResponder
{
    public int Damage { get; }
    public bool CheckHit(HitData data);
    public void Response(HitData data);
}

public interface IHitDetector
{
    public IHitResponder hitResponder { get; set; }
    public void CheckHit();  
}

public interface IHurtResponder
{
    public bool CheckHit(HitData data);

    public void Response(HitData data);
}

public interface IHurtBox
{
    public bool Active { get; }
    public GameObject Owner { get; }
    public Transform Transform { get; }
    public IHurtResponder hurtResponder { get; set; }
    public bool CheckHit(HitData hitData);
}