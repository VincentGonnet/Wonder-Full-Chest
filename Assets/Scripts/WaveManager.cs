using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public List<GameObject> obstacles = new();
    private Random random = new();

    [SerializeField] private int obstaclesPerWave;
    // Used for the genetic thingy
    [SerializeField] private int populationSize;
    [SerializeField] private float mutationRate;

    private void Start()
    {
        this.SpawnWave();
    }

    public void SpawnWave()
    {
        this.gameManager.wave++;
        foreach(GameObject obstacle in obstacles)
        {
            Object.Destroy(obstacle);
        }
        this.obstacles.Clear();

        ObstacleMetadata[] obstacleMetadatas = this.GenerateObstacleOrder(this.gameManager.wave + 5);
        for (int i = 0; i < this.obstaclesPerWave; i++) {
            obstacles.Add(obstacleMetadatas[i].Spawn(new Vector3(11 + i*Constants.DISTANCE_BETWEEN_OBSTACLE, 0) + this.transform.position, this.transform));
        }
    }

    public void RemoveObstacle() {
        Destroy(obstacles[0]);
        obstacles.RemoveAt(0);
    }

    private ObstacleMetadata[] GenerateObstacleOrder(int waveDifficulty)
    {
        ObstacleMetadata[] possibleObstacles = ObstacleMetadata.OBSTACLES.Where(obstacle => obstacle.minWaveSpawn <= this.gameManager.wave).ToArray();
        int numberOfMaxDifficulty = 1;
        for (int i = possibleObstacles.Length - 2; i >= 0; i--) {
            if (possibleObstacles[i].difficulty == possibleObstacles.Last().difficulty) {
                numberOfMaxDifficulty++;
            }
        }
        // At the beginning we take the most difficult obstacle that can spawn
        int[][] population = new int[this.populationSize][];
        for (int i = 0; i < this.populationSize; i++) {
            population[i] = new int[this.obstaclesPerWave];
            for (int j = 0; j < this.obstaclesPerWave; j++) {
                population[i][j] = possibleObstacles.Length - 1 - this.random.Next(numberOfMaxDifficulty);
            }
        }
        int bestSolution = 0;
        int bestSolutionFitness;
        do {
            // Generate new population
            int[][] newPopulation = new int[this.populationSize][];
            newPopulation[0] = population[bestSolution];
            for (int i = 1; i < this.populationSize; i++) {
                int mergeWithId = random.Next(this.populationSize - 1);
                // We don't want the best solution to merge with itself
                if (mergeWithId >= bestSolution) {
                    mergeWithId++;
                }

                int[] newSolution = new int[this.obstaclesPerWave];
                for (int j = 0; j < this.obstaclesPerWave; j++) {
                    // Apply mutation
                    if (this.random.NextDouble() < this.mutationRate) {
                        newSolution[j] = this.random.Next(possibleObstacles.Length);
                        continue;
                    }

                    newSolution[j] = this.random.Next(2) == 0
                        ? population[bestSolution][j]
                        : population[mergeWithId][j];
                }

                newPopulation[i] = newSolution;
            }

            population = newPopulation;
            // Calculate best solution
            bestSolution = 0;
            bestSolutionFitness = Int32.MinValue;
            for (int i = 0; i < this.populationSize; i++) {
                int solutionDifficulty = 0;
                for (int j = 0; j < this.obstaclesPerWave; j++) {
                    solutionDifficulty += possibleObstacles[population[i][j]].difficulty;
                }

                int fitness = solutionDifficulty > waveDifficulty ? -solutionDifficulty : solutionDifficulty;
                if (fitness > bestSolutionFitness) {
                    bestSolution = i;
                    bestSolutionFitness = fitness;
                }
            }
        } while (bestSolutionFitness < 0);

        return population[bestSolution].Select(obstacle => possibleObstacles[obstacle]).ToArray();
    }
}
