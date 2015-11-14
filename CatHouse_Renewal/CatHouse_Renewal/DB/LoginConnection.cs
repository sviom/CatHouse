using CatHouse_Renewal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.DB
{
    public class LoginConnection
    {
        // DBConnection 객체를 이용해 DB를 열고 닫는다.
        DBConnection db = new DBConnection();
        // DBConnection 객체에서 받아온 SqlConnection 객체를 받아옴.
        private static SqlConnection conn;

        public DataSet MemberLogin(string memEmail, string memPassword)
        {
            try
            {
                // DB를 새로 연다.
                conn = db.DbOpen();
                //열려 있으면 사용한다.
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                string query = "SELECT memEmail, memPassword FROM dbo.Member WHERE memEmail = '" + memEmail + "' AND memPassword = '" + memPassword + "';";

                SqlDataAdapter ddd = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                ddd.Fill(ds, "dbo.Member");


                //SqlCommand sqlQuery = new SqlCommand(query, conn);
                //SqlDataReader item = sqlQuery.ExecuteReader();
                //while (item.Read())
                //{
                //    item["memEmail"].ToString();
                //    item["memPassword"].ToString();
                //}
                //// 실행
                //int rows = sqlQuery.ExecuteNonQuery();
                db.DbClose();
                return ds;
            }
            catch (SqlException sqlEx)
            {
                sqlEx.Message.ToString();
                return null;
            }
        }
    }
}