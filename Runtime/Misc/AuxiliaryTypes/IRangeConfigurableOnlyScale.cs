namespace ColdWind.Core
{
    public interface IRangeConfigurableOnlyScale : IReadOnlyScale
    {
        public float Max { get; set; }

        public float Min { get; set; }
    }
}
