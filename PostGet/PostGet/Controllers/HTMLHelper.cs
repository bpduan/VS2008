using System;
using System.Text;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;


namespace MYHelper
{
    public class HTMLHelper
    {
        private static string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }
            HttpContext.Current.Session["code"] = checkCode.ToString();

            return checkCode;
        }
        public static void CreateCheckCodeImage()
        {
            string checkCode = GenerateCheckCode();
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 14.5)), 24);
            Graphics g = Graphics.FromImage(image);

            try
            {
                Random random = new Random();

                g.Clear(Color.Azure);

                Font[] fs = new Font[5];
                fs[0] = new Font("Arial", 14, (FontStyle.Bold | FontStyle.Italic));
                fs[1] = new Font("宋体", 14, (FontStyle.Bold));
                fs[2] = new Font("黑体", 14, (FontStyle.Italic));
                fs[3] = new Font("Times New Roman", 14, (FontStyle.Bold | FontStyle.Italic));
                fs[4] = new Font("serif", 14, (FontStyle.Bold));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Blue, 1.2f, true);
                for (int i = 0; checkCode.Length > i; i++)
                    g.DrawString(checkCode.Substring(i, 1), fs[random.Next(0, 4)], brush, image.Width * (i + 1) / 6 - random.Next(i, 7), random.Next(image.Height - 16));

                ////画曲线干扰线
                //Point[] ps = new Point[7];
                //ps[0] = new Point(1, random.Next(image.Height / 5 * 4) + image.Height / 10);
                //ps[1] = new Point(random.Next(image.Width / 6 - image.Width / 10, image.Width / 6 + image.Width / 10), random.Next(image.Height / 5 * 4) + image.Height / 10);
                //ps[2] = new Point(random.Next(image.Width / 3 - image.Width / 10, image.Width / 3 + image.Width / 10), random.Next(image.Height / 5 * 4) + image.Height / 10);
                //ps[3] = new Point(random.Next(image.Width / 2 - image.Width / 10, image.Width / 2 + image.Width / 10), random.Next(image.Height / 5 * 4) + image.Height / 10);
                //ps[4] = new Point(random.Next(image.Width * 2 / 3 - image.Width / 10, image.Width * 2 / 3 + image.Width / 10), random.Next(image.Height / 5 * 4) + image.Height / 10);
                //ps[5] = new Point(random.Next(image.Width * 5 / 6 - image.Width / 10, image.Width * 5 / 6 + image.Width / 10), random.Next(image.Height / 5 * 4) + image.Height / 10);
                //ps[6] = new Point(image.Width, random.Next(image.Height / 5 * 4) + image.Height / 10);

                //g.DrawBeziers(new Pen(Color.Blue, 2), ps);

                //画图片的边框线 
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        public static string fenyeSimpleStr(int Counts, int CurPage, int PageSize, string name, string url)
        {
            if (Counts <= 0)
                return "";
            if (url != "")
                url = "?" + url.Substring(1);
            StringBuilder tempfenye = new StringBuilder();
            int PageCount = (int)Math.Ceiling((double)Counts / PageSize);

            tempfenye.Append(

                "<script>function jump(){x=document.getElementById(\"CP\");if(x.value<1||x.value>" + PageCount + "){alert('请输入正确跳转页数');}else{ if(pageNoisNumber('CP')==true)  window.location.href=\"" + name + "/\"+x.value+\"" + url + "\";}}   " +
                "function pageNoisNumber(strId){ var strP = /^\\d+$/;  return strP.test($('#' + strId).val());  }"+
                "</script>"

                //alert(\"请输入正确的页码！\");
                );
            
            tempfenye.Append("共<font color=\"#f00\">" + Counts + "</font>条&nbsp;每页" + PageSize + "条&nbsp;当前页:<span class=\"red\">" + CurPage + "</span>/" + PageCount + "页");
            if (CurPage != 1)
            {
                tempfenye.Append("&nbsp;<a href=\"" + name + "/" + 1 + url + "\">首页</a> ");
                tempfenye.Append("&nbsp;<a href=\"" + name + "/" + (CurPage - 1) + url + "\">上一页</a>");
            }
            else
            {
                tempfenye.Append("&nbsp;首页");
                tempfenye.Append("&nbsp;上一页");
            }
            if (CurPage < PageCount)
            {
                tempfenye.Append("&nbsp;<a href=\"" + name + "/" + (CurPage + 1) + url + "\">下一页</a>");
                tempfenye.Append("&nbsp;<a href=\"" + name + "/" + PageCount + url + "\">末页</a>");
            }
            else
            {
                tempfenye.Append("&nbsp;下一页");
                tempfenye.Append("&nbsp;末页");
            }

            tempfenye.Append("<input onkeydown=\"if (event.keyCode == 13) {jump(); }\"  style=\"width:40px;border: none;text-align:center; border-bottom: 1px solid #000000;\"type=\"text\" id=\"CP\"/>&nbsp;页&nbsp;<input type=\"button\" class=\"button\" onclick=\"jump();\"  value=\"跳转\"/>");

            return tempfenye.ToString();
        }

        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            foreach (char c in str)
            {
                if (!Char.IsNumber(c) && c != '-' && c != '.')
                {
                    return false;
                }
            }
            return true;
        
        }

        public static bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string StrLength(string str, int length)
        {
            str = System.Web.HttpUtility.HtmlDecode(str.Replace("\r\n", ""));
            int len = 0, tagIndex = 0, strlength = str.Length;
            Regex objRegExp = new Regex("(<.+?>)", RegexOptions.IgnoreCase);
            MatchCollection matches = objRegExp.Matches(str);
            for (int i = 0; i < strlength; i++)
            {
                if (matches.Count > tagIndex && i == matches[tagIndex].Index)
                {
                    i += matches[tagIndex].Result("$1").Length - 1;
                    tagIndex++;
                }
                else
                {
                    if (System.Text.Encoding.Default.GetBytes(str.Substring(i, 1)).Length > 1)
                        len += 2;
                    else
                        len += 1;
                }
                if (len >= length)
                {
                    string other = "";
                    while (matches.Count > tagIndex)
                    {
                        other += matches[tagIndex].Value;
                        tagIndex++;
                    }
                    int otherlength = other.Length;
                    objRegExp = new Regex(@"<\s*\b(.+?)\b[^>]*?><\s*/\s*\b\1\b\s*>|<\s*\b(.+?)\b.*?/\s*>", RegexOptions.IgnoreCase);
                    while (objRegExp.Matches(other).Count > 0)
                    {
                        other = objRegExp.Replace(other, "");
                    }
                    other = new Regex(@"<\s*[^/]+?>", RegexOptions.IgnoreCase).Replace(other, "");
                    if (i + otherlength + 1 < strlength)
                        str = str.Substring(0, i) + other + "…";
                    else
                        str = str.Substring(0, i + 1) + other;
                    break;
                }
            }
            return str;
        }
        public static string RemoveHTML(string html)
        {
            Regex objRegExp = new Regex("<(?!p|/p|br).+?>", RegexOptions.IgnoreCase);
            return objRegExp.Replace(html, "");

        }
        public static string RemoveHTMLAll(string html)
        {

            Regex objRegExp = new Regex("<(.|\n)+?>");
            return objRegExp.Replace(html, "");

        }
        public static string RemoveHTMLExceptImg(string html)
        {
            Regex objRegExp = new Regex("<(?!Img).+?>", RegexOptions.IgnoreCase);
            return objRegExp.Replace(html, "\t");

        }

        public static string RemoveAllHTML(string Htmlstring)  
        {   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;

        }

        public static void WriteLog(string strLog)
        {
            string strPath = @"C:\test.txt";
            FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine(strLog);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
        }

        public static int getInfoType(int? fid, int? sid, int? tid, int? typeid, int? Department, int? Role)
        {
            int type = 0;
            if (typeid == 13&&(Department==5||Department==6||Department==7)&&Role==2)
            {
                type = 24;
            }
            else if (typeid == 13 && (Department == 5 || Department == 6 || Department == 7) && Role == 3)
            {
                type = 26;
            }
            else if (typeid == 13 && Department == 1 && Role == 3)
            {
                type = 28;
            }
            else if (typeid == 13 && Department == 2 && Role == 3)
            {
                type = 32;
            }
            else if (typeid == 13 && Department == 3 && Role == 3)
            {
                type = 37;
            }
            else if (typeid == 13 && (Department == 1 || Department == 2 || Department == 3) && Role == 2)
            {
                type = 1;
            }


            else if (typeid == 5 && Role == 2)
            {
                type = 2;
            }
            else if (typeid == 5 && Role == 3)
            {
                type = 29;
            }
            else if (typeid == 1 && fid == 5 && sid == 6 && Role == 2)
            {
                type = 3;
            }
            else if (typeid == 1 && fid == 5 && sid == 6 && Role == 3)
            {
                type = 30;
            }
            else if (typeid == 1 && fid == 9 && (sid == 19 || sid == 21))
            {
                type = 4;
            }
            else if (typeid == 1 && fid == 6 && sid == 10)
            {
                type = 6;
            }
            else if (typeid == 1 && fid == 6 && sid == 7)
            {
                type = 7;
            }
            else if (typeid == 1 && fid == 6 && sid == 11)
            {
                type = 8;
            }
            else if (typeid == 1 && fid == 7 && sid == 13)
            {
                type = 9;
            }
            else if (typeid == 2 && Role == 2)
            {
                type = 10;
            }
            else if (typeid == 2 && Role == 3)
            {
                type = 38;
            }
            else if (typeid == 15 && Role == 2)
            {
                type = 11;
            }
            else if (typeid == 15 && Role == 3)
            {
                type = 39;
            }
            else if (typeid == 9 && tid == 19)
            {
                type = 12;
            }
            else if (typeid == 9 && tid == 20)
            {
                type = 13;
            }
            else if (typeid == 11)
            {
                type = 14;
            }
            else if (typeid == 1 && fid == 7 && sid == 16)
            {
                type = 15;
            }
            else if (typeid == 1 && fid == 3 && sid == 30)
            {
                type = 16;
            }
            else if (typeid == 6 && tid == 1 && Role == 2)
            {
                type = 17;
            }
            else if (typeid == 6 && tid == 1 && Role == 3)
            {
                type = 34;
            }
            else if (typeid == 6 && tid == 2 && Role == 2)
            {
                type = 18;
            }
            else if (typeid == 6 && tid == 2 && Role == 3)
            {
                type = 35;
            }
            else if (typeid == 1 && fid == 6 && sid == 8)
            {
                type = 19;
            }
            else if (typeid == 1 && fid == 6 && sid == 9)
            {
                type = 20;
            }
            else if (typeid == 1 && fid == 3 && sid == 22)
            {
                type = 21;
            }
            else if (typeid == 1 && fid == 7 && sid == 14)
            {
                type = 22;
            }
            else if (typeid == 14 && Role == 2)
            {
                type = 23;
            }
            else if (typeid == 14 && Role == 3)
            {
                type = 33;
            }

            return type;
        }
    }
}
