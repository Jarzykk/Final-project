using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorEnemyController
{
    public static class States
    {
        public static class Triggers
        {
            public const string IsHurt = nameof(IsHurt);
            public const string IsAttaking = nameof(IsAttaking);
            public const string BecameIdle = nameof(BecameIdle);
        }

        public static class Bools
        {
            public const string IsDead = nameof(IsDead);
            public const string IsMoving = nameof(IsMoving);
        }
    }
}
