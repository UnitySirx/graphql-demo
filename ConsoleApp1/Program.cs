// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

// Example ECEF coordinates (X, Y, Z) in meters
double X = 207511.474;
double Y = 199132.042;
double Z = 490.545;

var (latitude, longitude, height) = EcefToWgs84Converter.EcefToWgs84(X, Y, Z);


Console.WriteLine($"Latitude: {latitude}°");
Console.WriteLine($"Longitude: {longitude}°");
Console.WriteLine($"Height: {height} meters");


var data = EcefToWgs84Converter.CalculatePercentage(3.6f);
Console.WriteLine(data);

public class EcefToWgs84Converter
{
    // WGS84 ellipsoid constants
    const double a = 6378137.0; // Semi-major axis in meters
    const double e2 = 6.69437999014e-3; // Square of first eccentricity

    public static (double latitude, double longitude, double height) EcefToWgs84(double X, double Y, double Z)
    {
        // Calculate longitude
        double longitude = Math.Atan2(Y, X);

        // Calculate intermediate values
        double p = Math.Sqrt(X * X + Y * Y);
        double theta = Math.Atan2(Z * a, p * (1 - e2));

        // Calculate latitude using iteration
        double sinTheta = Math.Sin(theta);
        double cosTheta = Math.Cos(theta);
        double latitude = Math.Atan2(Z + e2 * (1 - e2) * a * sinTheta * sinTheta * sinTheta,
            p - e2 * a * cosTheta * cosTheta * cosTheta);

        // Calculate height
        double sinLatitude = Math.Sin(latitude);
        double N = a / Math.Sqrt(1 - e2 * sinLatitude * sinLatitude);
        double height = p / Math.Cos(latitude) - N;

        // Convert radians to degrees
        latitude = latitude * (180.0 / Math.PI);
        longitude = longitude * (180.0 / Math.PI);

        return (latitude, longitude, height);
    }

    // 设定的电压阈值，具体数值可以根据电池特性进行调整
    private static readonly int[] VoltageThresholds = { 3000, 3200, 3400, 3600, 3800, 4000, 4200 };

    public static int CalculatePercentage(float? fvoltage)
    {
        int voltage = Convert.ToInt32(fvoltage * 1000);
        if (voltage <= VoltageThresholds[0])
            return 0; // 0% 电量

        if (voltage >= VoltageThresholds[^1])
            return 100; // 100% 电量

        for (int i = 0; i < VoltageThresholds.Length; i++)
        {
            if (voltage < VoltageThresholds[i])
            {
                int previousVoltage = VoltageThresholds[i - 1];
                return i * 10 - (10 * (VoltageThresholds[i] - voltage)) / (VoltageThresholds[i] - previousVoltage);
            }
        }

        return 100; // 默认返回100%
    }
}