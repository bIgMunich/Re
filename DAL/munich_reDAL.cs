using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Common;
using Base.BaseSql;
using System.Data;

namespace DAL
{
    public class munich_reDAL
    {

        public List<munich_re> GetList()
        {
            string sql = "select * from munich_re  order by Id desc";
            List<munich_re> list = new Helper().ExecuteList<munich_re>(sql, null);
            return list;
        }

        public ErrorResult Insert(munich_re entity)
        {
            ErrorResult result = new Helper().Insert(entity);
            return result;
        }

        public munich_re GetModel(int Id)
        {
            munich_re obj = new munich_re();
            obj.Id = Id;
            obj = new Helper().Find(obj);
            return obj;
        }

        public ErrorResult Update(munich_re entity)
        {
            ErrorResult result = new Helper().Update(entity);
            return result;
        }

        public ObjEntity GetPagerList(PagerInfo pageInfo, List<SqlParamList> list)
        {
            ObjEntity result = new Helper().ExecutePage(new munich_re(), pageInfo, list);
            return result;
        }

        public DataTable GetDataTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from munich_re where IsTrue=@IsTrue");
            List<SqlParam> list = new List<SqlParam>();
            list.Add(new SqlParam("IsTrue", 0));
            return new Helper().ExecuteDataTable(sb.ToString(), list);
        }

        public ObjEntity GetDataTablePager(PagerInfo pageInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from munich_re where IsTrue=@IsTrue");
            List<SqlParam> list = new List<SqlParam>();
            list.Add(new SqlParam("IsTrue", 0));
            return new Helper().ExecuteDataTablePagerInfo(sb.ToString(), list, pageInfo);
        }

    }
}
