using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianYing.Model
{
   public  class E_Source
    {
        //Id, MovieId, MovieName, MovieURL, Episode, Site
       public int Id
       {
           get;
           set;
       }
       public int MovieId
       {
           get;
           set;

       }
       public string MovieName
       {
           get;
           set;
       }
       public string MovieURL
       {
           get;
           set;
       }
       public int Episode
       {
           get;
           set;
       }
       public string Site
       {
           get;
           set;
       }
    }
}
