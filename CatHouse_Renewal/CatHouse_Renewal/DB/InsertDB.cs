using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CatHouse_Renewal.Models;

namespace CatHouse_Renewal.DB
{
    public class InsertDB
    {
        // DBConnection 객체를 이용해 DB를 열고 닫는다.
        DBConnection db = new DBConnection();
        // DBConnection 객체에서 받아온 SqlConnection 객체를 받아옴.
        private static SqlConnection conn;

        public bool MemberInsert(MemberModel memItem)
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
                else
                {
                    // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                    string query = "INSERT INTO dbo.Member (memName,memPassword,memAddress,memEmail,memPhone) VALUES (@memName,@memPassword,@memAddress,@memEmail,@memPhone)";
                    SqlCommand sqlQuery = new SqlCommand(query, conn);
                    sqlQuery.Parameters.AddWithValue("@memName", memItem.memName);
                    sqlQuery.Parameters.AddWithValue("@memPassword", memItem.memPassword);
                    sqlQuery.Parameters.AddWithValue("@memAddress", memItem.memAddress);
                    sqlQuery.Parameters.AddWithValue("@memEmail", memItem.memEmail);
                    sqlQuery.Parameters.AddWithValue("@memPhone", memItem.memPhone);
                    // 실행
                    int rows = sqlQuery.ExecuteNonQuery();
                }
                db.DbClose();
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