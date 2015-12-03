using CatHouse_Renewal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatHouse_Renewal.DB
{
    public class SelectDB : Controller
    {
        // DBConnection 객체를 이용해 DB를 열고 닫는다.
        DBConnection db = new DBConnection();
        // DBConnection 객체에서 받아온 SqlConnection 객체를 받아옴.
        private static SqlConnection conn;

        // 해당 회원의 좌표 가져오기
        public HomeModel MemberCoordinateSelect(int memID)
        {
            try
            {
                conn = db.DbOpen();
                //열려 있으면 사용한다.
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }

                // 해당 회원 정보를 가져온다.
                string query = "SELECT * FROM dbo.Member WHERE memID = @memID;";
                // 커맨드 생성
                SqlCommand sqlQuery = new SqlCommand(query, conn);
                // 커맨드에 파라미터 추가 (MemID)
                sqlQuery.Parameters.AddWithValue("@memID", memID);

                SqlDataReader item = sqlQuery.ExecuteReader();
                // DB가 데이터를 가지고 있으면 관련된 자료 리턴
                if (item.HasRows == false)
                {
                    // 없으면 에러처리
                    throw new Exception();
                }

                // 컨트롤러 쪽으로 넘겨줄 객체 생성
                HomeModel memAddress = new HomeModel();
                // 데이터를 전부 읽어들이면서 좌표값을 읽어온다.
                while (item.Read())
                {
                    memAddress = JsonConvert.DeserializeObject<HomeModel>(item["memAddress"].ToString());
                }
                db.DbClose();
                // 좌표값 리턴
                return memAddress;

            }
            catch (SqlException sqlEx)
            {
                sqlEx.Message.ToString();
                return null;
            }
        }
    }
}