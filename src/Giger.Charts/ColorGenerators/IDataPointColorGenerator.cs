namespace Giger.Charts.ColorGenerators
{
    public interface IDataPointColorGenerator
    {
        string GenerateColor(int group, int stack, int point, double value);
    }
}