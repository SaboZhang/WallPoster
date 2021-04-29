using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace WallPoster.Helper.Video
{
    public static class CleanStringParser
    {
        public static bool TryClean([NotNullWhen(true)] string name, IReadOnlyList<Regex> expression, out ReadOnlySpan<char> newName)
        {
            if (string.IsNullOrEmpty(name))
            {
                newName = ReadOnlySpan<char>.Empty;
                return false;
            }

            var len = expression.Count;
            for (int i = 0; i < len; i++)
            {
                if (TryClean(name, expression[i], out newName))
                {
                    return true;
                }
            }

            newName = ReadOnlySpan<char>.Empty;
            return false;
        }

        private static bool TryClean(string name, Regex expression, out ReadOnlySpan<char> newName)
        {
            var match = expression.Match(name);
            var index = match.Index;
            if (match.Success && index != 0)
            {
                newName = name.AsSpan().Slice(0, match.Index);
                return true;
            }

            newName = ReadOnlySpan<char>.Empty;
            return false;
        }
    }
}
