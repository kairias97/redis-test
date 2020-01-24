using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisTest.Models
{
    [Serializable]
    public class Test
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MiniTest Testing { get; set; }
    }
    [Serializable]
    public class MiniTest
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }


}
