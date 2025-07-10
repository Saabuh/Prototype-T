using System;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

namespace Prototype_S
{
    public static class Noise
    {

        public static float[,] GenerateNoiseMap(int width, int height, float scale, int octaves = 1, float exponent = 1, int seed = 0, bool lerp = false)
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
                    
                    //normalize x/y, between -1/+1
                    float nx = (2f * x) / width - 1;
                    float ny = (2f * y) / height - 1;
                    
                    //for each octave, increase frequency and lower amplitude
                    for (int i = 0; i < octaves; i++)
                    {
                        
                        //add a new offset to each octave to sample from different areas of noise space
                        float sampleX = (nx / scale * frequency) + octaveOffsets[i];
                        float sampleY = (ny / scale * frequency) + octaveOffsets[i];

                        float perlinValue =  amplitude * (Mathf.PerlinNoise(sampleX, sampleY));
                        
                        //final noise value
                        noiseHeight += perlinValue;

                        amplitudeSum += amplitude;
                        amplitude = (amplitude / 2);
                        frequency *= 2;
                    }
                    
                    
                    //add the amplitude sum to keep noiseHeight clamped to 0-1
                    noiseHeight = (noiseHeight / amplitudeSum);
                    
                    //raise value to an exponent for higher peaks/flatter valleys
                    // noiseMap[x, y] = (float)Math.Pow(noiseHeight, exponent);
                    noiseHeight = (float)Math.Pow(noiseHeight, exponent);

                    if (lerp)
                    {
                        //calculate distance from origin point
                        double d = 1 - (1 - Math.Pow(nx, 2)) * (1 - Math.Pow(ny, 2));
                    
                        //apply linear interpolation
                        noiseMap[x, y] = Mathf.Lerp(noiseHeight, (float)(1 - d), (float)0.5);
                    }
                    else
                    {
                        noiseMap[x, y] = noiseHeight;
                    }
                    
                }
            }

            return noiseMap;

        }
        
    }
}
