using System;


namespace Tools
{
    public static class TextHelper {

        /**
         * Capitalize a string
         * 
         * @param name
         * @return
         */
        public static string Cap(string name) {
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        public static string CommaSep(string[] l)
        {
            return String.Join(", ", l);
        }

        public static string Plus(int num) {
            return num > 0 ? "+" + num : num.ToString();
        }

        public static string RichText(params string[] d)
        {
            return String.Join(";;", d);
        }

        public static string Proc(int act, int max)
        {
            return Math.Round(act * 100d / max) + "%";
        }

        public static string Header(string h)
        {
            return $"@H@{h}";
        }

        public static string IconLabel(string icon, string text)
        {
            return $"@IL@{icon}@{text}";
        }
    }
}
