using System;

namespace WpfPvZ.BaseClasses
{
    public abstract class Plant
    {
        public PlantType Type { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public uint PlantID { get; set; } = nextId++;
        private static uint nextId = 1;


        public int Cost { get; init; }
        public int Health { get; set; }
        public int maxHealth { get; init; }

        public float CollisionRadius { get; init; }
        public Cell? CurrentCell { get; private set; }

        public Action<GameManager, float>? OnUpdate { get; set; }
        public event Action<Plant>? OnDestroyed;
        public event Action<Plant, int>? OnDamaged;

        public void SetCell(Cell? cell)
        {
            CurrentCell = cell;
        }

        public void Update(GameManager gameManager, float deltaTime)
        {
            OnUpdate?.Invoke(gameManager, deltaTime);
        }

        public void TakeDamage(int damage, GameManager gameManager)
        {
            Health -= damage;

            OnDamaged?.Invoke(this, damage);

            if (Health <= 0)
            {
                Console.WriteLine($"{Name} {PlantID} уничтожено!");
                OnDestroyed?.Invoke(this);
                Destroy(gameManager);
            }
            else
            {
                Console.WriteLine($"{Name} {PlantID} получило урон! Текущее здоровье: {Health}");
            }
        }

        private void Destroy(GameManager gameManager)
        {
            Console.WriteLine($"{Name} {PlantID} уничтожено.");
            OnDestroyed?.Invoke(this);
            gameManager.RemovePlant(this);
        }

        public void ClearEvents()
        {
            OnDestroyed = null;
            OnUpdate = null;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Растение: {Name}, Тип: {Type}, Здоровье: {Health}, Стоимость: {Cost}");
        }

        public enum PlantType
        {
            Shooter,
            Defender,
            Support,
            Utility
        }
    }
}
