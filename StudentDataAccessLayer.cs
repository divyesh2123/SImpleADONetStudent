using System.Data;
using System.Data.SqlClient;

namespace SImpleADONetExample
{
    public class StudentDataAccessLayer
    {
        string connectionString = "Data Source=LAPTOP-BLNTEBH7\\SQLEXPRESS; Initial Catalog=ADOEveningBatch;Trusted_Connection=True;TrustServerCertificate=true";
        public List<Student> GetAllStudent()
        {
            List<Student> lstStudent = new List<Student>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.FirstName = rdr["FirstName"].ToString();
                    student.LastName = rdr["LastName"].ToString();
                    student.Email = rdr["Email"].ToString();
                    student.Mobile = rdr["Mobile"].ToString();
                    student.Address = rdr["Address"].ToString();

                    lstStudent.Add(student);


                }

                con.Close();


            }

            return lstStudent;

        }

        public Student GetStudent(int studentId)
        {
            Student student = new Student();

            using (SqlConnection co = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from Student where id=" + studentId, co);

                sqlCommand.CommandType = CommandType.Text;

                co.Open();
                SqlDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.FirstName = rdr["FirstName"].ToString();
                    student.LastName = rdr["LastName"].ToString();
                    student.Email = rdr["Email"].ToString();
                    student.Mobile = rdr["Mobile"].ToString();
                    student.Address = rdr["Address"].ToString();


                }

                return student;
            }

        }
        public void AddStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void updateStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                cmd.Parameters.AddWithValue("@Id", student.Id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (SqlConnection d = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("DeleteStudent", d);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StudentId", studentId);

                d.Open();

                int effectrows = sqlCommand.ExecuteNonQuery();

            }



        }
    }
}
