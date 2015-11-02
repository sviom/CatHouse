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

        // 회원 삭제
        public bool MemberRemove(string memEmail)
        {
            // 이메일을 가지고 회원의 ID를 찾은 후에 삭제한다.
            try
            {
                // DB를 새로 연다.
                conn = db.DbOpen();
                //열려 있으면 사용한다.
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }

                // 아이디를 가져오기 위한 쿼리문 생성
                string selectIDQuery = "SELECT memID FROM dbo.Member WHERE memEmail = '" + memEmail + "';";
                // Command 작성
                SqlCommand selectQuery = new SqlCommand(selectIDQuery, conn);
                // 쿼리 시작 및 읽어오기 시작
                IAsyncResult selectQueryResult = selectQuery.BeginExecuteReader();
                // 데이터 리더 생성
                SqlDataReader dataReader = selectQuery.EndExecuteReader(selectQueryResult);
                // 아이디를 받기 위한 변수
                int memID = -1;
                // 리더가 끝날 때 까지 돌려서 전부 읽어온다.
                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        memID = Convert.ToInt32(dataReader.GetValue(i));
                    }
                }

                // 아이디가 -1이면 오류
                if (memID == -1)
                {
                    throw new Exception();
                }

                // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                string deleteQuery = "DELETE FROM dbo.Member WHERE memID = " + memID + ";";
                SqlCommand sqlQuery = new SqlCommand(deleteQuery, conn);
                // 실행
                int rows = sqlQuery.ExecuteNonQuery();

                // 영향을 받는 행이 없으면 오류
                if (rows < 1)
                {
                    throw new Exception();
                }

                // 삭제에 성공하면 true리턴
                db.DbClose();
                return true;
            }
            catch (SqlException sqlEx)
            {
                // 삭제에 실패하면 false 리턴
                sqlEx.Message.ToString();
                return false;
            }
        }
    }
}