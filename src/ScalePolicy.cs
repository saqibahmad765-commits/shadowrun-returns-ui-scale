namespace ShadowrunUIScale
{
    internal static class ScalePolicy
    {
        internal static float Clamp(float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value)) return 1f;
            return value < 1f ? 1f : (value > 1.5f ? 1.5f : value);
        }
    }
}
