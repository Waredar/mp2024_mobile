using System;
using System.Numerics;


namespace WpfPvZ.BaseClasses
{

    public abstract class Zombie
    {
        public ZombieType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint ZombieID { get; private set; }
        private static uint nextId = 1;

        public int CurrentHealth { get; set; }
        private int defaultHealth;
        public int CurrentDamage { get; set; }
        private int defaultDamage;
        public float CurrentMoveSpeed { get; set; }
        private float defaultMoveSpeed;

        public float CurrentAttackSpeed { get; set; }
        private float defaultAttackSpeed;
        private float timeSinceLastAttack;

        public float CollisionRadius { get; set; }
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }

        public List<ZombieEffect> ActiveEffects { get; private set; } = [];

        public Action<Zombie>? OnUpdate { get; set; }

        public event Action<Zombie, int>? OnDamaged;
        public event Action<Zombie>? OnDestroyed;


        public Zombie(
            ZombieType type,
            string name,
            string description,
            int health,
            int damage,
            float atackSpeed,
            float moveSpeed,
            Vector2 direction,
            Vector2 position,
            float collisionRadius
            )
        {
            Type = type;
            ZombieID = nextId++;
            Name = name;
            Description = description;

            CurrentHealth = health;
            defaultHealth = health;
            CurrentDamage = damage;
            CurrentMoveSpeed = moveSpeed;
            defaultMoveSpeed = moveSpeed;
            CurrentAttackSpeed = atackSpeed;
            defaultAttackSpeed = atackSpeed;
            timeSinceLastAttack = 0;

            CollisionRadius = collisionRadius;
            Position = position;
            Direction = Vector2.Normalize(direction);

            Console.WriteLine($"Зомби создан");
        }


        public void ApplyEffect(ZombieEffect effect)
        {
            ActiveEffects.Add(effect);
            effect.Apply(this);
        }

        public void UpdateEffects(float deltaTime)
        {
            foreach (var effect in ActiveEffects.ToList())
            {
                effect.Update(this, deltaTime);
                if (effect.IsExpired)
                {
                    ActiveEffects.Remove(effect);
                }
            }
        }

        public void Update(GameManager gameManager, float deltaTime)
        {
            UpdateEffects(deltaTime);

            timeSinceLastAttack += deltaTime;
            FindPlantInRange(gameManager);
            Move(deltaTime);
            if (Position.X < -20)
            {
                Console.WriteLine($"Зомби вышел за границы мира");
                Destroy(gameManager);
            }
        }

        private void Move(float deltaTime)
        {
            if (timeSinceLastAttack >= CurrentAttackSpeed && CurrentMoveSpeed > 0)
            {
                var movement = Direction * CurrentMoveSpeed * deltaTime;
                Position = Position + movement;
                Console.WriteLine($"Зомби{ZombieID} ({Name}) передвинулся на координаты {Position}.");
            }
        }

        private void FindPlantInRange(GameManager gameManager)
        {
            foreach (var plant in gameManager.GetPlants())
            {
                if (plant.CurrentCell == null)
                    continue;

                var plantPosition = plant.CurrentCell.GetCenter();

                if (Vector2.Distance(Position, plantPosition) <= plant.CollisionRadius)
                {
                    Console.WriteLine($"Зомби {ZombieID} ({Name}) нашёл {plant.PlantID} {plant.Name}");
                    if (timeSinceLastAttack >= CurrentAttackSpeed)
                    {
                        CurrentMoveSpeed = 0;
                        Attack(plant, gameManager);
                        return;
                    }
                }
            }
            CurrentMoveSpeed = defaultMoveSpeed;
        }

        public void Attack(Plant plant, GameManager gameManager)
        {
            if (plant != null)
            {
                Console.WriteLine($"Зомби {ZombieID} ({Name}) атакует растение {plant.PlantID} {plant.Name}");
                plant.TakeDamage(CurrentDamage, gameManager);
            }
        }

        public void TakeDamage(int damage, GameManager gameManager)
        {
            CurrentHealth -= damage;
            OnDamaged?.Invoke(this, damage);

            Console.WriteLine($"Зомби {ZombieID} ({Name}) получил {damage} урона. Текущее здоровье {CurrentHealth}");

            if (CurrentHealth <= 0)
            {
                Destroy(gameManager);
            }
        }

        private void Destroy(GameManager gameManager)
        {
            OnDestroyed?.Invoke(this);
            gameManager.RemoveZombie(this);
            Console.WriteLine($"Зомби {ZombieID} ({Name}) уничтожен.");
        }

        public void ClearEvents()
        {
            OnDestroyed = null;
            OnUpdate = null;
        }

        public enum ZombieType
        {
            Walker,
            Runner,
            Tank,
            Flyer
        }
    }
}