using CatHouse_Renewal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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

        // 필터로 업자의 정보 가져오기
        public List<FilterSearchView> TraderSelectWithFilter(string existPet, int petNumber)
        {
            try
            {
                conn = db.DbOpen();
                //열려 있으면 사용한다.
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }

                // 기존 동물의 존재여부와 마릿수를 가지고 있는 동물들 전부를 가져온다.existPetIntro
                // 기존 동물의 존재 여부 뿐만이 아니라 주소를 기반으로 검색한다.
                // string query = "SELECT * FROM dbo.TraderMember WHERE existPet = @existPet OR existPetIntro = @petNumber;";

                // 모든 사용자의 좌표 데이터를 가져와서 마커 클러스터에 표시한다.
                string query = "SELECT dm.memEmail as memberEmail, dm.memName as memberName, dm.memAddress as memberAddress , dt.* FROM dbo.Member dm INNER JOIN dbo.TraderMember dt ON dm.memID = dt.memID;";

                // 커맨드 생성
                SqlCommand sqlQuery = new SqlCommand(query, conn);
                // 커맨드에 파라미터 추가 (MemID)
                // sqlQuery.Parameters.AddWithValue("@existPet", existPet);        // 기존 동물 존재 여부
                //sqlQuery.Parameters.AddWithValue("@petNumber", petNumber);      // 동물 숫자 여부

                SqlDataReader item = sqlQuery.ExecuteReader();
                // DB가 데이터를 가지고 있으면 관련된 자료 리턴
                if (item.HasRows == false)
                {
                    // 값이 없으면 없다고 리턴해주어야 한다.
                    throw new Exception();
                }

                // 컨트롤러 쪽으로 넘겨줄 객체 생성
                List<FilterSearchView> traderList = new List<FilterSearchView>();

                // 데이터를 전부 읽어들이면서 좌표값을 읽어온다.
                while (item.Read())
                {
                    FilterSearchView modelItem = new FilterSearchView()
                    {
                        memEmail = item["memberEmail"].ToString(),              // 업자 이메일
                        memName = item["memberName"].ToString(),                // 업자 이름
                        memAddress = item["memberAddress"].ToString(),          // 업자 집 주소. 여기서 위도와 경도 정보를 가져온다.
                        homePrice = Convert.ToInt32(item["homePrice"]),         // 업자의 서비스 제공 가격
                        homeIntro = (item["homeIntro"] == null ? "No come Intro" : item["homeIntro"].ToString()),       // 집 소개가 존재하지 않으면 No come Intro라고 쓰여 보낸다.
                        existPetIntro = (item["existPetIntro"] == null ? "No exist pet Intro." : item["existPetIntro"].ToString()), // 집에 동물이 몇마리 있는가
                        existPet = (item["existPet"] == null ? false : Convert.ToBoolean(item["existPet"])),                        // 기존의 동물 존재 여부
                    };
                    traderList.Add(modelItem);
                }
                db.DbClose();

                // 해당 값을 가진 모든 업자들을 컨트롤러로 전달해준다.
                return traderList;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        // 집 주소 가져오기
        public HomeModel GetMemberAddress(int memId, string memEmail)
        {
            try
            {
                conn = db.DbOpen();
                //열려 있으면 사용한다.
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }

                // 커맨드 생성 / 프로시저는 멤버 정보 갖고 오는거
                SqlCommand sqlQuery = new SqlCommand("P_GET_MEMBER_ADDRESS", conn);
                sqlQuery.CommandType = CommandType.StoredProcedure;
                // 커맨드에 파라미터 추가 (MemID)
                sqlQuery.Parameters.AddWithValue("@memId", memId);
                sqlQuery.Parameters.AddWithValue("@memEmail", memEmail);

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