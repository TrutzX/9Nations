using System;
using System.Collections.Generic;
using Maps;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public class NVector
    {
        public int x;
        public int y;
        public int level;

        public NVector(){}
        
        public NVector(int x, int y, int level)
        {
            this.x = x;
            this.y = y;
            this.level = level;
        }
        
        public NVector(NVector pos)
        {
            this.x = pos.x;
            this.y = pos.y;
            this.level = pos.level;
        }

        public NVector(string pos)
        {
            string[] sett = SplitHelper.Seperator(pos);
            
            this.x = ConvertHelper.Int(sett[1]);
            this.y = ConvertHelper.Int(sett[2]);
            this.level = ConvertHelper.Int(sett[0]);
        }

        public bool Valid()
        {
            return XXGameMapMgmt.Valid(this);
        }

        public NVector Diff(int x, int y, int level=0)
        {
            return new NVector(this.x+x,this.y+y,this.level+level);
        }

        public NVector DiffX(int x)
        {
            return new NVector(this.x+x,y,level);
        }

        public NVector DiffY(int y)
        {
            return new NVector(x,this.y+y,level);
        }

        public NVector DiffLevel(int level)
        {
            return new NVector(x,y,this.level+level);
        }

        public override string ToString()
        {
            return $"{level}:{x},{y}";
        }

        public bool Equals(NVector other)
        {
            return other != null && x == other.x && y == other.y && level == other.level;
        }

        public NVector Clone()
        {
            return new NVector(this);
        }

        public override bool Equals(object obj)
        {
            return obj is NVector other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x;
                hashCode = (hashCode * 397) ^ y;
                hashCode = (hashCode * 397) ^ level;
                return hashCode;
            }
        }
    }
}