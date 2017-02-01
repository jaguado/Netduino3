namespace JAM.Netduino3.App.Helpers
{
   public static class StringHelper
    {
        public static bool IsNullOrEmpty(string pInput)
        {
            // ReSharper disable once ReplaceWithStringIsNullOrEmpty
            return (pInput == null || pInput == string.Empty);
        }
        public static string Replace(string pInput, string pPattern, string pReplacement)
        {
            if (IsNullOrEmpty(pInput) || IsNullOrEmpty(pPattern))
            {
                return pInput;
            }
            var retval = "";
            int startIndex = 0;

            int matchIndex = -1;
            while ((matchIndex = pInput.IndexOf(pPattern, startIndex)) >= 0)
            {
                if (matchIndex > startIndex)
                {
                    retval += pInput.Substring(startIndex, matchIndex - startIndex);
                }
                retval += pReplacement;
                matchIndex += pPattern.Length;
                startIndex = matchIndex;
            }
            if (startIndex < pInput.Length)
            {
                retval += pInput.Substring(startIndex);
            }
            return retval;
        }

    }
}
