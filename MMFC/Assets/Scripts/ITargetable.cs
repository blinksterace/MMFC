using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
   public bool Targetable { get; }
   
   
   public Transform targetTransform { get; }
}
