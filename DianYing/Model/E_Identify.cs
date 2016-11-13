using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianYing.Model
{
   public  class E_Identify
    {
       public int Id
       {
           get;
           set;
       }
       public int UserId
       {
           get;
           set;
       }
       public int MovieId
       {
           get;
           set;
       }
       public bool IsLike
       {
           get;
           set;
       }
       public bool IsPlan
       {
           get;
           set;
       }
       public bool IsRead
       {
           get;
           set;
       }


       //新加
       public int CurrentPage
       {
           get;
           set;
       }
       public int PageSize
       {
           get;
           set;
       }
       public string StrWhere
       {
           get;
           set;
       }
       public string OrderBy
       {
           get;
           set;
       }

    }
}
