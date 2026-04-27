using System.Security.Cryptography;
using SlotEngine.Application.Abstractions;

namespace SlotEngine.Infrastructure.Random;

public sealed class CryptoRng : IRng
{
    public int Next(int maxExclusive)
    {
        if (maxExclusive <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxExclusive), "Upper bound must be positive.");
        return RandomNumberGenerator.GetInt32(maxExclusive);
    }
}
