using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CCP.Repositori.Enums;

namespace CCP.Repositori.ResultData
{
    public static class MeasurementStandards
    {
        // Height Standards (cm)
        public static readonly Dictionary<int, float> MaleHeightStandard = new()
        {
            {12, 75.7f}, {24, 87.8f}, {36, 96.1f}, {48, 103.3f}, {60, 110.0f},
            {72, 116.0f}, {84, 121.7f}, {96, 127.0f}, {108, 132.2f}, {120, 137.5f}
        };

        public static readonly Dictionary<int, float> FemaleHeightStandard = new()
        {
            {12, 74.0f}, {24, 86.4f}, {36, 95.1f}, {48, 102.7f}, {60, 109.4f},
            {72, 115.1f}, {84, 120.8f}, {96, 126.2f}, {108, 131.5f}, {120, 137.0f}
        };

        // Weight Standards (kg)
        public static readonly Dictionary<int, float> MaleWeightStandard = new()
        {
            {12, 9.6f}, {24, 12.2f}, {36, 14.3f}, {48, 16.3f}, {60, 18.3f},
            {72, 20.5f}, {84, 22.9f}, {96, 25.3f}, {108, 28.1f}, {120, 31.4f}
        };

        public static readonly Dictionary<int, float> FemaleWeightStandard = new()
        {
            {12, 8.9f}, {24, 11.5f}, {36, 13.9f}, {48, 16.0f}, {60, 18.2f},
            {72, 20.5f}, {84, 23.0f}, {96, 25.8f}, {108, 28.8f}, {120, 32.0f}
        };

        // Head Circumference Standards (cm)
        public static readonly Dictionary<int, float> MaleHeadCircumferenceStandard = new()
        {
            {12, 46.5f}, {24, 48.5f}, {36, 49.5f}, {48, 50.0f}, {60, 50.5f},
            {72, 51.0f}, {84, 51.5f}, {96, 52.0f}, {108, 52.5f}, {120, 53.0f}
        };

        public static readonly Dictionary<int, float> FemaleHeadCircumferenceStandard = new()
        {
            {12, 45.5f}, {24, 47.5f}, {36, 48.5f}, {48, 49.0f}, {60, 49.5f},
            {72, 50.0f}, {84, 50.5f}, {96, 51.0f}, {108, 51.5f}, {120, 52.0f}
        };

        // BMI rating (common for both genders)
        public static BmiRating GetBmiRating(float bmi)
        {
            return bmi switch
            {
                < 18.5f => BmiRating.UnderWeight,
                >= 18.5f and < 24.9f => BmiRating.Normal,
                >= 25f and < 27f => BmiRating.OverWeight,
                >= 27f and < 30f => BmiRating.PreObesity,
                >= 30f and < 35f => BmiRating.ObesityI,
                >= 35f and < 40f => BmiRating.ObesityII,
                _ => BmiRating.ObesityIII
            };
        }
    }
}
