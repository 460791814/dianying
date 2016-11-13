using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianYing.Model;
using DianYing.DAL;

namespace DianYing.BLL
{
   public  class T_Channel
    {

       D_Channel dal = new D_Channel();
                /// <summary>
        /// 根据表名获取对应的数据
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
       public List<E_Channel> SelectChannelList()
       {
          return dal.SelectChannelList();
       }

       /// <summary>
       /// 增加一条数据
       /// </summary>
       public int Add(E_Channel model)
       {
           return dal.Add(model);
       }

       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool Update(E_Channel model)
       {
           return dal.Update(model);
       }

       /// <summary>
       /// 删除一条数据
       /// </summary>
       public bool Delete(int ChannelID)
       {

           return dal.Delete(ChannelID);
       }
    }
}
