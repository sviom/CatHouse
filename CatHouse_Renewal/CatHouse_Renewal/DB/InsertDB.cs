using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CatHouse_Renewal.Models;
using System.Data;

namespace CatHouse_Renewal.DB
{
    public class InsertDB
    {
        // DBConnection 객체를 이용해 DB를 열고 닫는다.
        DBConnection db = new DBConnection();
        // DBConnection 객체에서 받아온 SqlConnection 객체를 받아옴.
        private static SqlConnection conn;

        /// <summary>
        /// 회원 가입 함수
        /// 회원은 가입하면 고양이와 업자가 될 가능성이 있으므로 최소한의 데이터를 포함한 컬럼을 하나 만들어 놓는다.
        /// 고양이와 업자로 추가로 할 때는 ALTER를 하는 방향으로
        /// </summary>
        /// <param name="memItem"></param>
        /// <returns>bool</returns>
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


        // 고양이를 DB에 넣기
        public bool CatInsertToDB(CatModel catItem)
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
                    // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                    SqlCommand createMemSqlQuery = new SqlCommand("P_CREATE_NEW_MEMBER", conn);
                    // 데이터 입력 형식은 저장 프로시저
                    createMemSqlQuery.CommandType = CommandType.StoredProcedure;
                    sqlQuery.Parameters.AddWithValue("@name", catItem.catName);
                    sqlQuery.Parameters.AddWithValue("@age", catItem.catAge);
                    sqlQuery.Parameters.AddWithValue("@gender", catItem.catGender);
                    sqlQuery.Parameters.AddWithValue("@netu", catItem.catNeuter);
                    sqlQuery.Parameters.AddWithValue("@memo", catItem.catMemo);
                    sqlQuery.Parameters.AddWithValue("@url", catItem.catPhotoURL);

                    // Input param
                    SqlParameter pInput = new SqlParameter("@in", SqlDbType.Int);
                    pInput.Direction = ParameterDirection.Input;
                    pInput.Value = 1;
                    createMemSqlQuery.Parameters.Add(pInput);

                    // Output param
                    SqlParameter pOutput = new SqlParameter("@out", SqlDbType.Int);
                    pOutput.Direction = ParameterDirection.Output;
                    createMemSqlQuery.Parameters.Add(pOutput);

                    // Return value
                    SqlParameter pReturn = new SqlParameter();
                    pReturn.Direction = ParameterDirection.ReturnValue;
                    createMemSqlQuery.Parameters.Add(pReturn);

                    // Run SP
                    createMemSqlQuery.ExecuteNonQuery();

                    Console.WriteLine(pOutput.Value); // output
                    Console.WriteLine(pReturn.Value); // return value

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

        //업자 회원 추가
        public bool TraderMemberInsertToDB(TraderModel tmemItem)
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
                    // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모
                    string query = "INSERT INTO dbo.TraderMember (homePrice,existPetintro,existPet,homePhotoURL,homeIntro,homeAddress) VALUES (@homePrice,@existPetintro,@existPet,@homePhotoURL,@homeIntro,@homeAddress)";
                    SqlCommand sqlQuery = new SqlCommand(query, conn);
                    sqlQuery.Parameters.AddWithValue("@homePrice", tmemItem.homePrice);
                    sqlQuery.Parameters.AddWithValue("@existPetintro", tmemItem.existPetIntro);
                    sqlQuery.Parameters.AddWithValue("@existPet", tmemItem.existPet);
                    sqlQuery.Parameters.AddWithValue("@homePhotoURL", tmemItem.homePhotoURL);
                    sqlQuery.Parameters.AddWithValue("@homeIntro", tmemItem.homeIntro);
                    sqlQuery.Parameters.AddWithValue("@homeAddress", tmemItem.homeAddress);
                    // 쿼리문 실행
                    int rows = sqlQuery.ExecuteNonQuery();
                }
                db.DbClose();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }
}