using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZCS_Common
{
    public static class StringUtilites
    {
        /// <summary>
        /// 16位字符转List
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static ArrayList Str16ToArrayList(string strIn)//字符串拆分
        {
            string sParse = "";
            ArrayList myAL = new ArrayList();

            int i = 0;
            foreach (char cc in strIn)
            {
                i++;
                if (cc == ' ' || sParse.Length == 2)
                {
                    myAL.Add(sParse);
                    if (sParse.Length == 2 && cc != ' ')//两个字符
                    {
                        sParse = Convert.ToString(cc);
                    }
                    else
                    {
                        sParse = "";
                    }
                }
                else
                {
                    sParse += Convert.ToString(cc);
                    if (i == strIn.Length && cc != ' ')//末尾字符
                    {
                        myAL.Add(sParse);
                    }
                }
            }
            return myAL;
        }
    }
}
