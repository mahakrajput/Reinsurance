using System;
namespace Reinsurance.FileReader
{
    /// <summary>
    /// Conceptually this can be considered as data retrieved from an a database, filesystem or API
    /// </summary>
    public class Data
    {
        public static int[][] Events => new[]
        {
           new []{3, 3, 3, 750},
           new []{2, 3, 2, 500},
           new []{1, 2, 1, 1000},
           new []{4, 1, 3, 2000}
       };
    }

}
