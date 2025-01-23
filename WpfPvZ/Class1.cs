using System;
using System.Timers;
using WpfPvZ;
using WpfPvZ.BaseClasses;

public class ZombieSpawner
{
    private int remainingZombies;
    private readonly System.Timers.Timer spawnTimer;
    private readonly GameManager gameManager;
    private readonly Random random;

    public ZombieSpawner(GameManager gameManager, int totalZombies)
    {
        if (gameManager == null)
            throw new ArgumentNullException(nameof(gameManager));

        this.gameManager = gameManager;
        remainingZombies = totalZombies;
        random = new Random();

        spawnTimer = new System.Timers.Timer(5000);
        spawnTimer.Elapsed += SpawnZombies;
    }

    // Запуск спавнера
    public void Start()
    {
        Console.WriteLine("Спавнер зомби запущен.");
        spawnTimer.Start();
    }

    // Остановка спавнера
    public void Stop()
    {
        Console.WriteLine("Спавнер зомби остановлен.");
        spawnTimer.Stop();
    }

    // Метод для спавна зомби
    private void SpawnZombies(object? sender, ElapsedEventArgs e)
    {
        if (remainingZombies <= 0)
        {
            Stop();
            Console.WriteLine("Все зомби заспавнены.");
            return;
        }


        int zombiesToSpawn = random.Next(1, Math.Min(3, remainingZombies + 1));

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            int randomLine = random.Next(1, gameManager.gameField.Rows);
 

             var zombie = new TestZombie(Zombie.ZombieType.Walker, "Walker", "Slow but strong", 100, 10, 0.5f, 100f, new(-1, 0), new(gameManager.gameField.FieldWidth - 100, gameManager.gameField.Cells[0, 0].Height * randomLine - 50), 30f);

            gameManager.AddZombie(zombie);
            Console.WriteLine($"Заспавнен зомби: {zombie.Name} на позиции {zombie.Position}.");

            remainingZombies--;

            if (remainingZombies <= 0)
            {
                Console.WriteLine("Все зомби закончились.");
                Stop();
                break;
            }
        }
    }
}


