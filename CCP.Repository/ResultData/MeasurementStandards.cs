using CCP.Repositori.Enums;

namespace CCP.Repositori.ResultData
{
    public static class MeasurementStandards
    {
        public static readonly Dictionary<int, float> MaleHeightStandard = new()
        {
            {1, 75.7f}, {2, 87.8f}, {3, 96.1f}, {4, 103.3f}, {5, 110.0f},
            {6, 116.0f}, {7, 121.7f}, {8, 127.0f}, {9, 132.2f}, {10, 137.5f},
            {11, 143.0f}, {12, 149.0f}, {13, 156.0f}, {14, 163.0f}, {15, 168.0f},
            {16, 172.0f}, {17, 175.0f}, {18, 176.5f}, {19, 177.0f}, {20, 177.5f}
        };

        public static readonly Dictionary<int, float> FemaleHeightStandard = new()
        {
            {1, 74.0f}, {2, 86.4f}, {3, 95.1f}, {4, 102.7f}, {5, 109.4f},
            {6, 115.1f}, {7, 120.8f}, {8, 126.2f}, {9, 131.5f}, {10, 137.0f},
            {11, 144.0f}, {12, 150.0f}, {13, 155.5f}, {14, 160.0f}, {15, 162.5f},
            {16, 164.0f}, {17, 165.0f}, {18, 165.5f}, {19, 166.0f}, {20, 166.2f}
        };

        public static readonly Dictionary<int, float> MaleWeightStandard = new()
        {
            {1, 9.6f}, {2, 12.2f}, {3, 14.3f}, {4, 16.3f}, {5, 18.3f},
            {6, 20.5f}, {7, 22.9f}, {8, 25.3f}, {9, 28.1f}, {10, 31.4f},
            {11, 35.0f}, {12, 40.0f}, {13, 45.0f}, {14, 50.0f}, {15, 57.0f},
            {16, 62.0f}, {17, 66.0f}, {18, 70.0f}, {19, 72.0f}, {20, 74.0f}
        };

        public static readonly Dictionary<int, float> FemaleWeightStandard = new()
        {
            {1, 8.9f}, {2, 11.5f}, {3, 13.9f}, {4, 16.0f}, {5, 18.2f},
            {6, 20.5f}, {7, 23.0f}, {8, 25.8f}, {9, 28.8f}, {10, 32.0f},
            {11, 36.0f}, {12, 41.0f}, {13, 46.0f}, {14, 51.0f}, {15, 55.0f},
            {16, 58.0f}, {17, 60.0f}, {18, 61.5f}, {19, 62.0f}, {20, 62.5f}
        };

        public static readonly Dictionary<int, float> MaleHeadCircumferenceStandard = new()
        {
            {1, 46.5f}, {2, 48.5f}, {3, 49.5f}, {4, 50.0f}, {5, 50.5f},
            {6, 51.0f}, {7, 51.5f}, {8, 52.0f}, {9, 52.5f}, {10, 53.0f},
            {11, 53.2f}, {12, 53.4f}, {13, 53.5f}, {14, 53.6f}, {15, 53.7f},
            {16, 53.8f}, {17, 53.9f}, {18, 54.0f}, {19, 54.0f}, {20, 54.0f}
        };

        public static readonly Dictionary<int, float> FemaleHeadCircumferenceStandard = new()
        {
            {1, 45.5f}, {2, 47.5f}, {3, 48.5f}, {4, 49.0f}, {5, 49.5f},
            {6, 50.0f}, {7, 50.5f}, {8, 51.0f}, {9, 51.5f}, {10, 52.0f},
            {11, 52.2f}, {12, 52.4f}, {13, 52.5f}, {14, 52.6f}, {15, 52.7f},
            {16, 52.8f}, {17, 52.9f}, {18, 53.0f}, {19, 53.0f}, {20, 53.0f}
        };

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