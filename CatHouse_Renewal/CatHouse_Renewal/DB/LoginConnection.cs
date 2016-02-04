using CatHouse_Renewal.Models;
using Newtonsoft.Json.Linq;
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

        // 회원 로그인
        public SessionData MemberLogin(string memEmail, string memPassword)
        {
            try
            {
                // 돌아오는 멤버 ID 및 이름을 저장하기 위한 객체
                SessionData sessionData = new SessionData();
                
                // DB를 새로 연다.
                conn = db.DbOpen();
                //열려 있으면 사용한다.
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                string query = "SELECT * FROM dbo.Member WHERE memEmail = '" + memEmail + "' AND memPassword = '" + memPassword + "';";

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
                    sessionData.MemberID = Convert.ToInt32(item["memID"]);
                    sessionData.MemberName = item["memName"].ToString();
                    sessionData.MemberEmail = item["memEmail"].ToString();
                    sessionData.MemberPhone = Convert.ToInt32(item["memPhone"]);
                }
                db.DbClose();
                
                return sessionData;
            }
            catch (SqlException sqlEx)
            {
                sqlEx.Message.ToString();
                return null;
            }
        }

        public class SessionData
        {
            public string MemberName { get; set; }
            public int MemberID { get; set; }
            public string MemberEmail { get; set; }
            public int MemberPhone { get; set; }
        }
    }
}