using System;
using WpfPvZ.BaseClasses;

namespace WpfPvZ.ZombieEffects
{
    public class SlowEffect : ZombieEffect
    {
        private readonly float slowFactor;
   
        public SlowEffect(float duration, float slowFactor)
        {
            Duration = duration;
            this.slowFactor = slowFactor;
        }

        public override void Apply(Zombie zombie)
        {
            zombie.CurrentMoveSpeed *= slowFactor;
        }

        public override void Update(Zombie zombie, float deltaTime)
        {
            Duration -= deltaTime;
            if (IsExpired)
            {
                zombie.CurrentMoveSpeed /= slowFactor;
            }
        }
    }

}
