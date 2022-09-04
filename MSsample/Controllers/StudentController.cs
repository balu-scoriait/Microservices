using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MSsample.Model;

namespace MSsample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                           select * from
                           dbo.student
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CollegeDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }



        [Route("CreateStudent")]


        [HttpPost]

        public JsonResult CreateStudent(Student dep)
        {
            string query = @"
                           insert into dbo.student
                           values (@id, @name, @role, @phone, @address)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CollegeDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", dep.Id);
                    myCommand.Parameters.AddWithValue("@name", dep.Name);
                    myCommand.Parameters.AddWithValue("@role", dep.Role);
                    myCommand.Parameters.AddWithValue("@phone", dep.Phone);
                    myCommand.Parameters.AddWithValue("@address", dep.Address);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }
        [HttpPut("{id}")] // made changes

        public JsonResult Put(Student dep)
        {
            string query = @"
                           update dbo.student
                           set name= @Name, role= @Role, phone= @Phone, address= @Address
                           where id= @Id
                            ";
        /*    @"
                           update dbo.student
                           set name= @Name, role= @Role, phone= @Phone, address= @Address
                           where id= @Id
                            ";*/

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CollegeDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", dep.Id);
                    myCommand.Parameters.AddWithValue("@name", dep.Name);
                    myCommand.Parameters.AddWithValue("@role", dep.Role);
                    myCommand.Parameters.AddWithValue("@phone", dep.Phone);
                    myCommand.Parameters.AddWithValue("@address", dep.Address);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");

        }


        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.student                           
                           where id= @Id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CollegeDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");

        }
    }
}
