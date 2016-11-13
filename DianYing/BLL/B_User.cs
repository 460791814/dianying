using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianYing.Model;
using DianYing.DAL;

namespace DianYing.BLL
{
   public  class B_User
    {
       D_User dal = new D_User();
       /// <summary>
       /// 新建用户
       /// </summary>
       /// <param name="eUser"></param>
       /// <returns></returns>
        public int Insert(E_User eUser)
        {
            return dal.Insert(eUser);
        }
              /// <summary>
       /// 是否存在
       /// </summary>
       /// <param name="e"></param>
       /// <returns></returns>
        public int IsExist(E_User eUser)
        {
            return dal.IsExist(eUser);
        }
              /// <summary>
       /// 是否存在相同的用户名
       /// </summary>
       /// <param name="e"></param>
       /// <returns></returns>
        public bool IsExistName(string eUser)
        {
            return dal.IsExistName(eUser);
        }
              /// <summary>
       /// 增加一条数据
       /// </summary>
        public int Add(E_User model)
        {
            return dal.Add(model);
        }
              /// <summary>
       /// 更新一条数据
       /// </summary>
        public bool Update(E_User model)
        {
            return dal.Update(model);
        }
    }
}
