using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ManagerStaff2.Models
{
    public class Staff
    {
        [Required(ErrorMessage = "Moi nhap id")]
        [Display(Name = "id")]
        public int id { get; set; }
        [Required(ErrorMessage = "Moi nhap idStaff")]
        [Display(Name = "idStaff")]
        public string idstaff { get; set; }
        [Required(ErrorMessage = "Moi nhap name")]
        [Display(Name = "name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Moi nhap birthdate")]
        [Display(Name = "birthdate")]
        public DateTime birthdate { get; set; }
        [Required(ErrorMessage = "Moi nhap gender")]
        [Display(Name = "gender")]
        public bool gender { get; set; }
        [Required(ErrorMessage = "Moi nhap active")]
        [Display(Name = "active")]
        public bool active { get; set; }
    }

    class StaffList
    {
        DBConnection db;
        public StaffList()
        {
            db = new DBConnection();
        }
        public List<Staff> getStaff(string id)
        {
            string sql;
            if (string.IsNullOrEmpty(id))
                sql = "SELECT * FROM dbo.Staff";
            else
                sql = "SELECT * FROM dbo.Staff WHERE id=" + id;
            List<Staff> lst = new List<Staff>();
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            Staff staffTemp;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                staffTemp = new Staff();
                staffTemp.id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                staffTemp.idstaff = dt.Rows[i]["idstaff"].ToString();
                staffTemp.name = dt.Rows[i]["name"].ToString();
                staffTemp.birthdate = Convert.ToDateTime(dt.Rows[i]["birthdate"].ToString());
                staffTemp.gender = Convert.ToInt32(dt.Rows[i]["gender"].ToString()) == 1 ? true : false;
                staffTemp.active = String.Compare(dt.Rows[i]["active"].ToString(), "true", true) == 0;
                lst.Add(staffTemp);
            }
            return lst;
        }
        public void AddStaff(Staff staff)
        {
            int gender = staff.gender == true ? 1 : 0;
            int active = staff.active == true ? 1 : 0;
            String.Format("{0:yyyy-MM-dd HH:mm:ss}", staff.birthdate);
            SqlConnection conn = db.getConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.POC_INSERT";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.Add("@IDSTAFF", SqlDbType.NVarChar).Value = staff.idstaff;
            cmd.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = staff.idstaff;
            cmd.Parameters.Add("@BIRTHDATE", SqlDbType.DateTime).Value = staff.birthdate;
            cmd.Parameters.Add("@GENDER", SqlDbType.TinyInt).Value = gender;
            cmd.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = active;
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        public void UpdateStaff(Staff staff)
        {
            int gender = staff.gender == true ? 1 : 0;
            int active = staff.active == true ? 1 : 0;
            String.Format("{0:yyyy-MM-dd HH:mm:ss}", staff.birthdate);
            SqlConnection conn = db.getConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.POC_UPDATE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = staff.id;
            cmd.Parameters.Add("@IDSTAFF", SqlDbType.NVarChar).Value = staff.idstaff;
            cmd.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = staff.idstaff;
            cmd.Parameters.Add("@BIRTHDATE", SqlDbType.DateTime).Value = staff.birthdate;
            cmd.Parameters.Add("@GENDER", SqlDbType.TinyInt).Value = gender;
            cmd.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = active;
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        public void DeleteStaff(Staff staff)
        {
            int gender = staff.gender == true ? 1 : 0;
            int active = staff.active == true ? 1 : 0;
            String.Format("{0:yyyy-MM-dd HH:mm:ss}", staff.birthdate);
            SqlConnection conn = db.getConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "POC_DELETE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = staff.id;
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
    }
}