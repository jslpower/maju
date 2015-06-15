using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.BLL
{
   public class BAttribute
    {
       /// <summary>
       /// 年龄泛型字典
       /// </summary>
       /// <returns></returns>
       public static Dictionary<int, string> GetAgeDict()
       {
           Dictionary<int, string> DictAge = new Dictionary<int, string>();
           DictAge.Add(0, "≤30岁");
           DictAge.Add(1, "31~50岁");
           DictAge.Add(2, "≥50岁");
           return DictAge;
       }

       /// <summary>
       /// 根据Key值返回value
       /// 
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
       public static string GetAgeValue(int key)
       {
           string retValue = "";
           var dict = GetAgeDict();
           if (dict.ContainsKey(key))
           {
               retValue = dict[key];
           }
           return retValue;
       }

       /// <summary>
       /// 年收入泛型字典
       /// </summary>
       /// <returns></returns>
       public static Dictionary<int, string> GetRevenueDict()
       {
           Dictionary<int, string> DictRevenue = new Dictionary<int, string>();
           DictRevenue.Add(0, "≤10万");
           DictRevenue.Add(1,"11~50万");
           DictRevenue.Add(2, "≥50万");
           return DictRevenue;
       }

       /// <summary>
       /// 根据Key值返回Value
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
       public static string GetRevenueValue(int key)
       {
           string retValue = "";
           var dict = GetRevenueDict();
           if (dict.ContainsKey(key))
           {
               retValue = dict[key];
           }
           return retValue;
       }
    }
}
