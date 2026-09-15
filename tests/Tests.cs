using System;
class Tests
{
    static void Main()
    {
        float[] inputs = { 1f, 1.15f, 1.5f, 0f, -5f, 4f, float.NaN, float.PositiveInfinity, float.NegativeInfinity };
        float[] expected = { 1f, 1.15f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 1f };
        for (int i = 0; i < inputs.Length; i++)
            if (ShadowrunUIScale.ScalePolicy.Clamp(inputs[i]) != expected[i]) throw new Exception("Scale test " + i);
        Console.WriteLine("PASS: 9 scale-boundary checks. In-game layout testing still required.");
    }
}
