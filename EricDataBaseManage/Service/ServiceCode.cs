using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TwjDataBaseManage.DataAccess;

namespace TwjDataBaseManage.Service
{
    public class ServiceCode
    {
        public static object ReadTableInfo(string d, string t)
        {
            DataTable tbInfo = DataAccessCode.ReadTableInfo(d, t);
            return new
            {
                total = 0,
                rows = from p in tbInfo.AsEnumerable()
                       select new
                       {
                           Tindex = p["Tindex"],
                           Tcname = p["Tcname"],
                           Tres = p["Tres"],
                           Ttype = p["Ttype"],
                           Tprec = p["Tprec"],
                           Tweishu = p["Tweishu"],
                           IsKey = p["IsKey"].ToString() == "1" ? "<img src=\"/Content/images/pk.gif\">" : "",
                           IsCanNull = p["IsCanNull"].ToString() == "1" ? "Yes" : "No",
                           Tdefault = p["Tdefault"]
                       }
            };
        }

        public static object GetTableList()
        {
            var databaseList = DataAccessCode.GetDbList();
            return from p in databaseList.AsEnumerable()
                   select new
                   {
                       id = 0,
                       text = p["name"].ToString(),
                       state = "closed",
                       iconCls = "icon-database",
                       children = from p2 in DataAccessCode.GetTableList(p["name"].ToString()).AsEnumerable()
                                  select new
                                  {
                                      id = 0,
                                      text = p2["name"].ToString(),
                                      iconCls = "icon-table",
                                      attributes = new { url = "d=" + p["name"].ToString() + "&t=" + p2["name"].ToString() }
                                  }


                   };
        }
    }
}