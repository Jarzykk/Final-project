using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorPlayerController
{
    public static class Params
    {
        public const string Speed = nameof(Speed);
    }

    public static class States
    {
        public static class Triggers
        {
            public const string IsAttaking = nameof(IsAttaking);
            public const string Jump = nameof(Jump);
            public const string Fall = nameof(Fall);
            public const string LandedAfterFall = nameof(LandedAfterFall);
            public const string Hurt = nameof(Hurt);
        }

        public static class Bools
        {
            public const string IsDead = nameof(IsDead);
            public const string IsGrounded = nameof(IsGrounded);
        }
    }
}
