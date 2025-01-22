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
