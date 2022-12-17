using UnityEngine;

namespace _Scripts
{
    public static class AnimationConstants
    {
        public static readonly int IsRunning = Animator.StringToHash("IsRunning");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int AttackEmit = Animator.StringToHash("AttackEmit");
        public static readonly int AttackFinished = Animator.StringToHash("AttackFinished");
        public static readonly int TakeHit = Animator.StringToHash("TakeHit");
        public static readonly int Die = Animator.StringToHash("Die");
        
    }
}