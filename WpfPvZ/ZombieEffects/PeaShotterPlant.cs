using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Automation.Peers;
using WpfPvZ.BaseClasses;

namespace WpfPvZ.ZombieEffects
{
    public class PeaShotterPlant : Plant
    {
        public int Damage { get; private set; }
        public float AttackSpeed { get; private set; }
        public float LastAttackTime { get; private set; }
        public PeaShotterPlant() 
        {
            this.Name = "Горострел";
            this.Description = "Стреляет горошинами";
            this.Type = PlantType.Shooter;

            this.Health = 100;
            this.maxHealth = Health;
            this.Cost = 100;
            this.Damage = 20;
            this.AttackSpeed = 1f;
            this.LastAttackTime = 1f;

            this.CollisionRadius = 20;
            this.OnUpdate += ShootPea;
        }

        private void ShootPea(GameManager gameManager, float deltaTime)
        {
            if (CurrentCell != null)
            {
                // Проверяем, есть ли зомби на линии перед атакой
                if (IsZombieOnLine(gameManager))
                {
                    if (LastAttackTime >= AttackSpeed)
                    {
                        Pea pea = new(Damage, 150f, gameManager.gameField.FieldWidth, CurrentCell.GetCenter());
                        gameManager.AddProjectail(pea);
                        LastAttackTime = 0;
                    }
                    LastAttackTime += deltaTime;
                }
            }
        }

        private bool IsZombieOnLine(GameManager gameManager)
        {
            // Получаем список всех зомби на поле
            var zombies = gameManager.GetZombies();

            // Получаем позицию центра текущей клетки
            var plantPosition = CurrentCell.GetCenter();
            float tolerance = 10.0f; // Допустимое отклонение по оси Y для определения линии

            // Проверяем, есть ли зомби на той же линии (с учётом допустимого отклонения)
            foreach (var zombie in zombies)
            {
                if (Math.Abs(zombie.Position.Y - plantPosition.Y) <= tolerance)
                {
                    return true;
                }
            }

            return false;
        }

    }



    public class Pea : Projectile
    {
        public Pea(int damage, float speed, float range, Vector2 startPosition)
        {
            this.Name = "Горох";
            this.Damage = damage;
            this.Speed = speed;
            this.Range = range;
            this.Position = startPosition;
            this.Direction = new(1,0);
            this.Type = ProjectileType.Standard;
        }
    }
}
