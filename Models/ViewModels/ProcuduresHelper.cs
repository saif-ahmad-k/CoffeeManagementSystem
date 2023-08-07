using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace CoffeePricingMgt.Models
{
    public class ProcuduresHelper
    {
        DataContext db = new DataContext();
        string connectionString = "Data Source=.;" + "Initial Catalog=CoffeePricingDB;Integrated Security=SSPI";
        //66.165.248.146
        //.\\MSSQLSERVER2016
        //string connectionString = "Data Source=66.165.248.146;Initial Catalog=labcom_LabMS;User Id=LabMSuser;Password=Mjfn387^;integrated security=False";
        //string connectionString = "Data Source=95.216.202.81;Initial Catalog=buyeskco_LabMS;User Id=LabUser;Password=6fS%w7x0;integrated security=False";
        //string connectionString = "Data Source=66.165.248.146;Initial Catalog=labcom_LABDB;User Id=LabDBUser;Password=b~l8t5O0;integrated security=False";

        public DataSet GetFOBOfferList()
        {
            //DateTime FromDate, DateTime ToDate, int SerialNumber, int DoctorId, int PatientId
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["DataContext1"].ConnectionString;
            string SQL = "pFOBOfferList";
            SqlConnection con = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter param;
            //if (FromDate != DateTime.MinValue && ToDate != DateTime.MinValue && FromDate != null && ToDate != null)
            //{
            //    param = cmd.Parameters.Add("@FromDate", SqlDbType.DateTime);
            //    param.Value = FromDate;
            //    param = cmd.Parameters.Add("@ToDate", SqlDbType.DateTime);
            //    param.Value = ToDate;
            //}
            //if (SerialNumber != null && SerialNumber != 0)
            //{
            //    param = cmd.Parameters.Add("@SerialNumber", SqlDbType.Int);
            //    param.Value = SerialNumber;
            //}
            //if (DoctorId != null && DoctorId != 0)
            //{
            //    param = cmd.Parameters.Add("@DoctorId", SqlDbType.Int);
            //    param.Value = DoctorId;
            //}
            //if (PatientId != null && PatientId != 0)
            //{
            //    param = cmd.Parameters.Add("@PatientId", SqlDbType.Int);
            //    param.Value = PatientId;
            //}
            con.Open();

            int rowsAffected = cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            da.Fill(ds);
            con.Close();
            return ds;
        }
    }
}