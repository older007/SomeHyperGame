using System;
using System.Collections.Generic;
using System.Linq;

public static class Utils
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        var rnd = new Random();
        
        return source.OrderBy<T, int>((item) => rnd.Next());
    }      
}