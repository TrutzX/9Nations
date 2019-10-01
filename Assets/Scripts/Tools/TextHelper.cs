using System;


/**
 * @author sven
 *
 */
namespace Tools
{
    public class TextHelper {

        private TextHelper() {}

        /**
     * Capitalize a string
     * 
     * @param name
     * @return
     */
        public static string cap(string name) {
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        /**
     * Get the time stamp
     * 
     * @param l
     * @return
     */
        public static string CommaSep(params string[] l) {
            return CommaSepA(l);
        }

        /**
     * Get the time stamp
     * 
     * @param l
     * @return
     */
        public static string CommaSepA(string[] l) {
            string erg = "";
            foreach (string s in l) {
                if (String.IsNullOrEmpty(s)) {
                    continue;
                }

                if (erg.Length > 0) {
                    erg += ", ";
                }
                erg += s;
            }

            return erg;
        }

        public static string plus(int num) {
            return num > 0 ? "+" + num : num.ToString();
        }
    }
}
