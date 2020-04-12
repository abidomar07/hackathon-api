using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackathon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace Hackathon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        //public IConfiguration Configuration { get; private set; }

        private readonly IConfiguration configuration;

        public MemberController(IConfiguration config)
        {
            this.configuration = config;
        }

        // GET: api/Member
        [HttpGet]
        public IList<Member> Get()
        {
            
            List<Member> memberList = new List<Member>();
            //string CS =
            //ConfigurationExtensions
            //.GetConnectionString(this.Configuration, "HackathonDbConnectionString");
            //string CS = ConfigurationManager.ConnectionStrings["HackathonDbConnectionString"].ConnectionString;
            var CS = configuration.GetConnectionString("HackathonDbConnectionString");
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Member", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var member = new Member();

                    member.MemberId = (int)rdr["MemberId"];
                    member.Name = rdr["Name"].ToString();
                    member.Email = rdr["Email"].ToString();
                    
                    memberList.Add(member);
                }
            }
            return memberList;
        }

        // GET: api/Member/5
        [HttpGet("{id}")]
        public ActionResult<Member> Get(int id)
        {
            var CS = configuration.GetConnectionString("HackathonDbConnectionString");
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Member where MemberId =" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    Member member = new Member();

                    member.MemberId = (int)rdr["MemberId"];
                    member.Name = rdr["Name"].ToString();
                    member.Email = rdr["Email"].ToString();

                    return Ok(member);
                }
                else
                {
                    return NotFound();
                }
            }           
        }

        // POST: api/Member
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Member/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
