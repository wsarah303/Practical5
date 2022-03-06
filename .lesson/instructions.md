## COM741 Web Applications Development

### Practical 5

At the end of this practical we will have a simple but functional web application providing Student CRUD (Create, Read, Update, Delete) functionality. You should ensure you are familiar with the basics of how the MVC pattern works in .NET MVC

*Please ensure that as you complete each question you verify the application still compiles and DO not move on until you fix the issues. Also ensure you use proper layout and keep all .cs and .cshtml files neat using proper indentation and comments where necessary*

1. Complete the functionality to view a list of Students as follows:
    - Complete the StudentController ```Index()``` action by calling the service method ```GetStudents()```and passing the students returned to the View for display. 
    - Complete the ```Index.cshtml``` view to display the list of students. Use the Razor ```@foreach(var s in Model) {...}``` command to render a table row for each student.
    - Modify the ```Index.cshtml``` file to add a ```View``` anchor tag in the 'Action' column for each student. When clicked the anchor tag will navigate to view the details of the student with the specified Id. The anchor tag is configured as follows. Note the use of the razor asp-controller, asp-action and the optional asp-route-id attributes: ```<a asp-controller="Student" asp-action="Details" asp-route-id="@s.Id">View</a>```
  
2. Complete the functionality to view a specific Student as follows:
    - Complete the StudentController ```Details(int id)``` action by calling the service method ```GetStudent(id)```, passing the id parameter. Check that if the student returned is null (could happen if the id passed to the details action is for a non existent student) then return ```NotFound()``` otherwise return the view containing the student as a parameter.
    -  Complete the ```Details.cshtml``` view file to display the student in a data list ```<dl></dl>``` (see practical 3). Use the ```@Model.Name``` syntax to display model properties at the relevant positions in the view.
    - Modify the ```Details.cshtml``` file to add an ```Index``` anchor tag, that when clicked will navigate back to the list of students (no route id is required). The anchor tag should be styled as a bootstrap primary button.
 
3. Complete the functionality to Create a specific Student as follows:
    - Modify the ```Index.cshtml``` view to add a ```Create``` anchor tag (above the table), that when clicked will navigate to the ```Create()``` StudentController action. The anchor tag should be styled as a bootstrap primary button. 
    - Complete the ```Create.cshtml``` view by making the necessary amendments listed in the view file. Each form input ```div``` contains a ```label```, ```input``` and ```span``` element and should have the necessary ```asp-for``` and ```asp-validation-for``` properties added. See the ```Name``` input for an example.
    - Modify the ```Student.cs``` model in the SMS.Data.Models package and add validation attributes as follows. 
        - Name, Course, Age, Email, Grade are ```[Required]```
        - Age should be in a ```[Range(16,80)]``` and Grade in a ```[Range(0,100)]``` 
        - Email should be an ```[EmailAddress]```
        - PhotoUrl a ```[Url]```
    - Complete the StudentControllers  ```Create(Student s)``` action (POST) so that it saves the validated student using the service method ```AddStudent(...)```, before redirecting to the Index view

4. Complete the functionality to Edit a specific Student as follows
    - Modify the ```Details.cshtml``` file to add an ```Edit``` anchor tag beside the existing Index anchor tag, so that when clicked will navigate to the StudentController ```Edit(int id)``` action. The anchor tag should be styled as a bootstrap primary button. You will need to configure the asp-route-id property of the anchor tag (see Q1 for an example).
    - Complete the StudentController ```Edit(int id)``` action (GET) by verifying that the student exists before passing to the view.
    - Complete the ```Edit.cshtml``` view file (see comments in file) to display the form used to collect student data. You can copy the form groups needed from the Create.cshtml file.
    - Complete the StudentController ```Edit(int id, Student s)``` action (POST) to save the validated form data passed via the Student 's' parameter, using the service ```UpdateStudent(s)``` method.

5. OPTIONAL - complete the Delete student functionality as outlined in the notes. 
    - Complete the ```Delete.cshtml``` view to contain a form that submits the ```DeleteDetails``` POST request to the student controller.
    - Add a "Delete" anchor tag beside "Index" and "Edit" in the Details.cshtml view
    - Complete the ```DeleteConfirm(...)``` action in the StudentController.cs file so that it calls the service to delete the student. 
