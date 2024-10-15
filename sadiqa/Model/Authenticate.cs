using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace Sadiqa.Model
{
    public class Authenticate
    {
        public int id { get; set; } = 0;
        public string fname { get; set; } = "";
        public string lname { get; set; } = "";
        public string phone { get; set; } = "";
        public string mail { get; set; } = "";
        public string pass { get; set; } = "";


        static string dataAccess = DataAccess.GetConnection();

      public  ResultResponse RegisterAccount(Authenticate authenticate)
        {
            ResultResponse resultResponse = new ResultResponse();
            Sadiqas sadiqa=new Sadiqas();
            if (authenticate == null)
            {
                resultResponse.status = "failed";
                return resultResponse;
            }
          
            using (SqlConnection sqlConnection = new SqlConnection(dataAccess))
            {
               if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

               DynamicParameters dynamicParameters = new DynamicParameters();
               
                dynamicParameters.Add("@fname", authenticate.fname);
                dynamicParameters.Add("@lname", authenticate.lname);
                dynamicParameters.Add("@phone", authenticate.phone);
                dynamicParameters.Add("@mail", authenticate.mail);
                dynamicParameters.Add("@pass", authenticate.pass);
                dynamicParameters.Add("@intResult", dbType: DbType.Int32,direction:ParameterDirection.Output);
                SqlMapper.Query<int>(sqlConnection, "sp_register_add", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                int result =dynamicParameters.Get<int>("@intResult");
                if (result > 0)
                {
                    sadiqa.id = result;
                    resultResponse.account = sadiqa;
                    resultResponse.status = "succeed";
                    resultResponse.message = "succeed";
                }
                else if(result == -1)
                {
                    resultResponse.status = "failed";
                    resultResponse.message = "This email already exits";
                }
                else
                {
                    resultResponse.status = "failed";
                    resultResponse.message = "Try after some time";
                }
            }
            return resultResponse;
        }
        public static ProfileResponse? GetYourProfile(int userid)
        {
            ProfileResponse profileResponse = new ProfileResponse();
            using (SqlConnection sqlConnection = new SqlConnection(dataAccess))
            {
                if(sqlConnection.State==ConnectionState.Closed)
                 sqlConnection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", userid);
                profileResponse.data = SqlMapper.Query<GetProfile>(sqlConnection, "sp_register_view", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                profileResponse.status = "succeed";
                profileResponse.message = "succeed";
            }
            return profileResponse;
        }

        public Result UpdateProfile(Authenticate authenticate)
        {
            Result result = new Result();
            using (SqlConnection sqlconnection = new SqlConnection(dataAccess))
            {
                if (sqlconnection.State == ConnectionState.Closed)
                sqlconnection.Open();

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", authenticate.id);
                dynamicParameters.Add("@fname", authenticate.fname);
                dynamicParameters.Add("@lname", authenticate.lname);
                dynamicParameters.Add("@phone", authenticate.phone);
                dynamicParameters.Add("@mail", authenticate.mail);
                dynamicParameters.Add("@pass", authenticate.pass);
                dynamicParameters.Add("@intResult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                SqlMapper.Query<Authenticate>(sqlconnection, "sp_register_update", dynamicParameters,commandType: CommandType.StoredProcedure).FirstOrDefault();
                int newid = dynamicParameters.Get<int>("@intResult");
                if (newid > 0)
                {
                    result.status= "success";
                    result.message = "Successful Updated";

                }
                else
                {
                    result.status = "failed";
                    result.message = "Try after some time";
                }
            }
            return result;
        }
        public ResultResponse login(Authenticate authenticate)
        {
            ResultResponse obj = new ResultResponse();
            Sadiqas objaccount = new Sadiqas();
            if (authenticate == null)
            {
                obj.status = "failed";
                return obj;
            }

            using (SqlConnection con = new SqlConnection(dataAccess))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@mail", authenticate.mail);
                parameter.Add("@pass", authenticate.pass);
                int result = SqlMapper.Query<int>(con, "sp_login", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (result > 0)
                {
                    objaccount.id = result;
                    obj.account = objaccount;
                    obj.status = "succeed";
                    obj.message = "you are successfully login";
                }
                else
                {
                    obj.status = "failed";
                    obj.message = "You have entered either incorrect username or password.";
                }
            }
            return obj;
        }


    }
    public class Sadiqas
    {
        public int id { get; set; } = 0;
    }

    public class ResultResponse
    {
        public Sadiqas account { get; set; }
        public string status { get; set; } = "";
        public string message { get; set; } = "";
    }

    public class GetProfile
    {
        public int id { get; set; } = 0;
        public string fname { get; set; } = "";
        public string lname { get; set; } = "";
        public string phone { get; set; } = "";
        public string mail { get; set; } = "";
        public string pass { get; set; } = "";

    }
    public class ProfileResponse
    {
        public GetProfile data { get; set; }
        public string status { get; set; } = "";
        public string message { get; set; } = "";
    }
}