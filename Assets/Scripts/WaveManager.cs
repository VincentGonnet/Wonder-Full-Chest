using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class WaveManager : MonoBehaviour
{
    private List<GameObject> obstacles = new();
    private int wave;
    private Random random;

    [SerializeField] private int obstaclesPerWave;
    // Used for the genetic thingy
    [SerializeField] private int populationSize;
    [SerializeField] private float mutationRate;

    private void Start()
    {
        this.SpawnWave();
    }

    private void FixedUpdate()
    {
        // Spawn wave if the last one is finished
        if (this.obstacles.Last().transform.position.x < -11) {
            this.SpawnWave();
        }
    }

    private void SpawnWave()
    {
        this.wave++;
        foreach(GameObject obstacle in obstacles)
        {
            Object.Destroy(obstacle);
        }
        this.obstacles.Clear();
        ObstacleMetadata[] obstacleMetadatas = this.GenerateObstacleOrder(this.wave + 5);
        for (int i = 0; i < this.obstaclesPerWave; i++) {
            obstacles.Add(obstacleMetadatas[i].Spawn(new Vector3(11 + i*3, 1) + this.transform.position, this.transform));
        }
    }

    private ObstacleMetadata[] GenerateObstacleOrder(int waveDifficulty)
    {
        ObstacleMetadata[] possibleObstacles = ObstacleMetadata.OBSTACLES.Where(obstacle => obstacle.minWaveSpawn <= this.wave).ToArray();
        // At the beginning we take the most difficult obstacle that can spawn
        int[][] population = Enumerable.Repeat(Enumerable.Repeat(possibleObstacles.Length - 1, this.obstaclesPerWave).ToArray(), this.populationSize).ToArray();
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
