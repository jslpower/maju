using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.MAJU.Model
{
   /// <summary>
   /// 广告查询实体
   /// </summary>
   public class AdvSearch
    {
       /// <summary>
       /// 广告标题
       /// </summary>
       public string Title { get; set; }
       /// <summary>
       /// 图片位置
       /// </summary>
       public Model.EnumType.导航条位置? Type { get; set; }
    }
}
