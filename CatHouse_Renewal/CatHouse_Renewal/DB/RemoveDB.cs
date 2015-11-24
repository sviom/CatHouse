using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.DB
{
    public class RemoveDB
    {
        // DBConnection 객체를 이용해 DB를 열고 닫는다.
        DBConnection db = new DBConnection();
        // DBConnection 객체에서 받아온 SqlConnection 객체를 받아옴.
        private static SqlConnection conn;

        // 회원 삭제 DB
        public bool MemberDrop(int memID)
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
                string query = "DELETE FROM dbo.Member WHERE memID=" + memID + ";";

                // SQL Command를 작성해서, 실행
                SqlCommand sqlQuery = new SqlCommand(query, conn);
                int queryResult = sqlQuery.ExecuteNonQuery();
                if (queryResult <= 0)
                {
                    throw new Exception();
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