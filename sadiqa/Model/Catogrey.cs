using Dapper;
using Sadiqa.Model;
using System.Data;
using System.Data.SqlClient;
namespace sadiqa.Model
{
    public class Catogrey
    {
        public int id { get; set; } = 0;

        public int subid { get; set; } = 0;
        public string img { get; set; } = "";
        public string title { get; set; } = "";
        public string desp { get; set; } = "";

        static string dataAccess = DataAccess.GetConnection();

        public CatogryResponse CatogryAdd(Catogrey authenticate)
        {
            CatogryResponse resultResponse = new CatogryResponse();
            Catogreys catogreys = new Catogreys();
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

                dynamicParameters.Add("@id", authenticate.id);
                dynamicParameters.Add("@img", authenticate.img);
                dynamicParameters.Add("@title", authenticate.title);
                dynamicParameters.Add("@desp", authenticate.desp);
                dynamicParameters.Add("@intResult", dbType: DbType.Int32, direction: ParameterDirection.Output);
                SqlMapper.Query<int>(sqlConnection, "SP_CatogryList_Insert", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                int result = dynamicParameters.Get<int>("@intResult");
                if (result > 0)
                {
                    catogreys.id = result;
                    resultResponse.account = catogreys;
                    resultResponse.status = "succeed";
                    resultResponse.message = "succeed";
                }
               
                else
                {
                    resultResponse.status = "failed";
                    resultResponse.message = "Try after some time";
                }
            }
            return resultResponse;
        }
          public static CatogryProfileResponse? GetCatogryList(int userid)
        {
            CatogryProfileResponse catogryProfileResponse = new CatogryProfileResponse();
            using (SqlConnection sqlConnection = new SqlConnection(dataAccess))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", userid);
                catogryProfileResponse.data = SqlMapper.Query<GetCatogryProfile>(sqlConnection, "SP_CatogryList_view", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
                catogryProfileResponse.status = "succeed";
                catogryProfileResponse.message = "succeed";
            }
            return catogryProfileResponse;
        }
        public static CatogryDetailResponse? GetCatogryDetails(int userid)
        {
            CatogryDetailResponse catogryDetailResponse = new CatogryDetailResponse();
            using (SqlConnection sqlConnection = new SqlConnection(dataAccess))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@subid", userid);
                catogryDetailResponse.data = SqlMapper.Query<GetCatogryProfile>(sqlConnection, "SP_CatogryDetail_view", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                catogryDetailResponse.status = "succeed";
                catogryDetailResponse.message = "succeed";
            }
            return catogryDetailResponse;
        }
    }
    public class Catogreys
    {
        public int id { get; set; } = 0;
    }

    public class CatogryResponse
    {
        public Catogreys account { get; set; }
        public string status { get; set; } = "";
        public string message { get; set; } = "";
    }

    public class GetCatogryProfile
    {
        public int id { get; set; } = 0;

        public int subid { get; set; } = 0;
        public string img { get; set; } = "";
        public string title { get; set; } = "";
        public string desp { get; set; } = "";
    }
    public class CatogryProfileResponse
    {
        public List<GetCatogryProfile> data { get; set; }
        public string status { get; set; } = "";
        public string message { get; set; } = "";
    }
    public class CatogryDetailResponse
    {
        public GetCatogryProfile data { get; set; }
        public string status { get; set; } = "";
        public string message { get; set; } = "";
    }
}
