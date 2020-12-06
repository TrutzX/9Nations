using System.Collections.Generic;
using Tools;

namespace UI
{
    public static class CircleGenerator
    {

        public static List<NVector> Gen(NVector pos, int radius)
        {
            List<NVector> list = new List<NVector>();
            
            Add(pos, list);
            if (radius <= 0) return list;
            
            AddRadial(pos, 1, list);
            if (radius <= 1) return list;
            
            AddCorner(pos, 1, list);
            if (radius <= 2) return list;
            
            AddRadial(pos, 2, list);
            if (radius <= 3) return list;
            
            AddSpringer(pos, 2, 1, list);
            if (radius <= 4) return list;
            
            AddRadial(pos, 3, list);
            if (radius <= 5) return list;
            
            AddCorner(pos, 2, list);
            if (radius <= 6) return list;
            
            AddSpringer(pos, 3, 1, list);
            if (radius <= 7) return list;
            
            AddRadial(pos, 4, list);
            if (radius <= 8) return list;
            
            AddSpringer(pos, 3, 2, list);
            if (radius <= 9) return list;
            
            AddSpringer(pos, 4, 1, list);
            if (radius <= 10) return list;
            
            AddCorner(pos, 3, list);
            if (radius <= 11) return list;
            
            AddSpringer(pos, 4, 2, list);
            if (radius <= 12) return list;
            
            AddRadial(pos, 5, list);
            if (radius <= 13) return list;
            
            AddSpringer(pos, 5, 1, list);
            if (radius <= 14) return list;
            
            AddSpringer(pos, 4, 3, list);
            if (radius <= 15) return list;
            
            AddCorner(pos, 4, list);
            if (radius <= 16) return list;
            
            AddSpringer(pos, 5, 2, list);
            if (radius <= 17) return list;
            
            AddSpringer(pos, 5, 3, list);
            if (radius <= 18) return list;
            
            AddRadial(pos, 6, list);
            if (radius <= 19) return list;

            //TODO to continue
            
            return list;
        }

        private static void AddRadial(NVector pos, int radius, List<NVector> list)
        {
            Add(pos.DiffX(-radius), list);
            Add(pos.DiffX(radius), list);
            Add(pos.DiffY(-radius), list);
            Add(pos.DiffY(radius), list);
        }

        private static void AddCorner(NVector pos, int radius, List<NVector> list)
        {
            Add(pos.Diff(-radius,-radius), list);
            Add(pos.Diff(radius,-radius), list);
            Add(pos.Diff(-radius,radius), list);
            Add(pos.Diff(radius,radius), list);
        }

        private static void AddSpringer(NVector pos, int radiusX, int radiusY, List<NVector> list)
        {
            Add(pos.Diff(-radiusX,-radiusY), list);
            Add(pos.Diff(radiusX,-radiusY), list);
            Add(pos.Diff(-radiusX,radiusY), list);
            Add(pos.Diff(radiusX,radiusY), list);
            Add(pos.Diff(-radiusY,-radiusX), list);
            Add(pos.Diff(radiusY,-radiusX), list);
            Add(pos.Diff(-radiusY,radiusX), list);
            Add(pos.Diff(radiusY,radiusX), list);
        }
        
        private static void Add(NVector pos, List<NVector> list)
        {
            if (!pos.Valid()) return;
            
            list.Add(pos);
        }
    }
}