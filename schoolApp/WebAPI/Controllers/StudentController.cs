using Microsoft.AspNetCore.Mvc;
using schoolApp.Entities;
using schoolApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace schoolApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private SchoolDbContext _schoolDbContext;

        public StudentController()
        {
            _schoolDbContext = new SchoolDbContext();
        }

        // GET: api/<StudentController>
        [HttpGet]
        public IActionResult Get()
        {
            // LINQ QUERY - Method Syntax
            // EF will generate SQL query 
            // select * from dbo.students d
            // inner join StudentSubjects s on s.StudentID = d.StudentID 

            // it will get the result set( table records)
            // EF will convert the table records into List of student Objects
            
            
            var studentEntities = _schoolDbContext.StudentDetails.Include(p => p.StudentSubjects).ToList();

            var studentsData = new List<StudentModel>();

            foreach (var studentEntity in studentEntities)
            {
                // subject model create and also assign the values to properties
                var studentObj = new StudentModel
                {
                    class_id = studentEntity.ClassId,
                    student_name = studentEntity.FullName,
                    email = studentEntity.Email,
                    password = studentEntity.Password
                };

                // we will loop thrpugh subjects of students and we will take subject ids and 
                // add to list
                var subjectIds = new List<int>();
                if (studentEntity.StudentSubjects != null && studentEntity.StudentSubjects.Count > 0)
                {
                    foreach (var subject in studentEntity.StudentSubjects)
                    {
                        subjectIds.Add(subject.SubjectId);
                    }
                }

                // assing the subject ids
                studentObj.subject = subjectIds;

                studentsData.Add(studentObj);
            }

            return Ok(studentsData);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // LINQ QUERY
            // EF will generate SQL QUery
            // select * from Students d
            // inner join studentsubjects s on s.StudentID = d.StudentID
            // where d.studentid = 5

            // from db we will get one record
            // EF will convert that record into student object

            var studentEntity = _schoolDbContext.StudentDetails
                                       .Include(p => p.StudentSubjects)
                                       .FirstOrDefault(p => p.StudentId == id);

            if (studentEntity == null)
            {
                return Ok(null);
            }

            var studentObj = new StudentModel
            {
                class_id = studentEntity.ClassId,
                student_name = studentEntity.FullName,
                email = studentEntity.Email,
                password = studentEntity.Password,
                subject = studentEntity.StudentSubjects?.Select(p => p.SubjectId).ToList()
            };

            // we will loop thrpugh subjects of students and we will take subject ids and 
            // add to list
            var subjectIds = new List<int>();
            if (studentEntity.StudentSubjects != null && studentEntity.StudentSubjects.Count > 0)
            {
                foreach (var subject in studentEntity.StudentSubjects)
                {
                    subjectIds.Add(subject.SubjectId);
                }
            }

            studentObj.subject = subjectIds;

            return Ok(studentObj);
        }

        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Post(StudentModel model)
        {

            var names = model.student_name.Split(new char[] { ' ' });


            var studentDetail = new StudentDetail();
            studentDetail.FName = names[0];

            if (names.Length >= 2)
            {
                studentDetail.LName = names[1];
            }

            studentDetail.ClassId = model.class_id;
            studentDetail.Email = model.email;
            studentDetail.Password = model.password;


            // here we are passing the Student Object to EF 
            // EF will generate INSERT command from the student obj
            // and will execute on DB 
            // we are usign .Add(studentDetail), by this EF will consider Insert Command
            _schoolDbContext.StudentDetails.Add(studentDetail);
            _schoolDbContext.SaveChanges(); // execute and db changes will happen

            if (model.subject != null && model.subject.Count > 0)
            {

                foreach (var subjectId in model.subject)
                {
                    var studentSubject = new StudentSubject
                    {
                        StudentId = studentDetail.StudentId, // student was saved above
                        SubjectId = subjectId

                    };

                    _schoolDbContext.StudentSubjects.Add(studentSubject);
                }

                _schoolDbContext.SaveChanges();
            }


            var msg = "New Student Created Successfully with Id:" + studentDetail.StudentId;

           

            return Ok(msg);

        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, StudentModel model)
        {
            var names = model.student_name.Split(new char[] { ' ' });


            var studentDetail = _schoolDbContext.StudentDetails.FirstOrDefault(p => p.StudentId == id);

            studentDetail.FName = names[0];

            if (names.Length >= 2)
            {
                studentDetail.LName = names[1];
            }

            studentDetail.ClassId = model.class_id;
            studentDetail.Email = model.email;
            studentDetail.Password = model.password;

            // EF will generate UPDATE COMMAND
            _schoolDbContext.StudentDetails.Update(studentDetail);

            _schoolDbContext.SaveChanges();

            var msg = "Student data updated Successfully";

            return Ok(msg);
        }

    }
}
