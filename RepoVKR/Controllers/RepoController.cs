using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RepoVKR.Models;

namespace RepoVKR.Controllers
{
    public class RepoController : Controller
    {
        //
        // GET: /Repo/

        public ActionResult Index(string id)
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                int iLevelId = 0;
                int.TryParse(id, out iLevelId);

                var lst =
                    (from GradBook in context.GraduateBook
                     join Stud in context.Student on GradBook.StudentId equals Stud.Id
                     join Pers in context.Person on Stud.PersonId equals Pers.Id
                     join SLMask in context.StudyLevelNameMask on Stud.StudyLevelName equals SLMask.StudyLevelName
                     where SLMask.TypeInt == iLevelId
                     select new
                     {
                         Pers.Surname,
                         Pers.Name,
                         Stud.FacultyName,
                         Stud.DirectionName,
                         GradBook.Id,
                         GradBook.VKRName,

                     }).ToList()
                     .Select(x => new RepoMainListItem()
                     {
                         FIO = x.Surname + " " + x.Name,
                         Id = x.Id.ToString(),
                         VKRName = x.VKRName,

                     }).ToList();

                RepoMainList model = new RepoMainList()
                {
                    lstGraduates = lst,
                    lstFilters = new List<string>()
                };

                return View(model);
            }
        }

        public ActionResult Search(RepoSearchModel Model)
        {
            if (Model ==  null)
            {
                Model = new RepoSearchModel();
                Model.SLName1 = Model.SLName2 = Model.SLName3 = "1";
            }
            using (RepoVKREntities context = new RepoVKREntities())
            {
                if (Model.StudyLevelNameId == null)
                    Model.StudyLevelNameId = new List<int>();
                if (Model.SLName1 == "1")
                    Model.StudyLevelNameId.Add(1);
                if (Model.SLName2 == "1")
                    Model.StudyLevelNameId.Add(2); 
                if (Model.SLName3 == "1")
                    Model.StudyLevelNameId.Add(3);

                List<string> PersonFIOlst = (Model.PersonFIO??"").ToLower().Split(' ').ToList();
                List<string> ScienceDirectorFIOlst = (Model.ScienceDirector ?? "").ToLower().Split(' ').ToList();

                var lst =
                    (from GradBook in context.GraduateBook
                     join Stud in context.Student on GradBook.StudentId equals Stud.Id
                     join SDir in context.ScienceDirector on GradBook.ScienceDirectorId equals SDir.Id
                     join Pers in context.Person on Stud.PersonId equals Pers.Id
                     join SLMask in context.StudyLevelNameMask on Stud.StudyLevelName equals SLMask.StudyLevelName
                     where
                     (Model.StudyLevelNameId.Count > 0 ? Model.StudyLevelNameId.Contains(SLMask.TypeInt) : true)
                     && (!String.IsNullOrEmpty(Model.VKRName) ? GradBook.VKRName.Contains(Model.VKRName) : true)
                     && (!String.IsNullOrEmpty(Model.DirectionName) ? Stud.DirectionName.Contains(Model.DirectionName) : true)
                     select new
                     {
                         Pers.Surname,
                         Pers.Name,
                         Stud.FacultyName,
                         Stud.DirectionName,
                         GradBook.Id,
                         SDirSurname = SDir.Surname,
                         SDirName = SDir.Name,
                         GradBook.VKRName,
                     }).ToList();

                foreach (string s in PersonFIOlst)
                {
                    if (!string.IsNullOrEmpty(s) && s!=" ")
                        lst = lst.Where(x => x.Surname.ToLower().Contains(s) || x.Name.ToLower().Contains(s)).ToList();
                }
                foreach (string s in ScienceDirectorFIOlst)
                {
                    if (!string.IsNullOrEmpty(s) && s != " ")
                        lst = lst.Where(x => x.SDirSurname.ToLower().Contains(s) || x.SDirName.ToLower().Contains(s)).ToList();
                }
                Model.lstGraduates = lst.Select(x => new RepoMainListItem()
                     {
                         FIO = x.Surname + " " + x.Name,
                         Id = x.Id.ToString(),
                         DirectionName = x.DirectionName,
                         VKRName = x.VKRName,
                         ScienceDirector = x.SDirSurname + " " + x.SDirName ,
                     }).ToList();

                Model.DirectionNames =
                    (from Stud in context.Student
                     select new SelectListItem()
                     {
                         Value = Stud.DirectionName,
                         Text = Stud.DirectionName
                     }).Distinct().ToList();
                var l = new SelectListItem() { Value = "", Text = "все" };
                Model.DirectionNames.Add(l);
                Model.DirectionNames = Model.DirectionNames.OrderBy(x =>  x.Value).ToList();
                return View(Model);
            }
        }

        public ActionResult GraduateBook(string id)
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                Guid gId = Guid.Empty;
                if (!Guid.TryParse(id, out gId))
                    return View();

                var Graduate = context.GraduateBook.Where(x => x.Id == gId).FirstOrDefault();

                GraduateBookModel model = new GraduateBookModel();
                model.Annotation = Graduate.VKRAnnotation;
                model.AnnotationEng = Graduate.VKRAnnotationEng;

                model.Name = Graduate.Student.Person.Name;
                model.Reviever = Graduate.Reviewer.Surname + " " + Graduate.Reviewer.Name;
                model.ScienceDirector = Graduate.ScienceDirector.Surname + " " + Graduate.ScienceDirector.Name;

                model.Surname = Graduate.Student.Person.Surname;
                model.VkrName = Graduate.VKRName;
                model.VkrNameEng = Graduate.VKRNameEng;

                model.lstFiles = new List<GraduateBookFileItem>();
                var lstFiles = Graduate.GraduateWorkFile.Select(x => new GraduateBookFileItem() { Id = x.Id.ToString(), FileName = x.FileName, FileSize = x.FileSize }).ToList();
                model.lstFiles.AddRange(lstFiles);

                return View(model);
            }
        }

        //public ActionResult GetFile(string id)
        //{
        //    Guid FileId = new Guid(id);
        //    using (RepoVKREntities context = new RepoVKREntities())
        //    {
        //        var File = context.GraduateWorkFile.Where(x => x.Id == FileId).FirstOrDefault();
        //        if (File == null)
        //            return new HttpStatusCodeResult(404);

        //        Stream fs = Util.GetStreamSQL("FileStorage", "FileData", File.FileStorageId);
        //        string contentType = File.MimeType;

        //        var file = new FileStreamResult(fs, contentType);
        //        file.FileDownloadName = File.FileName;

        //        return file;
        //    }
        //}

        public void GetFile(string id)
        {
            Guid FileId = new Guid(id);
            using (RepoVKREntities context = new RepoVKREntities())
            {
                var File = context.GraduateWorkFile.Where(x => x.Id == FileId).FirstOrDefault();
                if (File == null)
                    return;

                using (Stream fs = Util.GetStreamSQL("FileStorage", "FileData", File.FileStorageId))
                {
                    string contentType = File.MimeType;

                    try
                    {
                        // **************************************************
                        Response.Buffer = false;

                        // Setting the unknown [ContentType]
                        // will display the saving dialog for the user
                        Response.ContentType = "application/octet-stream";

                        // With setting the file name,
                        // in the saving dialog, user will see
                        // the [strFileName] name instead of [download]!
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + File.FileName);

                        long lngFileLength = File.FileSize; //fs.Length;

                        // Notify user (client) the total file length
                        Response.AddHeader("Content-Length", lngFileLength.ToString());
                        // **************************************************

                        // Total bytes that should be read
                        long lngDataToRead = lngFileLength;

                        // Read the bytes of file
                        while (lngDataToRead > 0)
                        {
                            // The below code is just for testing! So we commented it!
                            //System.Threading.Thread.Sleep(200);

                            // Verify that the client is connected or not?
                            if (Response.IsClientConnected)
                            {
                                // 8KB
                                int intBufferSize = 8 * 1024;

                                // Create buffer for reading [intBufferSize] bytes from file
                                byte[] bytBuffers =
                                    new System.Byte[intBufferSize];

                                // Read the data and put it in the buffer.
                                int intTheBytesThatReallyHasBeenReadFromTheStream =
                                    fs.Read(buffer: bytBuffers, offset: 0, count: intBufferSize);

                                // Write the data from buffer to the current output stream.
                                Response.OutputStream.Write
                                    (buffer: bytBuffers, offset: 0,
                                    count: intTheBytesThatReallyHasBeenReadFromTheStream);

                                // Flush (Send) the data to output
                                // (Don't buffer in server's RAM!)
                                Response.Flush();

                                lngDataToRead =
                                    lngDataToRead - intTheBytesThatReallyHasBeenReadFromTheStream;
                            }
                            else
                            {
                                // Prevent infinite loop if user disconnected!
                                lngDataToRead = -1;
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        if (fs != null)
                        {
                            //Close the stream.
                            fs.Close();
                            fs.Dispose();
                        }

                        //Response.Close();
                        this.HttpContext.ApplicationInstance.CompleteRequest();
                    }
                }
            }
        }
    }
}
