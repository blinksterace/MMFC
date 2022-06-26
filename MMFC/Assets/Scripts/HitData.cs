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
    int Damage { get; }
    bool CheckHit(HitData data);
    void Response(HitData data);
}

public interface IHitDetector
{
      IHitResponder hitResponder { get; set; }
        
}

public interface IHurtResponder
{
    bool CheckHit(HitData data);

    void Response(HitData data);
}

public interface IHurtBox
{
    bool Active { get; }
     GameObject Owner { get; }
     Transform Transform { get; }
     IHurtResponder hurtResponder { get; set; }
    bool CheckHit(HitData hitData);
}