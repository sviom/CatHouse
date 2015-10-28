using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CatHouse_Renewal.DB
{
    public class DBConnection
    {
        // Connection 문장
        private static SqlConnection conn;

        // DB 열기
        public SqlConnection DbOpen(string ConnectionName = "CatHouseConnction")
        {
            // WebConfig 파일에 있는 CatHouseConnection 문장을 이용해 SqlConnection 객체를 만든다.
            conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["CatHouseConnction"].ToString());
            try
            {
                // 현재 연결되어 있지 않은 상황이면 연결
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }

                return conn;
            }
            catch (SqlException sqlEx)
            {
                sqlEx.Message.ToString();
                return null;
            }
        }

        // DB 닫기
        public bool DbClose()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (SqlException sqlEx)
            {
                sqlEx.Message.ToString();
                return false;
            }
        }
    }
}