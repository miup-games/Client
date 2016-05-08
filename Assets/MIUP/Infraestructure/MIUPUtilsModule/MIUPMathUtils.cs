using System;

namespace MIUP.Infraestructure.MIUPUtilsModule
{
    public static class MIUPMathUtils
    {
        /// <summary>
        /// Gets the int division result.
        /// </summary>     
        public static int GetIntDivisionResult(float dividend, float divisor)
        {
            return (int)Math.Round(dividend / divisor);       
        }

       /// <summary>
       /// Gets the int division remainder.
       /// Usefull to check if the remainder is closer to 0 or 1
       /// </summary>
        public static float GetIntDivisionRemainder(float dividend, float divisor)
        {
            return (int)Math.Round(dividend % divisor);       
        }
    }
}
