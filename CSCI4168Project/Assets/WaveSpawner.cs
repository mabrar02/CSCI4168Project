using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{


    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float coolDownPhase = 5f;
    public float waveCountDown;

    private float searchCountdown = 1f;

    private void Start() {
        waveCountDown = coolDownPhase;
    }

    private void Update() {
        if(GameManager.Instance.State == GameState.BattlePhase) {
            if(!EnemyIsAlive()) {
                WaveCompleted();
            }
        }
        if(waveCountDown <= 0) {
            if(GameManager.Instance.State != GameState.SpawnPhase) {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else {
            waveCountDown -= Time.deltaTime;
        }
    }

    private void WaveCompleted() {
        if(nextWave + 1 > waves.Length -1) {
            nextWave = 0;
            GameManager.Instance.UpdateGameState(GameState.VictoryPhase);
        }
        else {
            nextWave++;
        }

        waveCountDown = coolDownPhase;
    }

    private bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f) {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        // spawning
        for(int i = 0; i< _wave.count; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        // waiting
        yield break;
    }

    private void SpawnEnemy(Transform _enemy) {
        Debug.Log("Spawning Enemy " +  _enemy.name);
    }

}