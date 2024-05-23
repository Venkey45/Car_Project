using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Car_Project.Models;
using System.IO;

namespace Car_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Car_Api : ControllerBase
    {
        private readonly Repositry _repositry;
        
        public Car_Api(Repositry repositry)
        {
            _repositry = repositry;
        }
        [HttpPost]
       public async Task<IActionResult>Register([FromForm]Model obj,[FromForm]string otp)
        {
            
            if (otp == obj.Email_OTP)
            {
                await _repositry.Register(obj);
               
            }
            return Ok();

        }
       [HttpPost("Otp")]
        public async Task<IActionResult>Otp([FromForm] Model obj)
        {
            string otp = "";
            string otp1 = "1234567890";
            Random Random = new Random();
            for (int i = 0; i < 4; i++)
            {
                otp += otp1[Random.Next(otp1.Length)];
            }
            
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("vadalavenkatesh456@gmail.com");
                mailMessage.To.Add($"{obj.Email}");
                mailMessage.Subject = "Subject";
                mailMessage.Body = $"Hi Welcome to the VB_car_ShowRoom : OTP is {otp}";

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
            return Ok(otp);
        }
        [HttpGet("image")]
        public async Task<IActionResult> image()
        {
            string imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pics");
            var imageFiles= Directory.GetFiles(imagepath);
            List<string> imagepaths = new List<string>();
           foreach(var imageFile in imageFiles)
            {
                var Filname = Path.GetFileName(imageFile);
                imagepaths.Add( Filname);
            }
            return Ok(imagepaths);
        }
        [HttpPost("user_Booking_details")]
        public async Task<IActionResult> user_Booking_Details([FromForm]Model obj)
        {
           
            await _repositry.user_Booking_details(obj);
            return Ok();
        }
        [HttpPost("Logincheck")]
        public async Task<IActionResult> Logincheck([FromForm] Model obj)
        {
            bool user = await _repositry.Login(obj.Email, obj.password); // Assuming obj.Password for consistency
            if (user)
            {

                return Ok(new { mesaage=obj.Email});
            }
            else
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
        }
        [HttpGet("Login_user_details1")]
        public async Task<IActionResult> Login_user_details1([FromQuery] string email)
        {
            var model = await _repositry.Login_user_details(new Model { Email = email });
            if (model == null || !model.Any())
            {
                return NotFound("No Data Found");
            }
            return Ok(model);
        }
    }
}
