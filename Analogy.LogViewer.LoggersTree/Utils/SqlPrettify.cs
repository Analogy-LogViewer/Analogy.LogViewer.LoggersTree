//Adapted from SqlPrettify, Version=1.0.3.0, MIT license

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Analogy.LogViewer.LoggersTree.Utils
{
    public class SqlPrettify
    {
        public static string Pretty(string q)
        {
            q += " ";
            int num1 = 0;
            string str1 = string.Empty;
            int num2 = 0;
            List<string> stringList1 = new List<string>()
                                       {
                                           "select",
                                           "from",
                                           "where",
                                           "inner",
                                           "outer",
                                           "left",
                                           "right",
                                           "full",
                                           "join",
                                           "group",
                                           "order",
                                           "by"
                                       };
            List<string> stringList2 = new List<string>()
                                       {
                                           "and",
                                           "or",
                                           "between"
                                       };
            List<string> stringList3 = new List<string>()
                                       {
                                           "left",
                                           "right",
                                           "inner",
                                           "outer",
                                           "full"
                                       };
            List<string> stringList4 = new List<string>()
                                       {
                                           "order",
                                           "group"
                                       };
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            for (int index = 0; index < q.Length; ++index)
            {
                char ch = q[index];
                str4 += ch.ToString();
                if (ch == '\'' && q[index - 1] != '\\')
                    num2 = (num2 + 1) % 2;
                if (num2 == 0 && ch == ',')
                    str4 += " ";
            }
            q = str4;
            bool isText = false;
            for (int index = 0; index < q.Length; index++)
            {
                char ch = q[index];
                bool isSpace = ch == '\n' || ch == '\r' || ch == ' ' || ch == '\t';
                if (ch == '\'' && q[index - 1] != '\\')
                    isText = !isText;
                if (!isSpace || isText)
                {
                    str3 += ch.ToString();
                    if (ch == '(')
                        ++num1;
                    if (ch == ')')
                        --num1;
                }
                else if (str3 != string.Empty)
                {
                    if (stringList1.Contains(str3.Trim().ToLower()))
                    {
                        if (str3.Trim().ToLower() == "join")
                        {
                            if (stringList3.Contains(str1))
                                str2 = str2.Substring(0, str2.Length - 1 - (num1 + 1) * 4) + " " + " " + str3 + Environment.NewLine + SpaceAdder((num1 + 1) * 4);
                            else
                                str2 = str2 + Environment.NewLine + SpaceAdder(num1 * 4) + str3 + Environment.NewLine + SpaceAdder((num1 + 1) * 4);
                        }
                        else if (str3.Trim().ToLower() == "by")
                        {
                            if (stringList4.Contains(str1))
                                str2 = str2.Substring(0, str2.Length - 1 - (num1 + 1) * 4) + " " + " " + str3 + Environment.NewLine + SpaceAdder((num1 + 1) * 4);
                            else
                                str2 = str2 + str3 + Environment.NewLine + SpaceAdder((num1 + 1) * 4);
                        }
                        else
                            str2 = str2 + Environment.NewLine + SpaceAdder(num1 * 4) + str3 + Environment.NewLine + SpaceAdder((num1 + 1) * 4);
                    }
                    else if (stringList2.Contains(str3.Trim().ToLower()))
                    {
                        str2 = str2 + Environment.NewLine + SpaceAdder((num1 + 1) * 4) + str3 + " ";
                    }
                    else
                    {
                        string str5 = str3;
                        str2 = str5[^1] != ',' ? str2 + str3 + " " : str2 + str3 + Environment.NewLine + SpaceAdder((num1 + 1) * 4);
                    }
                    str1 = str3.ToLower();
                    str3 = string.Empty;
                }
            }
            if (str2[0] != '\n')
                return str2;
            string str6 = str2;
            int length1 = str6.Length;
            int startIndex = 1;
            int num4 = startIndex;
            int length2 = length1 - num4;
            return str6.Substring(startIndex, length2);
        }

        private static string SpaceAdder(int spaces)
        {
            string str = string.Empty;
            for (; spaces > 0; --spaces)
                str += " ";
            return str;
        }
    }
}