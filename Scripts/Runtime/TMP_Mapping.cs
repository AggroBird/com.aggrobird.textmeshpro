using System;

namespace TMPro
{
    public static class TMP_Mapping
    {
        public delegate ReadOnlySpan<char> FormatMappingTagDelegate(ReadOnlySpan<char> name);
        public static event FormatMappingTagDelegate FormatMappingTagCallback;
        private static uint[] u32Buffer = new uint[128];
        internal static ReadOnlySpan<uint> InvokeFormatMappingTagCallback(ReadOnlySpan<char> name)
        {
            if (FormatMappingTagCallback != null)
            {
                var result = FormatMappingTagCallback(name);
                if (result.Length > u32Buffer.Length)
                {
                    u32Buffer = new uint[result.Length];
                }
                for (int i = 0; i < result.Length; i++)
                {
                    u32Buffer[i] = result[i];
                }
                return u32Buffer.AsSpan(0, result.Length);
            }
            return default;
        }
    }
}
