using Dapper;
using Sadiqa.Model;
using System.Data;
using System.Data.SqlClient;

namespace sadiqa.Model
{
    public class VenderList
    {
        public int vid { get; set; } = 0;
        public int lid { get; set; } = 0;

        public string title { get; set; } = "";
        public string loc { get; set; } = "";
        public string cat { get; set; } = "";
        public string fname { get; set; } = "";
        public string lname { get; set; } = "";
        public string mob { get; set; } = "";
        public string gen { get; set; } = "";

        static string dataAccess = DataAccess.GetConnection();

        public VenderResponse AddVender(VenderList venderList)
        {
            VenderResponse venderResponse = new VenderResponse();
            Venders venders = new Venders();
            if (venderList == null)
            {
                venderResponse.status = "failed";
                return venderResponse;
            }

            using (SqlConnection sqlConnection = new SqlConnection(dataAccess))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                DynamicParameters dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("@lid", venderList.lid);
                dynamicParameters.Add("@title", venderList.title);
                dynamicParameters.Add("@loc", venderList.loc);
                dynamicParameters.Add("@cat", venderList.cat);
                dynamicParameters.Add("@fname", venderList.fname);
                dynamicParameters.Add("@lname", venderList.lname);
                dynamicParameters.Add("@mob", venderList.mob);
                dynamicParameters.Add("@gen", venderList.gen);
                dynamicParameters.Add("@intResult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                SqlMapper.Query<int>(sqlConnection, "sp_VenderList_add", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                int result = dynamicParameters.Get<int>("@intResult");
                if (result > 0)
                {
                    venders.id = result;
                    venderResponse.account = venders;
                    venderResponse.status = "succeed";
                    venderResponse.message = "succeed";
                }

                else
                {
                    venderResponse.status = "failed";
                    venderResponse.message = "Try after some time";
                }
            }
            return venderResponse;
        }
        public static VenderProfileResponse? GetVenderList(int userid)
        {
            VenderProfileResponse venderProfileResponse = new VenderProfileResponse();
            using (SqlConnection sqlConnection = new SqlConnection(dataAccess))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@lid", userid);
                venderProfileResponse.data = SqlMapper.Query<GetVenderProfile>(sqlConnection, "sp_VenderList_view", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
                venderProfileResponse.status = "succeed";
                venderProfileResponse.message = "succeed";
            }
            return venderProfileResponse;
        }
        public static VenderDetailResponse? GetVenderDetails(int userid)
        {
            VenderDetailResponse venderDetailResponse = new VenderDetailResponse();
            using (SqlConnection sqlConnection = new SqlConnection(dataAccess))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@vid", userid);
                venderDetailResponse.data = SqlMapper.Query<GetVenderProfile>(sqlConnection, "sp_VenderDetail_view", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                venderDetailResponse.status = "succeed";
                venderDetailResponse.message = "succeed";
            }
            return venderDetailResponse;
        }
    }

    public class Venders
    {
        public int id { get; set; } = 0;
    }

    public class VenderResponse
    {
        public Venders account { get; set; }
        public string status { get; set; } = "";
        public string message { get; set; } = "";
    }

    public class GetVenderProfile
{
    public int vid { get; set; } = 0;
    public int lid { get; set; } = 0;

        public string title { get; set; } = "";
        public string loc { get; set; } = "";
        public string cat { get; set; } = "";
        public string fname { get; set; } = "";
        public string lname { get; set; } = "";
        public string mob { get; set; } = "";
        public string gen { get; set; } = "";
    }
    public class VenderProfileResponse
    {
        public List<GetVenderProfile> data { get; set; }
        public string status { get; set; } = "";
        public string message { get; set; } = "";
    }
    public class VenderDetailResponse
    {
        public GetVenderProfile data { get; set; }
        public string status { get; set; } = "";
        public string message { get; set; } = "";
    }
}
