using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepoVKR_WinApp.Classes
{
    public static class DatabaseFuncProvider
    {
        public static Guid GetOrInsertScienseDirector(string ST, string Surname, string Name)
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                if (string.IsNullOrEmpty(ST))
                {
                    var z = context.ScienceDirector.Where(x => x.Surname == Surname && x.Name == Name);
                    if (z.Count() == 1)
                        return z.First().Id;
                }
                else
                {
                    var z = context.ScienceDirector.Where(x => x.ST == ST);
                    if (z.Count() == 1)
                        return z.First().Id;
                }

                //Если мы дошли до сюда, то ID не найден, создаём самостоятельно
                Guid Id = Guid.NewGuid();
                ScienceDirector s = new ScienceDirector();
                s.Id = Id;
                s.ST = ST;
                s.Surname = Surname;
                s.Name = Name;

                context.ScienceDirector.Add(s);
                context.SaveChanges();

                return Id;
            }
        }

        public static Guid GetOrInsertReviewer(string ST, string Surname, string Name)
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                if (string.IsNullOrEmpty(ST))
                {
                    var z = context.Reviewer.Where(x => x.Surname == Surname && x.Name == Name);
                    if (z.Count() == 1)
                        return z.First().Id;
                }
                else
                {
                    var z = context.Reviewer.Where(x => x.ST == ST);
                    if (z.Count() == 1)
                        return z.First().Id;
                }

                //Если мы дошли до сюда, то ID не найден, создаём самостоятельно
                Guid Id = Guid.NewGuid();
                Reviewer s = new Reviewer();
                s.Id = Id;
                s.ST = ST;
                s.Surname = Surname;
                s.Name = Name;

                context.Reviewer.Add(s);
                context.SaveChanges();

                return Id;
            }
        }

        public static Guid GetOrInsertPerson(int PK1, string ST, string Surname, string Name, DateTime BirthDate, string Email, string FIOEng, bool updateValues = true)
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                Person pers = null;
                bool bInsert = false;
                var z = context.Person.Where(x => x.PK1 == PK1);
                if (z.Count() == 1)
                {
                    pers = z.First();
                    if (!updateValues)
                        return pers.Id;
                }
                else
                {
                    pers = new Person();
                    pers.Id = Guid.NewGuid();
                    pers.PK1 = PK1;
                    bInsert = true;
                }

                pers.ST = ST;
                pers.Surname = Surname;
                pers.Name = Name;
                pers.Email = Email;
                pers.BirthDate = BirthDate;
                pers.FIOEng = FIOEng;

                if (bInsert)
                    context.Person.Add(pers);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return pers.Id;
            }
        }

        public static Guid GetOrInsertStudent(int PK1, Guid PersonId, int FacultyPK1, string FacultyName, string DirectionName, string ObrazProgramName,
            string WorkPlanNumber, string StudyLevelName, string StudyFormName, string StudyBasisName, string StudyNumber, bool updateValues = true)
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                Student stud = null;
                bool bInsert = false;
                var z = context.Student.Where(x => x.PK1 == PK1 && x.PersonId == PersonId);
                if (z.Count() == 1)
                {
                    stud = z.First();
                    if (!updateValues)
                        return stud.Id;
                }
                else
                {
                    stud = new Student();
                    stud.Id = Guid.NewGuid();
                    stud.PK1 = PK1;
                    stud.PersonId = PersonId;
                    bInsert = true;
                }

                stud.FacultyPK1 = FacultyPK1;
                stud.FacultyName = FacultyName;
                stud.DirectionName = DirectionName;
                stud.ObrazProgramName = ObrazProgramName;
                stud.WorkPlanNumber = WorkPlanNumber;
                stud.StudyLevelName = StudyLevelName;
                stud.StudyFormName = StudyFormName;
                stud.StudyBasisName = StudyBasisName;
                stud.StudyNumber = StudyNumber;

                if (bInsert)
                    context.Student.Add(stud);

                context.SaveChanges();

                return stud.Id;
            }
        }

        public static Guid GetOrInsertGraduateBook(Guid StudentId, Guid ScienceDirectorId, Guid ReviewerId, string VKRName, string VKRNameEng,
            string ScienceDirectorInfo, string ReviewerInfo, string VKRAnnotation, string VKRAnnotationEng, bool updateValues = true)
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                GraduateBook gradBook = null;
                bool bInsert = false;
                var z = context.GraduateBook.Where(x => x.StudentId == StudentId);
                if (z.Count() == 1)
                {
                    gradBook = z.First();
                    if (!updateValues)
                        return gradBook.Id;
                }
                else
                {
                    gradBook = new GraduateBook();
                    gradBook.Id = Guid.NewGuid();
                    gradBook.StudentId = StudentId;
                    bInsert = true;
                }

                gradBook.ScienceDirectorId = ScienceDirectorId;
                gradBook.ReviewerId = ReviewerId;
                gradBook.VKRName = VKRName;
                gradBook.VKRNameEng = VKRNameEng;
                gradBook.ScienceDirectorInfo = ScienceDirectorInfo;
                gradBook.ReviewerInfo = ReviewerInfo;
                gradBook.VKRAnnotation = VKRAnnotation;
                gradBook.VKRAnnotationEng = VKRAnnotationEng;

                if (bInsert)
                    context.GraduateBook.Add(gradBook);

                context.SaveChanges();

                return gradBook.Id;
            }
        }

        public static void GetOrInsertGraduateBookFile(Guid GraduateBookId, List<FileInfo> lstFI, string sVKRFileName, ref BackgroundWorker bw, BackgroundWorkerStateClass bwState, int progressPerc)
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                var lst = context.GraduateWorkFile.Where(x => x.GraduateBookId == GraduateBookId).Select(x => new { x.Id, x.FileName, x.FileStorageId }).ToList();

                List<FileInfo> lstToAdd = new List<FileInfo>();
                SQLVarbinaryStreamClass sql = new SQLVarbinaryStreamClass(context.Database.Connection.ConnectionString, "FileStorage", "FileData", "Id");
                CancellationToken token = new CancellationToken();
                foreach (var GradFile in lst.ToList())
                {
                    if (lstFI.Where(x => x.Name == GradFile.FileName).Count() > 0)
                    {
                        var f = context.GraduateWorkFile.Where(x => x.Id == GradFile.Id).FirstOrDefault();
                        if (f != null)
                        {
                            context.GraduateWorkFile.Remove(f);
                            context.SaveChanges();
                        }

                        sql.DeleteBLOBFromServer(GradFile.FileStorageId, token).Wait();

                        lst.Remove(GradFile);
                    }
                }

                foreach (FileInfo fi in lstFI)
                {
                    if (lst.Where(x => x.FileName == fi.Name).Count() == 0)
                        lstToAdd.Add(fi);
                }

                List<string> lstFileNamesToExcept = new List<string>() 
                { 
                    "bbData.xml",
                    "collections",
                    "contents",
                    "dublin_core.xml",
                    "license.txt"
                };

                foreach (FileInfo fi in lstToAdd)
                {
                    if (lstFileNamesToExcept.Contains(fi.Name))
                        continue;

                    bool IsMainFile = fi.Name.Equals(sVKRFileName, StringComparison.InvariantCultureIgnoreCase);

                    using (FileStream fs = fi.OpenRead())
                    {
                        if (fs.Length > int.MaxValue)
                            return;

                        if (fs.Length > 1024 * 1024 * 5) //(5MB)
                        {
                            bwState.CurrentBigFile = fi.Name + " (" + Math.Round((double)fs.Length / (1024d * 1024d), 2) + ") MB";
                            bw.ReportProgress(progressPerc, bwState);
                        }

                        Guid FileStorageId = Guid.NewGuid();

                        Task t = sql.InsertStreamBLOBToServer(fs, FileStorageId, token);
                        t.Wait();

                        GraduateWorkFile file = new GraduateWorkFile();
                        file.Id = Guid.NewGuid();
                        file.FileStorageId = FileStorageId;
                        file.GraduateBookId = GraduateBookId;
                        file.IsMainFile = IsMainFile;
                        file.FileName = fi.Name;
                        file.FileExtention = fi.Extension;
                        file.MimeType = Util.GetMimeFromExtention(fi.Extension);
                        file.FileSize = (int)fs.Length;
                        
                        file.FileHash = Util.HashSHA1(fs);

                        try
                        {
                            context.GraduateWorkFile.Add(file);
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }
    }
}
