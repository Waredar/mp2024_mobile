using System;
using WpfPvZ.BaseClasses;

namespace WpfPvZ
{
    public class GameManager
    {
        private readonly List<Plant> plants = [];
        private readonly List<Zombie> zombies = [];
        private readonly List<Projectile> projectiles = [];
        public readonly GameField gameField;
        private ZombieSpawner spawner;

        public GameManager(GameField field)
        {
            gameField = field ?? throw new ArgumentNullException(nameof(field));
            spawner = new ZombieSpawner(this, 5);
            spawner.Start();
        }

        public void AddPlant(Plant plant, int x, int y)
        {
            if (plant == null) throw new ArgumentNullException(nameof(plant));

            var cell = gameField.GetCell(x, y);
            if (cell == null || !gameField.AddPlantToCell(x, y, plant.PlantID))
            {
                Console.WriteLine($"Не удалось добавить  расстение {plant.Name} в клетку ({x}, {y}).");
                return;
            }

            plant.SetCell(cell);
            plants.Add(plant);
            Console.WriteLine($"Добавлено расстение {plant.Name} {plant.PlantID} to cell ({x}, {y}).");
        }
        public void RemovePlant(Plant plant)
        {
            if (plant == null) throw new ArgumentNullException(nameof(plant));
            if (!plants.Remove(plant)) return;

            if (plant.CurrentCell != null)
            {
                gameField.RemovePlantFromCell(plant.CurrentCell.Position.X, plant.CurrentCell.Position.Y, plant.PlantID);
                plant.SetCell(null);
            }

            plant.ClearEvents();

            Console.WriteLine($"Растение удалено {plant.Name} {plant.PlantID}.");
        }


        public void AddZombie(Zombie zombie)
        {
            if (zombie == null) throw new ArgumentNullException(nameof(zombie));
            zombies.Add(zombie);
            Console.WriteLine($"Добален зомби {zombie.Name} {zombie.ZombieID}.");
        }

        public void RemoveZombie(Zombie zombie)
        {
            if (zombie == null) throw new ArgumentNullException(nameof(zombie));

            if (zombies.Remove(zombie))
            {
                zombie.ClearEvents();

                Console.WriteLine($"Зомби удалён {zombie.Name} {zombie.ZombieID}.");
            }
        }

        public void AddProjectail(Projectile projectile)
        {
            if (projectile == null) throw new ArgumentNullException( nameof(projectile));
            projectiles.Add(projectile);
            Console.WriteLine("Снаряд добавлен");
        }

        public void RemoveProjectile(Projectile projectile)
        {
            if (projectile == null) throw new ArgumentNullException(nameof(projectile));

            if (projectiles.Remove(projectile))
            {

                Console.WriteLine($"Снаряд {projectile.Name} {projectile.Id} удалён.");
            }
        }

        public void ClearPlants()
        {
            foreach (var plant in plants)
            {
                RemovePlant(plant);
            }
            plants.Clear();
            Console.WriteLine("Все расстения удалены.");
        }

        public void ClearZombies()
        {
            foreach (var zombie in zombies)
            {
                RemoveZombie(zombie);
            }
            zombies.Clear();
            Console.WriteLine("Все зомби удалены.");
        }

        public void ClearAll()
        {
            ClearPlants();
            ClearZombies();
            Console.WriteLine("Все обьекты удалены.");
        }


        public List<Plant> GetPlants() => plants;
        public List<Zombie> GetZombies() => zombies;
        public List<Projectile> GetProjectiles() => projectiles;

        public void Update(float deltaTime)
        {
            CheckGameOverCondition();

            if (plants.Count > 0)
            {
                Console.WriteLine("Обновление расстений...");
                for (int i = 0; i < plants.Count; i++)
                {
                    plants[i].Update(this, deltaTime);
                }
            }

            if (zombies.Count > 0)
            {
                Console.WriteLine("Обновление зомби...");
                for (int i = 0; i < zombies.Count; i++)
                {
                    zombies[i].Update(this, deltaTime);
                }
            }

            if (projectiles.Count > 0)
            {
                Console.WriteLine("Обновление снарядов...");
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(this, deltaTime);
                }
            }


            Console.WriteLine("Обновление завершено!");
        }

        private void CheckGameOverCondition()
        {
            foreach (var zombie in GetZombies())
            {
                if (zombie.Position.X <= 0)
                {
                    Console.WriteLine("Зомби достиг левого края! Игра окончена.");
                    EndGame();
                    break;
                }
            }
        }

        private void EndGame()
        {
            Console.WriteLine("Игра завершена! Вы проиграли.");
            Environment.Exit(0);
        }

    }



}
