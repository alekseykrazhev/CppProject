using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace Pyatnashki
{
    [Serializable]
    public class Result : IComparer<Result>
    {
        public string Name { get; set; }
        public string BeginTime { get; set; }
        public string BuildingTime { get; set; }
        public int MovesAmount { get; set; }

        public Result()
        {
        }

        public string ToString()
        {
            return Name + " " + BeginTime + " " + MovesAmount + " " + BuildingTime;
        }
        
        public int Compare(Result first, Result second)
        {
            if (first.MovesAmount < second.MovesAmount) return 1;
            else if (first.MovesAmount > second.MovesAmount) return -1;
            else return 0;
        }

        public static bool operator >(Result one, Result two)
        {
            string[] args1 = one.BuildingTime.Split(':');
            string[] args2 = two.BuildingTime.Split(':');
            if (Convert.ToInt32(args1[0]) > Convert.ToInt32(args2[0])) return true;
            else if (Convert.ToInt32(args1[1]) > Convert.ToInt32(args2[1])) return true;
            else if (Convert.ToInt32(args1[2]) > Convert.ToInt32(args2[2])) return true;
            return false;
        }
        
        public static bool operator <(Result one, Result two)
        {
            return !(one > two);
        }
    }
}