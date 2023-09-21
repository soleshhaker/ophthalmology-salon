using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class RandomDBName
    {
        public static string GetRandomName()
        {
            // Generate a random suffix for the database name using a Guid
            return "TestDb_" + Guid.NewGuid().ToString();
        }
    }
}
