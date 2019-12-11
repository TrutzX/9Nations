namespace Improvements
{
    public class ImprovementHelper
    {
        public static int GetId(bool north, bool east, bool south, bool west)
        {
            
            //all
            if (north && east && south && west) return 10;
            
            //three
            if (north && east && south) return 9;
            if (north && east && west) return 13;
            if (north && south && west) return 12;
            if (east && south && west) return 8;
            
            //two rounds
            if (north && east) return 4;
            if (east && south) return 0;
            if (south && west) return 1;
            if (west && north) return 5;
            
            //two gerade
            if (north && south) return 15;
            if (east && west) return 11;

            //only one
            if (north) return 7;
            if (east) return 6;
            if (south) return 2;
            if (west) return 3;
            
            return 14;
        }
    }
}