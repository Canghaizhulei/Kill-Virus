using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFactory
{
    private static EnemyFactory instance;

    public static EnemyFactory GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyFactory();
            }
            return instance;
        }
    }
    private EnemyFactory() { }

    private ulong allEnemyCount;
    private ulong currentEnemyCount;
    private ulong deathEnemyCount;

    private int waveInterval ;


    public string[] EnemyObjs = new []{"NormalEnemy1","NormalEnemy2"};

    private List<Enemy> allAlivEnemies = new List<Enemy>(120);

    private bool isCreating;
    public void  StartUp()
    {
        
    }
    
    void Init()
    {
        allEnemyCount = PlayerController.GetInstance.PlayerData.Level*20 + 100;
        currentEnemyCount = 0;
        deathEnemyCount = 0;
        waveInterval = 20;
        isCreating = true;
    }

    public void StartCreat()
    {
        Init();
        GameController.GetInstance.StartCoroutine(CreatWave());
    }

    IEnumerator CreatWave()
    {
            yield return new WaitForSeconds(2);
            // 第1波
            while (currentEnemyCount<allEnemyCount / 5)
            {
                 if (!isCreating) yield break;
                CreatRadomEnemy(Random.Range( (int)allEnemyCount / 5/waveInterval, (int)allEnemyCount / 5 / waveInterval)*2);
                yield return new WaitForSeconds(1);
            }

            // 第2波
            while (currentEnemyCount < allEnemyCount / 5*2)
            {
                 if (!isCreating) yield break;
                CreatRadomEnemy(Random.Range((int)allEnemyCount / 5 / waveInterval, (int)allEnemyCount / 5 / waveInterval) * 2);
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(2);

            // 第3波
            while (currentEnemyCount < allEnemyCount / 5*4)
            {
                 if (!isCreating) yield break;
                CreatRadomEnemy(Random.Range((int)allEnemyCount *2 / waveInterval, (int)allEnemyCount / waveInterval) * 4);
                yield return new WaitForSeconds(1);
            }

            // 第4波
            while (currentEnemyCount < allEnemyCount)
            {
                 if (!isCreating) yield break;
                CreatRadomEnemy(Random.Range((int)allEnemyCount / 5 / waveInterval, (int)allEnemyCount / 5 / waveInterval) * 2);
                yield return new WaitForSeconds(1);
            }
           
    }

    public void CreatRadomEnemy(int count = 1)
    {
        while (count >0)
        {
            if (currentEnemyCount >= allEnemyCount) break;
            var obj =ObjectPool.GetInstance() .GetObj(EnemyObjs[Random.Range(0, EnemyObjs.Length)]);
            Enemy enemy = obj.GetComponent<Enemy>();
            

            ulong life = 0;
            float rad = Random.Range(0, 100);
            if (rad < 70)
            {
                life = (ulong)Random.Range(PlayerController.GetInstance.PlayerData.Level, PlayerController.GetInstance.PlayerData.Level* PlayerController.GetInstance.PlayerData.Level * 100);
            }
            else if (rad < 90)
            {
                life = (ulong)Random.Range(PlayerController.GetInstance.PlayerData.Level * 5, PlayerController.GetInstance.PlayerData.Level * PlayerController.GetInstance.PlayerData.Level * 200);
            }
            else if (rad < 95)
            {
                life = (ulong)Random.Range(PlayerController.GetInstance.PlayerData.Level * 10, PlayerController.GetInstance.PlayerData.Level * PlayerController.GetInstance.PlayerData.Level *400);
            }
            else if (rad < 98)
            {
                life =
                    (ulong)
                        Random.Range(PlayerController.GetInstance.PlayerData.Level * 50,
                            PlayerController.GetInstance.PlayerData.Level * PlayerController.GetInstance.PlayerData.Level * 600);
            }
            else
            {
                life =
                    (ulong)
                        Random.Range(PlayerController.GetInstance.PlayerData.Level * 100,
                            PlayerController.GetInstance.PlayerData.Level * PlayerController.GetInstance.PlayerData.Level *1000);
            }

            enemy.Init(life, Random.Range(0.4f, 1.2f));
            allAlivEnemies.Add(enemy);
            currentEnemyCount++;
            count --;
        }
        
    }

    public void CreatEnemy(ulong life, float radius, Vector3 dir, Vector3 pos)
    {
        if (currentEnemyCount >= allEnemyCount) return;
        var obj = ObjectPool.GetInstance().GetObj(EnemyObjs[Random.Range(0, EnemyObjs.Length)]);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.Init(life, radius ,dir ,pos);
            allAlivEnemies.Add(enemy);
        currentEnemyCount++;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        deathEnemyCount ++;
        allAlivEnemies.Remove(enemy);
        PlayerController.GetInstance.LevelData.LeftEnemyPercent = (allEnemyCount - deathEnemyCount)*1f/allEnemyCount;
        if (deathEnemyCount >= allEnemyCount)
        {
            //win
            GameController .GetInstance .OnLevelEnd(true);
        }
    }

    public void OnPlayerDeath()
    {
        isCreating = false;
    }

     public  void ClearEnemies()
    {
        for (int i = 0; i < allAlivEnemies.Count; i++)
        {
            ObjectPool.GetInstance().RecycleObj(allAlivEnemies[i].gameObject);
        }
        allAlivEnemies .Clear();
         isCreating = false;
    }
}
