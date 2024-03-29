﻿using System;
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
                    SqlCommand createMemSqlQuery = new SqlCommand("P_CREATE_NEW_MEMBER", conn);
                    // 데이터 입력 형식은 저장 프로시저
                    createMemSqlQuery.CommandType = CommandType.StoredProcedure;
                    //@MemberName @MemberEmail @MemberPassword @MemberPhone @MemberAddress

                    // 파라미터 지정
                    SqlParameter memEmail = new SqlParameter("@MemberEmail", SqlDbType.NVarChar);
                    SqlParameter memName = new SqlParameter("@MemberName", SqlDbType.NVarChar);
                    SqlParameter memPassword = new SqlParameter("@MemberPassword", SqlDbType.NVarChar);
                    SqlParameter memPhone = new SqlParameter("@MemberPhone", SqlDbType.Int);
                    SqlParameter memAddress = new SqlParameter("@MemberAddress", SqlDbType.NVarChar);
                    SqlParameter createMemReturn = new SqlParameter();      //리턴 파라미터
                    // 입력 방향 지정
                    memEmail.Direction = ParameterDirection.Input;
                    memName.Direction = ParameterDirection.Input;
                    memPassword.Direction = ParameterDirection.Input;
                    memPhone.Direction = ParameterDirection.Input;
                    memAddress.Direction = ParameterDirection.Input;
                    createMemReturn.Direction = ParameterDirection.ReturnValue;
                    // 값 설정
                    memEmail.Value = memItem.memEmail;
                    memName.Value = memItem.memName;
                    memPassword.Value = memItem.memPassword;
                    memPhone.Value = memItem.memPhone;
                    memAddress.Value = memItem.memAddress;

                    // Output param
                    //SqlParameter pOutput = new SqlParameter("@out", SqlDbType.Int);
                    //pOutput.Direction = ParameterDirection.Output;
                    //createMemSqlQuery.Parameters.Add(pOutput);

                    // 설정한 프로시저의 파라미터들을 SqlCommand의 파라미터로 추가한다.
                    createMemSqlQuery.Parameters.Add(memEmail);
                    createMemSqlQuery.Parameters.Add(memName);
                    createMemSqlQuery.Parameters.Add(memPassword);
                    createMemSqlQuery.Parameters.Add(memPhone);
                    createMemSqlQuery.Parameters.Add(memAddress);
                    createMemSqlQuery.Parameters.Add(createMemReturn);      // 리턴 값

                    // 쿼리 실행ㅔ
                    int rows = createMemSqlQuery.ExecuteNonQuery();
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

        /// <summary>
        /// 고양이 등록 함수는 회원을 등록 할 때 NULL값이 포함된 것이 하나가 이미 만들어져 있기 때문에 UPDATE
        /// </summary>
        /// <param name="catItem"></param>
        /// <returns>true/false</returns>
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
                    // catName,catAge,catNetu,catGender,catMemo,catPhotoURL  검색 조건memID
                    // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모 쿼리 생성
                    string query = "UPDATE dbo.CatModel SET catName=@catName, catAge=@catAge, catNetu=@catNetu,catGender=@catGender,catMemo=@catMemo,catPhotoURL=@catPhotoURL WHERE memID = " + catItem.memID + ";";
                    // 커맨드 생성
                    SqlCommand sqlQuery = new SqlCommand(query, conn);
                    // 커맨드에 파라미터 추가
                    sqlQuery.Parameters.AddWithValue("@catName", catItem.catName);
                    sqlQuery.Parameters.AddWithValue("@catAge", catItem.catAge);
                    sqlQuery.Parameters.AddWithValue("@catNetu", catItem.catNeuter);
                    sqlQuery.Parameters.AddWithValue("@catGender", catItem.catGender);
                    sqlQuery.Parameters.AddWithValue("@catMemo", catItem.catMemo);
                    sqlQuery.Parameters.AddWithValue("@catPhotoURL", catItem.catPhotoURL);
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

        /// <summary>
        /// 업자 등록 함수는 회원을 등록할 때 NULL값이 포함된 것이 또한차례 만들어져 있기 때문에 UPDATE로 처리
        /// </summary>
        /// <param name="tmemItem"></param>
        /// <returns>true/false</returns>
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
                    string query = "UPDATE dbo.TraderMember SET homePrice=@homePrice, existPetintro=@existPetintro, existPet=@existPet,homePhotoURL=@homePhotoURL,homeIntro=@homeIntro,homeAddress=@homeAddress WHERE memID = " + tmemItem.memID + ";";
                    // 커맨드 생성
                    SqlCommand sqlQuery = new SqlCommand(query, conn);
                    // 커맨드에 파라미터 추가
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