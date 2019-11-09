/**
 * Represent a point on the path-finding grid.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/

namespace NesScripts.Controls.PathFind
{
    /// <summary>
    /// A 2d point on the grid
    /// </summary>
    public struct PPoint
    {
        // point X
        public int x;

        // point Y
        public int y;

        /// <summary>
        /// Init the point with values.
        /// </summary>
        public PPoint(int iX, int iY)
        {
            this.x = iX;
            this.y = iY;
        }

        /// <summary>
        /// Init the point with a single value.
        /// </summary>
        public PPoint(PPoint b)
        {
            x = b.x;
            y = b.y;
        }

        /// <summary>
        /// Get point hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return x ^ y;
        }

        /// <summary>
        /// Compare points.
        /// </summary>
        public override bool Equals(System.Object obj)
        {
            // check type
            if (!(obj.GetType() == typeof(PathFind.PPoint)))
                 return false;

            // check if other is null
            PPoint p = (PPoint)obj;
            if (ReferenceEquals(null, p))
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }

        /// <summary>
        /// Compare points.
        /// </summary>
        public bool Equals(PPoint p)
        {
            // check if other is null
            if (ReferenceEquals(null, p))
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }

        /// <summary>
        /// Check if points are equal in value.
        /// </summary>
        public static bool operator ==(PPoint a, PPoint b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if (ReferenceEquals(null, a))
            {
                return false;
            }
            if (ReferenceEquals(null, b))
            {
                return false;
            }
            // Return true if the fields match:
            return a.x == b.x && a.y == b.y;
        }

        /// <summary>
        /// Check if points are not equal in value.
        /// </summary>
        public static bool operator !=(PPoint a, PPoint b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Set point value.
        /// </summary>
        public PPoint Set(int iX, int iY)
        {
            this.x = iX;
            this.y = iY;
            return this;
        }
    }
}
