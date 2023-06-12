using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PROJECT_12
{
    class Program
    {
        public static bool IsIdAvailable(SqlConnection con,string str)
        {
           
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = str;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                con.Close();
                return true;
                }
                else
                {
                con.Close();
                return false;
                }
           
        }
        public static bool IsCharacter(string chr)
        {
            Regex Reg = new Regex("[A-Za-z]");
            if(Reg.IsMatch(chr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsPhoneNumber(string num)
        {
            Regex Reg = new Regex("^[7-9]{1}[0-9]{9}$");
            if(Reg.IsMatch(num))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool IsGender(string G)
        {
            Regex g = new Regex("(Male|Female|M|F|male|female)");
            if (g.IsMatch(G))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsNumber(string n)
        {
            Regex Reg = new Regex("[0-9]");
            if (Reg.IsMatch(n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsEnrollmentId(string E)
        {
            Regex Reg = new Regex("^[E]{1}[0-9]$");
            if (Reg.IsMatch(E))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ChangesWithStudentTable(SqlConnection con)
        {
          A:  try
            {
                bool b = true;
                while (b)
                {
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Choose any one option");
                    Console.WriteLine("1.Add new student");
                    Console.WriteLine("2.Student list");
                    Console.WriteLine("3.Update student information");
                    Console.WriteLine("4.Remove student record");
                    Console.WriteLine("5.Exit");

                    Console.Write("\nEnter your choice : ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                    
                    switch (n)
                    {
                        case 1:
                            {
                                AddNewStudent(con);
                            Add: Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Add new student");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s1 = Convert.ToInt32(Console.ReadLine());
                                if (s1 == 1)
                                {
                                    AddNewStudent(con);
                                    goto Add;
                                }
                                else if (s1 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("ERROR : Invalid choice");
                                    goto Add;
                                }

                            }
                        case 2:
                            {
                                StudentList(con);
                            Select: Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Display all details for specific student");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s2 = Convert.ToInt32(Console.ReadLine());
                                if (s2 == 1)
                                {
                                    DisplayStudentRecord(con);
                                    goto Select;
                                }
                                else if (s2 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nERROR : Invalid choice");
                                    goto Select;
                                }

                            }
                        case 3:
                            {
                                UpdateStudentInformation(con);
                            Upd: Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Enter another Id to update.");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s3 = Convert.ToInt32(Console.ReadLine());
                                if (s3 == 1)
                                {
                                    UpdateStudentInformation(con);
                                    goto Upd;
                                }
                                else if (s3 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice");
                                    goto Upd;
                                }

                            }
                        case 4:
                            {
                                RemoveStudentRecord(con);
                            Remove: Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Enter another Id to Remove.");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s4 = Convert.ToInt32(Console.ReadLine());
                                if (s4 == 1)
                                {
                                    RemoveStudentRecord(con);
                                    goto Remove;
                                }
                                else if (s4 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nERROR : Invalid choice");
                                    goto Remove;
                                }

                            }
                        case 5:
                            {
                                b = false;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("\nERROR : Invalid Choice");
                                break;
                            }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                goto A;
            }

        }
        public static void AddNewStudent(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Insert_Student_Information1", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            try
            {

            Fnm: Console.Write("Enter student first name : ");
                string Fnm = Console.ReadLine();
                if (IsCharacter(Fnm))
                {
                    cmd.Parameters.AddWithValue("@Firstname", Fnm);
                }
                else
                {
                    Console.WriteLine("Input string is not in correct format");
                    goto Fnm;
                }


            Lnm: Console.Write("Enter student Last name : ");
                string Lnm = Console.ReadLine();
                if (IsCharacter(Lnm))
                {
                    cmd.Parameters.AddWithValue("@Lastname", Lnm);
                }
                else
                {

                    Console.WriteLine("Input string is not in correct format");
                    goto Lnm;
                }


            db1: try
                {
                db: Console.Write("Enter student DOB in YYYY-MM-DD format :");
                    DateTime Dob = Convert.ToDateTime(Console.ReadLine());
                    var age = DateTime.Today.Year - Dob.Year;

                    if (age > 15 && age < 100)
                    {

                        cmd.Parameters.AddWithValue("@DOB", Dob);
                    }
                    else
                    {

                        Console.WriteLine($"Invalid {age} age should be between 15 to 100");
                        goto db;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    goto db1;
                }

            
                gen: Console.Write("Enter student gender : ");
                string G = Console.ReadLine();
                if (IsGender(G))
                {
                    cmd.Parameters.AddWithValue("@Gender", G);
                }
                else
                {

                    Console.WriteLine("Input string is not in correct format");
                    goto gen;
                }

               ph: try
                {
                p: Console.Write("Enter student Phone number : ");
                    string phn = Console.ReadLine();
                    if (IsPhoneNumber(phn))
                    {
                        cmd.Parameters.AddWithValue("@PhoneNumber", phn);
                    }
                    else
                    {
                        Console.WriteLine("\nERROR : Number should be 10 digit and start with 7 or 8 or 9");
                        goto p;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("\n" + e.Message);
                    goto ph;
                }


            A: Console.Write("Enter student Address : ");
                string Add = Console.ReadLine();
                if (IsCharacter(Add))
                {
                    cmd.Parameters.AddWithValue("@Address", Add);
                }
                else
                {
                    Console.WriteLine("Input string is not in correct format");
                    goto A;
                }


                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            finally
            {
                con.Close();
            }
            Console.WriteLine("Record Inserted successfully");
          
        }
        public static void StudentList(SqlConnection con)
        {
            con.Open();
           d: try
            {
                string str = "Select * from Student";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = str;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("SId \tFirst Name \t Last Name");
                    Console.WriteLine("------------------------------------");
                    while (dr.Read())
                    {
                        Console.WriteLine($"{dr[0]} \t {dr[1]} \t {dr[2]} ");
                    }

                }
            }
            catch (Exception e)
            {
                con.Close();
                Console.WriteLine("\n" + e.Message);
                goto d;
            }
            finally
            {
                con.Close();
            }
        }
        public static void UpdateStudentInformation(SqlConnection con)
        {
           U: try
            {
                string str = null;
               Up: Console.Write("Enter student id : ");
                int id = Convert.ToInt32(Console.ReadLine());
                string str1 = $"Select * from Student where Id = {id}";
                bool b = IsIdAvailable(con,str1);
                if (b)
                {
                    con.Open();
                    Console.WriteLine("\nWhich column you want to update");
                    Console.WriteLine("1.First Name\n2.Last Name\n3.DOB\n4.Gender\n5.Phone Number\n6.Address\n7.Exit");

                    bool b2 = true;

                    while (b2)
                    {
                       a: Console.Write("\nEnter your choice : ");
                        int a = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
                        if (a == 1)
                        {
                        Fnm: Console.Write("Enter student first name : ");
                            string Fnm = Console.ReadLine();
                            if (IsCharacter(Fnm))
                            {
                                str = $"Update Student set FirstName ='{Fnm}' where Id={id}";

                            }
                            else
                            {

                                Console.WriteLine("\nERROR : Input string is not in correct format");
                                goto Fnm;
                            }
                        }

                      else  if (a == 2)
                        {
                        Lnm: Console.Write("Enter student Last name : ");
                            string Lnm = Console.ReadLine();
                            if (IsCharacter(Lnm))
                            {
                                str = $"Update Student set LastName='{Lnm}' where Id={id}";

                            }
                            else
                            {

                                Console.WriteLine("\nERROR : Input string is not in correct format");
                                goto Lnm;
                            }
                        }

                       else if (a == 3)
                        {

                        db1: try
                            {

                            db: Console.Write("Enter student DOB in YYYY-MM-DD format :");
                                DateTime Dob = Convert.ToDateTime(Console.ReadLine());
                                var age = DateTime.Today.Year - Dob.Year;

                                if (age > 15 && age < 100)
                                {

                                    str = $"Update Student set DOB='{Dob}'where Id={id}";
                                }
                                else
                                {

                                    Console.WriteLine($"\nERROR : Invalid age should be between 15 to 100");
                                    goto db;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                goto db1;
                            }
                        }

                       else if (a == 4)
                        {
                           
                        gen: Console.Write("Enter student gender : ");
                            string G = Console.ReadLine();
                            if (IsGender(G))
                            {
                                str = $"Update Student set Gender='{G}' where Id={id}";
                            }
                            else
                            {

                                Console.WriteLine("\nERROR : Input string is not in correct format");
                                goto gen;
                            }
                        }

                       else if (a == 5)
                        {
                         ph: try
                            {
                            p: Console.Write("Enter student Phone number : ");
                                string phn = Console.ReadLine();
                                if (IsPhoneNumber(phn))
                                {
                                    str = $"Update Student set PhoneNumber={phn} where Id={id}";
                                }
                                else
                                {
                                    Console.Write("\nERROR : Number should be 10 digit and start with 7 or 8 or 9");
                                    goto p;
                                }
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("\n" + e.Message);
                                goto ph;
                            }
                        }

                       else if (a == 6)
                        {
                        A: Console.Write("Enter student Address : ");
                            string Add = Console.ReadLine();
                            if (IsCharacter(Add))
                            {
                                str = $"Update Student set Address='{Add}' where Id={id}";
                            }
                            else
                            {
                                Console.WriteLine("\nERROR : Input string is not in correct format");
                                goto A;
                            }
                        }
                       else if (a == 7)
                        {
                            b2 = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nERROR : Invalid Choice");
                            goto a;
                        }

                        SqlCommand cmd = new SqlCommand(str, con);

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("\nDo you want to update another data for same stduent..");

                    }
                    Console.WriteLine("\n**Record update successfully**");
                }
                else
                {
                    Console.WriteLine("\nERROR : Particular Entered Id is not present in the list");
                    goto Up;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
                goto U;
            }
            finally
            {
                con.Close();
            }
          
        }
        public static void RemoveStudentRecord(SqlConnection con)
        {
           try
            {
                Console.Write("Enter Student Id : ");
                int id = Convert.ToInt32(Console.ReadLine());
                string str1 = $"Select * from Student where Id = {id}";
                bool b = IsIdAvailable(con, str1);
               
                if (b)
                {
                    con.Open();
                    string str = $"delete from Student where Id=({id})";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = str;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                 
                    Console.WriteLine("\n**Record deleted successfully**");
                }
                else
                {
                    Console.WriteLine("ERROR : Particular Entered Id is not present in the list");
                    //goto Remo;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
              //  goto R;
           
            }
            finally
            {
                con.Close();
            }
        }
        public static void DisplayStudentRecord(SqlConnection con)
        {
           // con.Open();
           d:try
            {
             D:   Console.Write("\n1.Enter the Student Id To Display All Details : ");
                int id = int.Parse(Console.ReadLine());

               con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $"Select * from Student where Id = {id}";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine("Sid\tFirst Name\tLast Name\tDate Of Birth\tGender\tPhone Number\t Address");
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine(dr[0] + "\t" + dr[1] + "\t " + dr[2] + "" + dr.GetDateTime(3).ToString("yyyy-MM-dd") + "\t" + dr[4] + "\t" + dr[5] + "\t" + dr[6] + "\n");
                }
                else
                {
                    con.Close();
                    Console.WriteLine("\nERROR : Particular Entered Id is not present in the list");
                    goto D;
                }
            }
            catch (Exception E)
            {
                con.Close();
                Console.Write("\n" +E.Message);
                goto d;
            }
            finally
            {
                con.Close();
            }
        }


        public static void ChangesWithCourseTable(SqlConnection con)
        {
          A:  try
            {
                bool b = true;
                while (b)
                {
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Choose any one option ");
                    Console.WriteLine("1.Add new Course");
                    Console.WriteLine("2.Course list");
                    Console.WriteLine("3.Update Course information");
                    Console.WriteLine("4.Remove Course");
                    Console.WriteLine("5.Exit");

                    Console.Write("\nEnter your choice :");
                    int n = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                    switch (n)
                    {
                        case 1:
                            {
                                AddNewCourse(con);
                            Add: Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Add new Course");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s1 = Convert.ToInt32(Console.ReadLine());
                                if (s1 == 1)
                                {
                                    AddNewCourse(con);
                                    goto Add;
                                }
                                else if (s1 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice");
                                    goto Add;
                                }
                                // break;

                            }
                        case 2:
                            {
                                CourseList(con);
                            Select: Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Display all Course details");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s2 = Convert.ToInt32(Console.ReadLine());
                                if (s2 == 1)
                                {
                                    DisplayCourseDetails(con);
                                    goto Select;
                                }
                                else if (s2 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice");
                                    goto Select;
                                }
                                //break;
                            }
                        case 3:
                            {
                                UpdateCourseInformation(con);
                            Upd: Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Enter another Id to new update.");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s3 = Convert.ToInt32(Console.ReadLine());
                                if (s3 == 1)
                                {
                                    UpdateCourseInformation(con);
                                    goto Upd;
                                }
                                else if (s3 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice");
                                    goto Upd;
                                }
                                //break;

                            }
                        case 4:
                            {
                                RemoveCourse(con);
                            Remove: Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Enter another Id to Remove.");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s4 = Convert.ToInt32(Console.ReadLine());
                                if (s4 == 1)
                                {
                                    RemoveCourse(con);
                                    goto Remove;
                                }
                                else if (s4 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice");
                                    goto Remove;
                                }
                                // break;
                            }
                        case 5:
                            {
                                b = false;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid Choice");
                                break;
                            }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                goto A;
            }
        }
        public static void AddNewCourse(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Add_New_Course_Info", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
        U:  try
            {
            Fnm: Console.Write("Enter Course Name : ");
                string Cnm = Console.ReadLine();
                if (IsCharacter(Cnm))
                {
                    cmd.Parameters.AddWithValue("@Cname", Cnm);
                }
                else
                {
                    Console.WriteLine("\nERROR : Input string is not in correct format");
                    goto Fnm;
                }


            A: Console.Write("Enter Subject : ");
                string sub = Console.ReadLine();
                if (IsCharacter(sub))
                {
                    cmd.Parameters.AddWithValue("@Subject", sub);
                }
                else
                {
                    Console.WriteLine("ERROR : Input string is not in correct format");
                    goto A;
                }

              e:  try
                {
                p: Console.Write("Enter Course Duration(Between 1 to 36) : ");
                    int Du = Convert.ToInt32(Console.ReadLine());
                    if (Du > 0 && Du < 37)
                    {
                        cmd.Parameters.AddWithValue("@Duration", Du);
                    }
                    else
                    {
                        Console.WriteLine("\nERROR : Input string is not in correct format");
                        goto p;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("\n"+ e.Message);
                    goto e;
                }

                cmd.ExecuteNonQuery();
                Console.WriteLine("\n**Record Inserted successfully**");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                con.Close();
                goto U;
            }
            finally
            {
                con.Close();
            }
          
        }
        public static void CourseList(SqlConnection con)
        {
           
           e:try
           {
                con.Open();
                string str = "Select * from Course";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = str;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Cid \t\t Course Name");
                    Console.WriteLine("-------------------------------");
                    while (dr.Read())
                    {
                        Console.WriteLine($"{dr[0]} \t\t {dr[1]}");

                    }

                }
            }
            catch (Exception e)
            {
                con.Close();
                Console.WriteLine(e.Message);
                goto e;
            }
            finally
            {
                con.Close();
            }
        }
        public static void UpdateCourseInformation(SqlConnection con)
        {
            try
            {
                string str = null;

             Up:  Console.Write("Enter course id : ");
                int id = Convert.ToInt32(Console.ReadLine());
                string str1= $"Select * from Course where Cid = {id}";
                bool b = IsIdAvailable(con, str1);
                if (b)
                {
                    con.Open();
                    Console.WriteLine("\nWhich column you want to update");
                    Console.WriteLine("1.All \n2.Only Course name\n3.Only subject\n4.Only course duration\n5.Exit");

                    bool b2 = true;

                    while (b2)
                    {
                        Console.Write("\nEnter your choice : ");
                        int a = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                        if (a == 1)
                        {
                        all: Console.Write("Enter Course Name : ");
                            string Cnm = Console.ReadLine();
                            Console.Write("Enter Subject  : ");
                            string sub = Console.ReadLine();
                            Console.Write("Enter Course Duration : ");
                            string Du = Console.ReadLine();

                            if (IsCharacter(Cnm) && IsCharacter(sub) && IsNumber(Du))
                            {
                                str = $"Update Course set Cname ='{Cnm}',Subject='{sub}',Duration={Du} where Cid={id}";
                            }
                            else
                            {

                                Console.WriteLine("\nERROR : Input string is not in correct format");
                                goto all;
                            }
                        }
                        if (a == 2)
                        {
                        cnm: Console.Write("Enter Course Name : ");
                            string Cnm = Console.ReadLine();

                            if (IsCharacter(Cnm))
                            {
                                str = $"Update Course set Cname ='{Cnm}' where Cid={id}";
                            }
                            else
                            {

                                Console.WriteLine("\nERROR : Input string is not in correct format");
                                goto cnm;
                            }
                        }

                        if (a == 3)
                        {
                        cnm: Console.Write("Enter Subject Name : ");
                            string sub = Console.ReadLine();
                            if (IsCharacter(sub))
                            {
                                str = $"Update Course set Subject ='{sub}' where Cid={id}";
                            }
                            else
                            {

                                Console.WriteLine("\nERROR : Input string is not in correct format");
                                goto cnm;
                            }
                        }
                        if (a == 4)
                        {
                           w: try
                            {
                            p: Console.Write("\nEnter Course Duration(Between 1 To 36) : ");
                                int Du = Convert.ToInt32(Console.ReadLine());
                                if (Du > 0 && Du < 37)
                                {
                                    str = $"Update Course set Duration={Du} where Cid={id}";
                                }
                                else
                                {
                                    Console.Write("\nERROR : Input string is not in correct format");
                                    goto p;
                                }
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("\n" + e.Message);
                                goto w;
                            }
                        }

                        if (a == 5)
                        {
                            b2 = false;
                            break;
                        }

                        SqlCommand cmd = new SqlCommand(str, con);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Do u want to update anything else for same course..");
                    }
                    Console.WriteLine("\n**Record update successfully**");
                }
                else
                {
                    Console.WriteLine("ERROR : Entered Id is not present in the list");
                    goto Up;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
          
        }
        public static void RemoveCourse(SqlConnection con)
        {
            try
            {
                Console.Write("Enter student id : ");
                int id = Convert.ToInt32(Console.ReadLine());
                string str1 = $"Select * from Course where Cid = {id}";
                bool b = IsIdAvailable(con,str1);
                if (b)
                {
                    con.Open();

                    string str = $"delete from Course where Cid=({id})";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = str;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\n**Record Removed successfully**");
                }
                else
                {
                    Console.WriteLine("\nERROR : Entered Id is not present in the list");
                   // goto Remo;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
           
        }
        public static void DisplayCourseDetails(SqlConnection con)
        {
          R:try
            {
              a:  Console.Write("\n1.Enter the Course Id To Display All Details :");
                int id = int.Parse(Console.ReadLine());

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $"Select * from Course where Cid = {id}";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();

                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("Cid \t Course Name\t\tSubject Name\t Course Duratoion");
                    Console.WriteLine("----------------------------------------------------------------");

                    Console.WriteLine($"{dr[0]} \t  {dr[1]}\t {dr[2]} \t\t{dr[3]}");
                }
                else
                {
                    con.Close();
                    Console.WriteLine("\nERROR : Entered Id is not present in the list");
                    goto a;
                    
                }
            }
            catch (Exception E)
            {
                con.Close();
                Console.Write("\n" + E.Message);
                goto R;
            }
            finally
            {
                con.Close();
            }
        }

        public static void ChangesWithEnrollmentTable(SqlConnection con)
        {
            try
            {
                bool b = true;
                while (b)
                {
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Choose any one option :");
                    Console.WriteLine("1.Add New Enrollment Record");
                    Console.WriteLine("2.Display Enrollment Record");
                    Console.WriteLine("3.Update Enrollment Record");
                    Console.WriteLine("4.Remove Enrollment Record");
                    Console.WriteLine("5.Exit");

                    Console.Write("\nEnter your choice : ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                    switch (n)
                    {
                        case 1:
                            {
                                AddNewEnrollmentRecord(con);
                            Add: Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Add New Enrollment Record");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s1 = Convert.ToInt32(Console.ReadLine());
                                if (s1 == 1)
                                {
                                    AddNewEnrollmentRecord(con);
                                    goto Add;
                                }
                                else if (s1 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nERROR : Invalid choice");
                                    goto Add;
                                }

                            }
                        case 2:
                            {
                                DisplayEnrollmentRecord(con);
                                //Select: Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

                                //     //Console.WriteLine("1.Display Enrollment Record");
                                //     //Console.WriteLine("2.Exit");

                                //     int s2 = Convert.ToInt32(Console.ReadLine());
                                //     if (s2 == 1)
                                //     {
                                //         DisplayEnrollmentRecord(con);
                                //         goto Select;
                                //     }
                                //     else if (s2 == 2)
                                //     {
                                break;
                                //    }
                                //    else
                                //    {
                                //        Console.WriteLine("Invalid choice");
                                //        goto Select;
                                //    }
                            }
                        case 3:
                            {
                                UpdateEnrollmentRecord(con);
                            Upd: Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Update Enrollment Record");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s3 = Convert.ToInt32(Console.ReadLine());
                                if (s3 == 1)
                                {
                                    UpdateEnrollmentRecord(con);
                                    goto Upd;
                                }
                                else if (s3 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nERROR : Invalid choice");
                                    goto Upd;
                                }

                            }
                        case 4:
                            {
                                RemoveEnrollmentRecord(con);
                            Remove: Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                                Console.WriteLine("1.Remove Enrollment Record");
                                Console.WriteLine("2.Exit");
                                Console.Write("\nEnter your choice : ");
                                int s4 = Convert.ToInt32(Console.ReadLine());
                                if (s4 == 1)
                                {
                                    RemoveEnrollmentRecord(con);
                                    goto Remove;
                                }
                                else if (s4 == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nERROR : Invalid choice");
                                    goto Remove;
                                }
                            }
                        case 5:
                            {
                                b = false;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("\nERROR : Invalid Choice");
                                break;
                            }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void DisplayEnrollmentRecord(SqlConnection con)
        {
            con.Open();
         D: try
            {
               
                string str = "Select * from Enrollment";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = str;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("Eid \t\t Sid \t\tCid \t\tDateOfAdmission");
                    Console.WriteLine("---------------------------------------------------------------");
                    while (dr.Read())
                    {
                        Console.WriteLine($"{dr[0]} \t\t {dr[1]} \t\t {dr[2]} \t\t{dr.GetDateTime(3).ToString("yyyy-MM-dd")}");
                    }

                }
            }
            catch (Exception e)
            {
                con.Close();
                Console.WriteLine("\n" + e.Message);
                goto D;
            }
            finally
            {
                con.Close();
            }

        }
        public static void AddNewEnrollmentRecord(SqlConnection con)
        {
        U: try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Add_New_Enrollment1", con);
                cmd.CommandType = CommandType.StoredProcedure;
            Fnm: Console.Write("Enter Enrollment Id : ");

                string En = Console.ReadLine();
                if (IsEnrollmentId(En))
                {
                   cmd.Parameters.AddWithValue("@Eid", En);
                }
                else
                {
                    Console.WriteLine("\nERROR : Input string is not in correct format");
                    goto Fnm;
                }

               
                Lnm: Console.Write("Enter Student Id : ");
                    string sid = Console.ReadLine();
                if (IsNumber(sid))
                {
                    cmd.Parameters.AddWithValue("@Sid", sid);
                }
                else
                {

                    Console.WriteLine("\nERROR : Input string is not in correct format");
                    goto Lnm;
                }

              
                A: Console.Write("Enter Course Id : ");
                    string cid = Console.ReadLine();
                    if (IsNumber(cid))
                    {
                        cmd.Parameters.AddWithValue("@Cid", cid);
                    }
                    else
                    {
                        Console.WriteLine("\nERROR : Input string is not in correct format");
                        goto A;
                    }
              

            db1: try
                {
                db: Console.Write("Enter student DOB in YYYY-MM-DD format :");
                    DateTime DOA = Convert.ToDateTime(Console.ReadLine());
                    var dat = DateTime.Today.Day - DOA.Day;

                    if (dat>=0)
                    {

                       cmd.Parameters.AddWithValue("@DOA", DOA);
                    }
                    else
                    {

                        Console.WriteLine("\nERROR : Invalid age should be Past/Present");
                        goto db;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    goto db1;
                }

                
                cmd.ExecuteNonQuery();
                Console.WriteLine("\n**Record Inserted successfully**");
            }
            catch (Exception e)
            {
                con.Close();
                Console.WriteLine("\n" + e.Message);
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------");
                goto U;
            }
            finally
            {
                con.Close();
            }
        }
        public static void UpdateEnrollmentRecord(SqlConnection con)
        {
           
           u:try
            {
                string str = null;
              Up:Console.Write("Enter Enrollment id : ");
                string id = Console.ReadLine();
                string str1 = $"Select * from Enrollment where Eid = '{id}'";
                bool b = IsIdAvailable(con, str1);
                if(b)
                {
                 con.Open();
                Console.WriteLine("\nWhich Detail You Want To Update");
                Console.WriteLine("1.Update Course Id\n2.Update Date Of Admission\n3.Update Both Course id and Date of Enrollment\n4.Exit");

                bool b2 = true;

                    while (b2)
                    {
                        Console.Write("\nEnter your choice : ");
                        int a = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                        if (a == 1)
                        {
                        Fnm: Console.Write("Enter Course Id  : ");
                            string cid = Console.ReadLine();
                            if (IsNumber(cid))
                            {
                                str = $"Update Enrollment set Cid ='{cid}' where Eid='{id}'";

                            }
                            else
                            {

                                Console.WriteLine("\nERROR : Input string is not in correct format");
                                goto Fnm;
                            }
                        }

                        if (a == 2)
                        {

                        db1: try
                            {

                            db: Console.Write("Enter student DOB in YYYY-MM-DD format :");
                                DateTime DOA = Convert.ToDateTime(Console.ReadLine());
                                var dat = DateTime.Today.Day - DOA.Day;

                                if (dat >= 0)
                                {

                                    str = $"Update Enrollment set DOA='{DOA}'where Eid='{id}'";
                                }
                                else
                                {

                                    Console.WriteLine("\nERROR : Invalid date should be Past/Present");
                                    goto db;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                goto db1;
                            }
                        }
                        if (a == 3)
                        {
                        db1: try
                            {
                                Console.Write("Enter Course Id  : ");
                                string cid = Console.ReadLine();
                            db: Console.Write("Enter student DOB in YYYY-MM-DD format :");
                                DateTime DOA = Convert.ToDateTime(Console.ReadLine());
                                if (IsNumber(cid))
                                {
                                    str = $"Update Enrollment set Cid ='{cid}' where Eid='{id}'";

                                }

                                var dat = DateTime.Today.Day - DOA.Day;

                                if (dat >= 0)
                                {

                                    str = $"Update Enrollment set DOA='{DOA}'where Eid='{id}'";
                                }
                                else
                                {

                                    Console.WriteLine("ERROR : Invalid date should be Past or Present");
                                    goto db;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\n" + e.Message);
                                goto db1;
                            }
                        }
                        if (a == 4)
                        {
                            b2 = false;
                            break;
                        }

                        SqlCommand cmd = new SqlCommand(str, con);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Do you want to update anything else for same record..");
                    }
                    Console.WriteLine("**Record update successfully**");
                }
                else
                {
                    Console.WriteLine("\nERROR : Entered Id is not present in the list");
                    goto Up;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
                goto u;
            }
            finally
            {
                con.Close();
            }
           
        }
        public static void RemoveEnrollmentRecord(SqlConnection con)
        {
         
            try
            {
              Console.Write("Enter Enrollment Id : ");
                string id = Console.ReadLine();
                string str1 = $"Select * from Enrollment where Eid = '{id}'";
                bool b = IsIdAvailable(con, str1);
                if (b)
                {
                   con.Open();

                    string str = $"delete from Enrollment where Eid ='{id}'";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = str;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\n**Record deleted successfully**");
                }
                else
                {
                    Console.WriteLine("Particular Entered Id is not present in the list");
                    
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
          
        }

        public static void DisplayStudentAllRecord(SqlConnection con)
        {
            try
            {
                con.Open();
                string str = "Select * from vw_Student_Course_Enrollment";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = str;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Sid \tFisrt Name \t Last Name");
                    Console.WriteLine("---------------------------------------");
                    while (dr.Read())
                    {
                        Console.WriteLine($"{dr[0]} \t {dr[1]} {dr[2]}");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }

            bool b = true;
            while (b)
            {
            Select: Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");

                Console.WriteLine("1.Display All Details For Student");
                Console.WriteLine("2.Exit");
                Console.Write("\nEnter your choice : ");
                int s2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                if (s2 == 1)
                {
                    DisplayParticularStudentAllRecord(con);
                    goto Select;
                }
                else if (s2 == 2)
                {
                    b = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                    goto Select;
                }
            }
        }
        public static void DisplayParticularStudentAllRecord(SqlConnection con)
        {
            g: try
            {
                Console.Write("\n1.Enter the Student Id To Display All Details :");
                int id = int.Parse(Console.ReadLine());

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $"Select * from vw_Student_Course_Enrollment where Id = {id}";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    Console.WriteLine(" Sid \t FirstName \t\t LastName  \tCourseName \t AdmissionDate");
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    while (dr.Read())
                    {
                        Console.WriteLine($" {dr[0]} \t {dr[1]} \t{dr[2]}  {dr[3]}\t  {dr.GetDateTime(4).ToString("yyyy-MM-dd")}");
                    }
                }
                else
                {
                    Console.WriteLine("\nERROR : Entered Id is not present in the list");
                    con.Close();
                    goto g;
                }
            }

            catch (Exception E)
            {
                Console.Write(E.Message);
            }
            finally
            {
                con.Close();
            }
        }



static void Main(string[] args)
        {
            string constr = @"Data Source=laptop-vvrrm071\sqlexpress;Initial Catalog=PROJECT_1;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);

            bool b1 = true;
            while (b1)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Choose any option\n1.Student Details\n2.Course Details\n3.Enrollment Details\n4.Student Record With All Course Details\n5.Exit");
                Console.Write("\nEnter Your Choice : ");
                int n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        {
                            ChangesWithStudentTable(con);
                            break;
                        }
                    case 2:
                        {
                            ChangesWithCourseTable(con);
                            break;
                        }

                    case 3:
                        {
                            ChangesWithEnrollmentTable(con);
                            break;
                        }
                    case 4:
                        {
                            DisplayStudentAllRecord(con);
                            break;
                        }
                    case 5:
                        {
                            b1 = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR : Invalid Choice");
                            break;
                        }
                }
            }


        }
    }
}
