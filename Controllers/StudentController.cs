using Microsoft.AspNetCore.Mvc;

namespace SImpleADONetExample.Controllers
{
    public class StudentController : Controller
    {
        private StudentDataAccessLayer GetStudentData;
        public StudentController()
        {
            GetStudentData = new StudentDataAccessLayer();
        }
        public IActionResult Index()
        {
            var students = GetStudentData.GetAllStudent();
            return View(students);
        }

        public IActionResult Edit(int editId)
        {
            var d = GetStudentData.GetStudent(editId);
            return View("Create",d);
        }

        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if(!ModelState.IsValid)
            {
                return View(student);   
            }
            StudentDataAccessLayer studentDataAccessLayer = new StudentDataAccessLayer();

            if (student.Id > 0)
            {
                studentDataAccessLayer.updateStudent(student);
            }
            else
            {
                studentDataAccessLayer.AddStudent(student);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteInfo(int deleteId)
        {
            StudentDataAccessLayer studentDataAccessLayer = new StudentDataAccessLayer();
            studentDataAccessLayer.DeleteStudent(deleteId);

            return RedirectToAction("Index");

        }



    }
}
