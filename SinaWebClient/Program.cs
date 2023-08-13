using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

Console.WriteLine("1-View list of courses and their basic details\n" +
                  "2-View detailed information about a specific course including instructor and enrolled students\n" +
                  "3-Create a new course\n" +
                  "4-Enroll a student in a course\n" +
                  "5-Update course details\n" +
                  "6-Delete a course\n" +
                  "7-Exit the application\n");

//HttpClient httpClient = new HttpClient();

//httpClient.BaseAddress = new Uri("https://localhost:7040/");

while (true)
{
    using (HttpClient httpClient = new HttpClient())
    {
        httpClient.BaseAddress = new Uri("https://localhost:7040/");

        int userOption = Convert.ToInt32(Console.ReadLine());

        if (userOption == 1)
        {
            HttpResponseMessage response = await httpClient.GetAsync("Course/GetAllCourses");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JArray Jarrayobject = JArray.Parse(responseBody);
                string formattedJson = JsonConvert.SerializeObject(Jarrayobject, Formatting.Indented);

                // Process the response body
                Console.WriteLine(formattedJson);
            }
        }
        else if (userOption == 2)
        {
            Console.WriteLine("Enter your Id : ");
            string id = Console.ReadLine();
            HttpResponseMessage response = await httpClient.GetAsync("Course/GetSpecific?CourseId=" + id);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JArray Jarrayobject = JArray.Parse(responseBody);
                string formattedJson = JsonConvert.SerializeObject(Jarrayobject, Formatting.Indented);

                // Process the response body
                Console.WriteLine(formattedJson);
            }
        }
        else if (userOption == 3)
        {
            Console.WriteLine("Enter course name :");
            string name = Console.ReadLine();

            var requestData = new { courseName = name };

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("Course/CreateCourse", content);

        }
        else if (userOption == 4)
        {
            Console.WriteLine("Enter course Id :");
            string CourseId = Console.ReadLine();
            Console.WriteLine("Enter student Id :");
            string StudentId = Console.ReadLine();


            var requestData = new { studentId = StudentId, courseId = CourseId };

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("Course/EnrollStudent", content);
        }
        else if (userOption == 5)
        {
            Console.WriteLine("Enter course Id :");
            string CourseId = Console.ReadLine();
            Console.WriteLine("Enter new course name :");
            string CouseName = Console.ReadLine();
            Console.WriteLine("Enter instructor Id : ");
            string insId= Console.ReadLine();


            var requestData = new { id = CourseId, courseName = CouseName, instructorId = insId };

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync("Course/UpdateCourse", content);
        }
        else if (userOption == 6)
        {
            Console.WriteLine("Enter course Id :");
            string CourseId = Console.ReadLine();

            HttpResponseMessage response = await httpClient.DeleteAsync("Course/DeleteCourse?Id="+CourseId);
        }
        else if (userOption == 7)
        {
            break;
        }
        else
        {
            Console.WriteLine("Somthing Went Wrong!...\n");
        }

    }
}


