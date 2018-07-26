using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TwjDataBaseManage.Common;

namespace TwjDataBaseManage.DataAccess
{
    public class DataAccessCode
    {
        const string selectTables = "select name from {0}.sys.tables";
        const string selectTableInfo = @"SELECT  obj.name AS Tname,
        col.colorder AS Tindex,
        col.name AS Tcname,
        ISNULL(ep.[value], '') AS Tres,
        t.name AS Ttype,
        col.prec AS Tprec,
        ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS Tweishu,
        CASE WHEN EXISTS ( SELECT   1
                           FROM     {0}.dbo.sysindexes si
                                    INNER JOIN {0}.dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid
                                    INNER JOIN {0}.dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid
                                    INNER JOIN {0}.dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
                           WHERE    sc.id = col.id AND sc.colid = col.colid ) THEN 1
             ELSE 0
        END AS IsKey,
       col.isnullable AS IsCanNull,
        ISNULL(comm.text, '') AS Tdefault
FROM    {0}.dbo.syscolumns col
        LEFT OUTER JOIN {0}.dbo.systypes t ON col.xtype = t.xusertype
        INNER JOIN {0}.dbo.sysobjects obj ON col.id = obj.id AND obj.xtype = 'U' AND obj.status >= 0
        LEFT OUTER JOIN {0}.dbo.syscomments comm ON col.cdefault = comm.id
        LEFT OUTER JOIN {0}.sys.extended_properties ep ON col.id = ep.major_id AND col.colid = ep.minor_id AND ep.name = 'MS_Description'
        LEFT OUTER JOIN {0}.sys.extended_properties epTwo ON obj.id = epTwo.major_id AND epTwo.minor_id = 0 AND epTwo.name = 'MS_Description'
WHERE   obj.name = '{1}'
ORDER BY col.colorder";

        public static DataTable GetTableList(string dataBase)
        {
            string strSql = string.Format(selectTables, dataBase);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        public static DataTable ReadTableInfo(string d, string t)
        {
            string strSql = string.Format(selectTableInfo, d, t);
            return SqlHelper.ExecuteDataTable(strSql);
        }

        public static DataTable GetDbList()
        {
            return SqlHelper.ExecuteDataTable("select name from sys.databases WHERE database_id>5 ");
        }
    }
}