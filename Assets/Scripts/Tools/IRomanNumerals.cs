namespace Tools
{
    public interface IRomanNumerals
    {
        /// <summary>
        /// Convert an arabic number to a roman numeral
        /// </summary>
        /// <param name="arabic"></param>
        /// <returns></returns>
        string ArabicToRoman(int arabic);

        /// <summary>
        /// Convert a roman numeral to an arabic number
        /// </summary>
        /// <param name="roman"></param>
        /// <returns></returns>
        int RomanToArabic(string roman);
    }
}