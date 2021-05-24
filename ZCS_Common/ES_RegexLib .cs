using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ZCS_Common
{
    public class ES_RegexLib
    {
        public ES_RegexLib()
        {

        }

        //验证Email地址 
        public bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format. 
            return Regex.IsMatch(strIn, "^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
        }

        //dd-mm-yy 的日期形式代替 mm/dd/yy 的日期形式。 
        public string MDYToDMY(String input)
        {
            return Regex.Replace(input, "//b(?//d{1,2})/(?//d{1,2})/(?//d{2,4})//b", "${day}-${month}-${year}");
        }

        //验证是否为小数 
        public bool IsValidDecimal(string strIn)
        {
            return Regex.IsMatch(strIn, "^[0]{0,1}[.]{1}[0-9]{1,}$");
        }
        //验证两位小数
        public bool Is2Decimal(string str_decimal)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_decimal, @"^[0-9]*[.]{1}[0-9]{2}$");
        }
        //验证一年的12个月
        public bool IsMonth(string str_Month)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Month, @"^(0?[[1-9]|1[0-2])$");
        }

        //验证一个月的31天
        public bool IsDay(string str_day)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_day, @"^((0?[1-9])|((1|2)[0-9])|30|31)$");
        }

        //验证是否为电话号码 
        public bool IsValidTel(string strIn)
        {
            return Regex.IsMatch(strIn, @"(/d+-)?(/d{4}-?/d{7}|/d{3}-?/d{8}|^/d{7,8})(-/d+)?");
        }

        //验证年月日 
        public bool IsValidDate(string strIn)
        {
            return Regex.IsMatch(strIn, @"^2/d{3}-(?:0?[1-9]|1[0-2])-(?:0?[1-9]|[1-2]/d|3[0-1])(?:0?[1-9]|1/d|2[0-3]):(?:0?[1-9]|[1-5]/d):(?:0?[1-9]|[1-5]/d)$");
        }

        //验证后缀名 
        public bool IsValidPostfix(string strIn)
        {
            return Regex.IsMatch(strIn, @"/.(?i:gif|jpg)$");
        }

        //验证字符是否在4至12之间 
        public bool IsValidByte(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[a-z]{4,12}$");
        }

        //验证IP 
        public bool IsValidIp(string strIn)
        {
            return Regex.IsMatch(strIn, @"^(((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))$");
               // @"^(/d{1,2}|1/d/d|2[0-4]/d|25[0-5])/.(/d{1,2}|1/d/d|2[0-4]/d|25[0-5])/.(/d{1,2}|1/d/d|2[0-4]/d|25[0-5])/.(/d{1,2}|1/d/d|2[0-4]/d|25[0-5])$"
        }
        // 验证输入汉字  
        public bool IsChinese(string str_chinese)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_chinese, @"^[/u4e00-/u9fa5],{0,}$");
        }

        //验证输入字符串 (至少8个字符)
        public bool IsLength(string str_Length)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Length, "^.{8,}$");
        }

        //验证数字输入
        public bool IsNumber(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, "^[0-9]+$");
        }

        //验证整数
        public bool IsInteger(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, "^[-,+]?[0-9]+$");
        }

        //验证手机
        public bool IsCellphoneNum(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, "^[1]{1}[0-9]{10}$");
        }
        //  验证密码长度 (6-18位)
        public bool IsPasswLength(string str_Length)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Length, "^/d{6,18}$");
        }

    }
}
