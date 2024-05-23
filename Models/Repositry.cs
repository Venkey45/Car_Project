using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace Car_Project.Models
{
    public class Repositry
    {

        private readonly string _ConnectionStrings;
        public Repositry(string ConnectionStrings)
        {
            _ConnectionStrings = ConnectionStrings;
        }
        public async Task Register(Model obj)
        {
            using (SqlConnection Con = new SqlConnection(_ConnectionStrings))
            {
                SqlCommand cmd = new SqlCommand("proc_userdata_Register", Con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@pass", obj.password);
                cmd.Parameters.AddWithValue("@email", obj.Email);
                cmd.Parameters.AddWithValue("@phone", obj.phone);
                Con.Open();
                await cmd.ExecuteNonQueryAsync();
                Con.Close();
            }

        }
        public async Task user_Booking_details(Model obj)
        {
            using (SqlConnection con1 = new SqlConnection(_ConnectionStrings))
            {
                SqlCommand cmd1 = new SqlCommand("proc_user_booking_id_return", con1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@email", obj._Email);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                await con1.OpenAsync();
                da1.Fill(dt1);
                con1.Close();
                string v = dt1.Rows[0]["Booking_id"].ToString();
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("vadalavenkatesh456@gmail.com");
                    mailMessage.To.Add($"{obj._Email}");
                    mailMessage.Subject = "Subject";
                    mailMessage.Body = $"Hi Welcome to the Car Show Room Your Booking Details is " +
                        $"Booking id :{v}" +
                        $"Name       :{obj.B_Name}" +
                        $"Email      :{obj._Email}" +
                        $"Model      :{obj.DeloreanModel}" +
                        $"PRice      :{obj.Deloreanprice}" +
                        $"💕Thanks for Booking 🏎"
                        ;

                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.Port = 587;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential("vadalavenkatesh456@gmail.com", "ktuw vbuo chbi sapr");
                        smtpClient.EnableSsl = true;
                        smtpClient.Timeout = 10000; // Increased timeout

                        try
                        {
                            smtpClient.Send(mailMessage);

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

                using (SqlConnection con = new SqlConnection(_ConnectionStrings))
            {
                SqlCommand cmd = new SqlCommand("proc_car_Booking_", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", obj.B_Name);
                cmd.Parameters.AddWithValue("@phone", obj._Phone);
                cmd.Parameters.AddWithValue("@email", obj._Email);
                cmd.Parameters.AddWithValue("@model", obj.DeloreanModel);
                cmd.Parameters.AddWithValue("@price", obj.Deloreanprice);
                cmd.Parameters.AddWithValue("@data", obj.time);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
     
        }
        public async Task<bool> Login(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionStrings))
            {
                bool flag = false;
                using (SqlCommand cmd = new SqlCommand("proc_car_Login", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    await con.OpenAsync();
                    flag = Convert.ToBoolean(await cmd.ExecuteScalarAsync());
                }
                return flag;
            }
        }
        public async Task<List<Model>> Login_user_details(Model obj1)
        {
            List<Model> result = new List<Model>();
            using (SqlConnection con = new SqlConnection(_ConnectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("proc_car_Booking_deatils1", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", obj1.Email);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        await con.OpenAsync();
                        da.Fill(dt);
                        con.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Model model = new Model
                            {
                                Booking_id = Convert.ToInt32(dr["Booking_id"]),
                                DeloreanModel = Convert.ToString(dr["Model"]),
                                Deloreanprice = Convert.ToDecimal(dr["price"]),
                                time = Convert.ToString(dr["book_date"])
                            };

                            result.Add(model);

                        }
                    }

                }
                return result;
            }
        }
    }
}
