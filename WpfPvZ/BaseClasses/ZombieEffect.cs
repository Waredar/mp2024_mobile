using System;

namespace WpfPvZ.BaseClasses
{
    public abstract class ZombieEffect
    {
        public float Duration { get; protected set; }
        public bool IsExpired => Duration <= 0;

        public abstract void Apply(Zombie zombie);
        public abstract void Update(Zombie zombie, float deltaTime);
    }

}
