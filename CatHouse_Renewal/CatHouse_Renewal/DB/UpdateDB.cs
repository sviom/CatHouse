using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.DB
{
    public class UpdateDB
    {
        // DBConnection 객체를 이용해 DB를 열고 닫는다.
        DBConnection db = new DBConnection();
        // DBConnection 객체에서 받아온 SqlConnection 객체를 받아옴.
        private static SqlConnection conn;

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                conn = db.DbOpen();
                //열려 있으면 사용한다.
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                else
                {
                    // 비밀번호 변경 쿼리
                    string query = "UPDATE dbo.Member SET memPassword = @newPassword WHERE memPassword = '" + oldPassword + "';";
                    // 커맨드 생성
                    SqlCommand sqlQuery = new SqlCommand(query, conn);
                    // 커맨드에 파라미터 추가
                    sqlQuery.Parameters.AddWithValue("@newPassword", newPassword);
                    // 쿼리문 실행
                    int rows = sqlQuery.ExecuteNonQuery();
                    // 영향을 받은 행의 갯수가 없으면 에러
                    if (rows <= 0)
                    {
                        throw new Exception();
                    }
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