using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class EnemyPool : MonoBehaviour, IPoolable<Enemy>
    {
        public static EnemyPool Instance;

        [SerializeField]
        private Enemy enemyPfb;

        private Stack<Enemy> enemieStack = new Stack<Enemy>();

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else if(Instance == this)
            {
                Destroy(gameObject);
            }
        }

        public Enemy GetFromPool()
        {
            if(enemieStack.Count == 0)
            {
                ReturnToPool(Instantiate(enemyPfb));
            }

            return enemieStack.Pop();
        }

        public void ReturnToPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            enemieStack.Push(enemy);
        }

    }
}