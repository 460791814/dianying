using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianYing.Model;
using DianYing.DAL;
using System.Data;

namespace DianYing.BLL
{
   public  class B_Identify
    {
        D_Identify dal = new D_Identify();
        /// <summary>
        /// 是否喜欢
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool UpdateLike(E_Identify e)
        {
            return dal.UpdateLike(e);
        }
        /// <summary>
        /// 是否计划
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool UpdateRead(E_Identify e)
        {
            return dal.UpdateRead(e);
        }
              /// <summary>
       /// 插入一条数据
       /// 
       /// </summary>
       /// <param name="e"></param>
       /// <returns></returns>
        public bool Insert(E_Identify e)
        {
            return dal.Insert(e);
        }
              /// <summary>
       /// 是否存在
       /// </summary>
       /// <param name="e"></param>
       /// <returns></returns>
        public bool IsExist(E_Identify e)
        {
            return dal.IsExist(e);
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="e"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataSet GetList(E_Identify e, ref int total)
        {
            return dal.GetList(e, ref total);
        }
    }
}
