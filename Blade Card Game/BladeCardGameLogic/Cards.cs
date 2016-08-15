    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeCardGameLogic
{
    public enum Face
    {
        one,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        mirror,
        bolt,
    }
    public class Cards
    {
        public Face Face { get; set; }
        public int Value { get; set; }
    }
}
