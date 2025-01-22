using System;
using System.Numerics;

namespace WpfPvZ.BaseClasses
{
    public abstract class Projectile
    {
        public string Name { get; init; }
        public int Damage { get; init; }
        public float Speed { get; init; }
        public Vector2 Direction { get; init; }
        public float Range { get; init;  }
        public Vector2 Position { get; set; }
        public uint Id { get; private set; } = nextId++;
        private static uint nextId = 1;

        public ProjectileType Type { get; set; }

        private float distanceTravelled;

        public List<ZombieEffect> Effects { get; set; } = [];

        public event Action<Projectile, Zombie>? OnHit;
        public event Action<Projectile>? OnDestroyed;

        public void Update(GameManager gameManager, float deltaTime)
        {
            Move(deltaTime);
            if (CheckCollision(gameManager) || CheckDistance())
            {
                Destroy(gameManager);
            }
        }

        private void Move(float deltaTime)
        {
            var movement = Direction * Speed * deltaTime;
            Position += movement;
            distanceTravelled += movement.Length();
            Console.WriteLine($"Снаряд ({Name} {Id}) передвинулся на координаты {Position}.");
        }


        public void ApplyEffects(Zombie zombie)
        {
            foreach (var effect in Effects)
            {
                zombie.ApplyEffect(effect);
            }
        }

        private bool CheckCollision(GameManager gameManager)
        {
            foreach (var zombie in gameManager.GetZombies())
            {
                if (Vector2.Distance(Position, zombie.Position) <= zombie.CollisionRadius)
                {
                    Console.WriteLine($"Снаряд ({Name} {Id}) попал по зомби {zombie.ZombieID}.");
                    zombie.TakeDamage(Damage, gameManager);
                    ApplyEffects(zombie);
                    OnHit?.Invoke(this, zombie);
                    return true;
                }
            }
            return false;
        }

        private bool CheckDistance()
        {
            if (distanceTravelled >= Range)
            {
                Console.WriteLine($"Снаряд ({Name} {Id}) достиг максимальной дальности.");
                return true;
            }
            return false;
        }

        private void Destroy(GameManager gameManager)
        {
            gameManager.RemoveProjectile(this);
            Console.WriteLine($"Снаряд ({Name} {Id}) уничтожен.");
            OnDestroyed?.Invoke(this);
        }

        public enum ProjectileType
        {
            Standard,
            Explosive,
            Piercing,
            Homing
        }
    }
}
