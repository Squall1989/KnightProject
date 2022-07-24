using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private float respawnTime;

        private Coroutine enemyCorout;
        private int currentDifficulty;
        private float heightInst;

        private void Awake()
        {
            GameManager.Instance.OnGameStart += StartGame;
            GameManager.Instance.OnDifficultyChange += UpdateDificulty;
            StartGame(GameManager.Instance.GameStarted);

            heightInst = Camera.main.WorldToViewportPoint(transform.position).y;
        }

        private void UpdateDificulty(int newDifficulty)
        {
            currentDifficulty = newDifficulty;
        }

        private void StartGame(bool start)
        {
            if(start)
            {
                enemyCorout = StartCoroutine(EnemyInstCorout());
            }
            else if(enemyCorout != null)
            {
                StopCoroutine(enemyCorout);
                enemyCorout = null;
            }
        }

        private IEnumerator EnemyInstCorout()
        {
            bool leftSide = true;
            while(true)
            {
                yield return new WaitForSeconds(respawnTime);
                leftSide ^= true;
                InstanceEnemy(leftSide);
            }
        }

        private void InstanceEnemy(bool leftSide)
        {
            float x = leftSide ? 0 : 1;

            Vector3 instPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, heightInst));

            Enemy enemy = EnemyPool.Instance.GetFromPool();
            enemy.gameObject.SetActive(true);
            enemy.transform.position = instPoint;
        }
    }
}