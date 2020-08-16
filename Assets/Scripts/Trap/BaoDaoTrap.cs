using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaoDaoTrap : Trap
{
    Collider2D coll;
    public override void Init(int id)
    {
        ID = id;
        Name = "宝刀"+id.ToString();
        
    }

    protected override void UpdateEntity(float detaTime,Vector3 dir)
    {
        
    }
    
}
