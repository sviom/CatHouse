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

        public int MemberLogin(string memEmail, string memPassword)
        {
            try
            {
                // 돌아오는 멤버 ID를 저장하기 위한 변수
                int memID = 0;
                // DB를 새로 연다.
                conn = db.DbOpen();
                //열려 있으면 사용한다.
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                string query = "SELECT memID, memEmail, memPassword FROM dbo.Member WHERE memEmail = '" + memEmail + "' AND memPassword = '" + memPassword + "';";

                // SQL Command를 작성해서, 실행
                SqlCommand sqlQuery = new SqlCommand(query, conn);
                SqlDataReader item = sqlQuery.ExecuteReader();
                // DB가 데이터를 가지고 있으면 관련된 자료 리턴
                if (item.HasRows == false)
                {
                    // 없으면 에러처리
                    throw new Exception();
                }
                // 데이터를 전부 읽어들이면서 ID를 읽어온다.
                while (item.Read())
                {
                    memID = Convert.ToInt32(item["memID"]);
                }
                db.DbClose();
                return memID;
            }
            catch (SqlException sqlEx)
            {
                sqlEx.Message.ToString();
                return -1;
            }
        }
    }
}