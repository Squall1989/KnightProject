using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightProject
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private float dificultyChangeTime;

        private int dificulty;
        private bool gameStarted;

        public Action<bool> OnGameStart;
        public Action<int> OnDifficultyChange;

        public bool GameStarted => gameStarted;

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

        // Start is called before the first frame update
        void Start()
        {
            OnGameStart?.Invoke(true);

            StartCoroutine(DifficultyCorout());
        }

        private IEnumerator DifficultyCorout()
        {
            while(true)
            {
                OnDifficultyChange?.Invoke(++dificulty);
                yield return new WaitForSeconds(dificultyChangeTime);
            }
        }
    }
}