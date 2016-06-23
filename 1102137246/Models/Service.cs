using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace _1102137246.Models
{
    public class Service
    {
        public List<Models.Employee> GetCodeTitle() {
            DataTable dataTable = new DataTable();
            string str = @"SELECT CodeId, CodeVal
                            FROM CodeTable";
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(str, conn);
                SqlDataAdapter ada = new SqlDataAdapter(comm);
                ada.Fill(dataTable);
                conn.Close();
            }
            return ConvertGetCodeTitle(dataTable);
        }

        public List<Models.Employee> GetCountry()
        {
            DataTable dataTable = new DataTable();
            string str = @"SELECT Country
                            FROM HR.Employees";
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(str, conn);
                SqlDataAdapter ada = new SqlDataAdapter(comm);
                ada.Fill(dataTable);
                conn.Close();
            }
            return ConvertGetCountry(dataTable);
        }



        public List<Models.Employee> Result(Models.Employee employee)
        {
            DataTable dataTable = new DataTable();
            string str = @"SELECT EmployeeID, FirstName, Title, Gender
                            FROM HR.Employees
                            WHERE (EmployeeID = @EmployeeID OR @EmployeeID = 0) AND
		                            (Title = @Title OR @Title IS NULL) AND
		                            (FirstName = @FirstName OR @FirstName IS NULL)";
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(str, conn);
                comm.Parameters.Add(new SqlParameter("@EmployeeID", employee.EmpId));
                comm.Parameters.Add(new SqlParameter("@Title", employee.Title == null ? (Object)DBNull.Value : employee.Title));
                comm.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName == null ? (object)DBNull.Value : employee.FirstName));
                SqlDataAdapter ada = new SqlDataAdapter(comm);
                ada.Fill(dataTable);
                conn.Close();
            }
            return ConvertResult(dataTable);
        }

        public void Delete(int employeeId)
        {
            string str = @"DELETE FROM HR.Employees
                            WHERE EmployeeID = @EmployeeID";
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(str, conn);
                comm.Parameters.Add(new SqlParameter("@EmployeeID", employeeId));
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Insert(Models.Employee employee)
        {
            string str = @"INSERT INTO HR.Employees(FirstName, LastName, Title, TitleOfCourtesy, BirthDate, HireDate,
						                            Address, City, Region, Gender, Country, Phone, MonthlyPayment, YearlyPayment)
                            VALUES (@FirstName, @LastName, @Title, @TitleOfCourtesy, @BirthDate, @HireDate, @Address, 
                                                    @City, @Region, @Gender, @Country, @Phone, @MonthlyPayment, @YearlyPayment)";
            using (SqlConnection conn = new SqlConnection(ConnectionString()))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(str, conn);
                comm.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                comm.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                comm.Parameters.Add(new SqlParameter("@Title", employee.Title));
                comm.Parameters.Add(new SqlParameter("@TitleOfCourtesy", employee.TitleOfCourtesy));
                comm.Parameters.Add(new SqlParameter("@BirthDate", employee.BirthDate));
                comm.Parameters.Add(new SqlParameter("@HireDate", employee.HireDate));
                comm.Parameters.Add(new SqlParameter("@Address", employee.Address));
                comm.Parameters.Add(new SqlParameter("@City", employee.City));
                comm.Parameters.Add(new SqlParameter("@Region", employee.Region));
                comm.Parameters.Add(new SqlParameter("@Gender", employee.Gender));
                comm.Parameters.Add(new SqlParameter("@Country", employee.Country));
                comm.Parameters.Add(new SqlParameter("@Phone", employee.Phone));
                comm.Parameters.Add(new SqlParameter("@MonthlyPayment", employee.MonthlyPayment));
                comm.Parameters.Add(new SqlParameter("@YearlyPayment", employee.YearlyPayment));
                SqlDataAdapter ada = new SqlDataAdapter(comm);
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }

        private List<Employee> ConvertResult(DataTable dataTable)
        {
            List<Models.Employee> list = new List<Employee>();
            foreach (DataRow item in dataTable.Rows)
            {
                list.Add(new Employee
                {
                    EmpId = (int)item["EmployeeID"],
                    Gender = item["Gender"].ToString(),
                    Title = item["Title"].ToString(),
                    FirstName = item["FirstName"].ToString()
                });
            }
            return list;
        }

        private List<Employee> ConvertGetCodeTitle(DataTable dataTable)
        {
            List<Models.Employee> list = new List<Employee>();
            foreach (DataRow item in dataTable.Rows)
            {
                list.Add(new Employee
                {
                    CodeId = item["CodeId"].ToString(),
                    CodeVal = item["CodeVal"].ToString(),
                });
            }
            return list;
        }

        private List<Employee> ConvertGetCountry(DataTable dataTable)
        {
            List<Models.Employee> list = new List<Employee>();
            foreach (DataRow item in dataTable.Rows)
            {
                list.Add(new Employee
                {
                    Country = item["Country"].ToString(),
                });
            }
            return list;
        }

        private string ConnectionString() {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultString"].ConnectionString.ToString();
        }
    }
}