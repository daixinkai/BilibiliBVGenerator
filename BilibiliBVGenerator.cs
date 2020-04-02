using System;
using System.Collections.Generic;
using System.Text;

public static class BilibiliBVGenerator
{
    static readonly string s_table = "fZodR9XQDSUm21yCkr6zBqiveYah8bt4xsWpHnJE7jL5VG3guMTKNPAwcF";

    static readonly long s_xor = 177451812;
    static readonly long s_add = 8728348608;
    static readonly Dictionary<char, int> s_tr;

    static readonly int[] s_s = new[] { 11, 10, 3, 8, 4, 6 };

    static BilibiliBVGenerator()
    {
        s_tr = new Dictionary<char, int>();
        for (int i = 0; i < 58; i++)
        {
            s_tr[s_table[i]] = i;
        }
    }

    public static long Decode(string bv)
    {
        if (bv.Length != 12)
        {
            return 0;
        }
        long r = 0;
        for (int i = 0; i < 6; i++)
        {
            int c;
            if (!s_tr.TryGetValue(bv[s_s[i]], out c))
            {
                return 0;
            }
            r += c * (long)Math.Pow(58, i);
        }
        return (r - s_add) ^ s_xor;
    }

    public static string Encode(long av)
    {
        av = (av ^ s_xor) + s_add;
        var r = new List<char>("BV1  4 1 7  ");

        for (int i = 0; i < 6; i++)
        {
            r[s_s[i]] = s_table[(int)(av / (long)Math.Pow(58, i) % 58)];
        }
        return string.Join("", r);
    }

}
