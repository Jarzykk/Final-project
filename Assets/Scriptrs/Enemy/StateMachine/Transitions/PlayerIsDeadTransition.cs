using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsDeadTransition : Transition
{
    private void Update()
    {
        if (Target.IsAlive == false)
            NeedTransit = true;
    }
}
