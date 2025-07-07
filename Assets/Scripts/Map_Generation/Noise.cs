using System;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

namespace Prototype_S
{
    public static class Noise
    {

        public static float[,] GenerateNoiseMap(int width, int height, float scale, int octaves = 1, float exponent = 1, int seed = 0)
        {
            float[,] noiseMap = new float[width, height];
            int[] octaveOffsets = new int[octaves];
            
            //pseudo-random, generate random number sequence #(seed)
            Random random = new Random(seed);

            //generate offsets for each octave, changing the range will provide different results, regardless of same seed
            for (int i = 0; i < octaves; i++)
            {
                octaveOffsets[i] = random.Next(0, 100000);
            }

            if (scale <= 0)
            {
                scale = 0.0001f;
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    float amplitude = 1;
                    float amplitudeSum = 0;
                    float frequency = 1;
                    float noiseHeight = 0;
                    
                    //for each octave, increase frequency and lower amplitude
                    for (int i = 0; i < octaves; i++)
                    {
                        //add a new offset to each octave to sample from different areas of noise space
                        
                        float sampleX = x / scale * frequency + octaveOffsets[i];
                        float sampleY = y / scale * frequency + octaveOffsets[i];

                        float perlinValue =  amplitude * (Mathf.PerlinNoise(sampleX, sampleY));
                        noiseHeight += perlinValue;

                        amplitudeSum += amplitude;
                        
                        amplitude = (amplitude / 2);
                        frequency *= 2;
                    }
                    
                    //add the amplitude sum to keep noise clamped to 0-1
                    noiseHeight = (noiseHeight / amplitudeSum);
                    
                    //raise value to an exponent for higher peaks/flatter valleys
                    noiseMap[x, y] = (float)Math.Pow(noiseHeight, exponent);
                }
            }

            return noiseMap;

        }
        
    }
}
