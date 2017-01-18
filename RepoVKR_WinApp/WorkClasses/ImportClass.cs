using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Xml;
using RepoVKR_WinApp.Classes;

namespace RepoVKR_WinApp
{
    class ImportClass
    {
        public const int percent = 1000;
        private static int totalFiles = 0;
        private static int numFile = 0;
        public static void Import(ref BackgroundWorker bw, string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            List<DirectoryInfo> lstCards = di.GetDirectories().ToList();
            totalFiles = lstCards.Count;
            numFile = 0;

            bw.ReportProgress(0, new BackgroundWorkerStateClass { AllCount = totalFiles, Count = numFile, CurrentCatalog = "", CurrentFIO = "" });

            foreach (DirectoryInfo entCard in lstCards)
                ParseInfo(ref bw, entCard);
        }

        private static void ParseInfo(ref BackgroundWorker bw, DirectoryInfo di)
        {
            List<FileInfo> lstFiles = di.GetFiles().ToList();

            FileInfo xmlData = lstFiles.Where(x => "bbData.xml".Equals(x.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (xmlData != null)
            {
                using (StreamReader sr = xmlData.OpenText())
                {
                    string xml = sr.ReadToEnd();

                    ParseXML(ref bw, xml, lstFiles);

                    sr.Close();
                }
            }
        }

        private static void ParseXML(ref BackgroundWorker bw, string xml, List<FileInfo> lstFiles)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNode node = doc.ChildNodes[0];

            string sPersonPK1 = node["pk1"].InnerText;
            string sPersonST = node["user_id"].InnerText;
            string sPersonSurname = node["lastname"].InnerText;
            string sPersonName = node["firstname"].InnerText;
            string sPersonFIOEng = node["FIOEng"].InnerText;
            string sPersonBirthDate = node["birthday"].InnerText;
            string sPersonEmail = node["email"].InnerText;

            int iPersonPK1 = int.Parse(sPersonPK1);
            DateTime dtPersonBirthDate = DateTime.Parse(sPersonBirthDate);

            numFile++;
            int progressPerc = (int)(((double)numFile * 100d) / (double)totalFiles);
            BackgroundWorkerStateClass bwState = new BackgroundWorkerStateClass()
            {
                AllCount = totalFiles,
                Count = numFile,
                CurrentCatalog = sPersonST,
                CurrentFIO = sPersonSurname + " " + sPersonName
            };

            bw.ReportProgress(progressPerc, bwState);

            Guid PersonId = DatabaseFuncProvider.GetOrInsertPerson(iPersonPK1, sPersonST, sPersonSurname, sPersonName, dtPersonBirthDate, sPersonEmail, sPersonFIOEng, false);

            string sStudentPK1 = node["course_users_pk1"].InnerText;
            string sFacultyPK1 = node["faculty_pk"].InnerText;
            string sFacultyName = node["faculty"].InnerText;
            string sDirectionName = node["speciality"].InnerText;
            string sObrazProgramName = node["study_program"].InnerText;
            string sWorkPlanNumber = node["work_plan"].InnerText;
            string sStudyLevelName = node["degree_name"].InnerText;
            string sStudyFormName = node["study_form"].InnerText;
            string sStudyBasisName = node["study_basis"].InnerText;
            string sStudyNumber = node["student_book_id"].InnerText;

            int iStudentPK1 = int.Parse(sStudentPK1);
            int iFacultyPK1 = int.Parse(sFacultyPK1);

            Guid StudentId = DatabaseFuncProvider.GetOrInsertStudent(iStudentPK1, PersonId, iFacultyPK1, sFacultyName, sDirectionName, sObrazProgramName,
                sWorkPlanNumber, sStudyLevelName, sStudyFormName, sStudyBasisName, sStudyNumber);

            string sScienceDirectorST = node["supervisor_user_id"].InnerText;
            string sScienceDirectorSurname = node["supervisor_lastname"].InnerText;
            string sScienceDirectorName = node["supervisor_firstname"].InnerText;

            Guid ScienceDirectorId = DatabaseFuncProvider.GetOrInsertScienseDirector(sScienceDirectorST, sScienceDirectorSurname, sScienceDirectorName);

            string sReviewerST = node["reviewer_user_id"].InnerText;
            string sReviewerSurname = node["reviewer_lastname"].InnerText;
            string sReviewerName = node["reviewer_firstname"].InnerText;

            Guid ReviewerId = DatabaseFuncProvider.GetOrInsertReviewer(sReviewerST, sReviewerSurname, sReviewerName);

            string sVKRName = node["graduate_VKR"].InnerText;
            string sVKRNameEng = node["graduate_VKREng"].InnerText;
            string sScienceDirectorInfo = node["supervisor_ru_info"].InnerText;
            string sReviewerInfo = node["reviewer_ru_info"].InnerText;
            string sVKRAnnotation = node["ru_annotation"].InnerText;
            string sVKRAnnotationEng = node["en_annotation"].InnerText;

            Guid GraduateBookId = DatabaseFuncProvider.GetOrInsertGraduateBook(StudentId, ScienceDirectorId, ReviewerId, 
                sVKRName, sVKRNameEng, sScienceDirectorInfo, sReviewerInfo, sVKRAnnotation, sVKRAnnotationEng);

            string sVKRFileName = node["file_name"].InnerText;

            DatabaseFuncProvider.GetOrInsertGraduateBookFile(GraduateBookId, lstFiles, sVKRFileName, ref bw, bwState, progressPerc);
        }

        public static async void TestFile()
        {
            using (RepoVKREntities context = new RepoVKREntities())
            {
                int iMaxFileSize = context.GraduateWorkFile.AsNoTracking().Select(x => x.FileSize).Max();
                var File = context.GraduateWorkFile.Where(x => x.FileSize == iMaxFileSize).Select(x => new { x.FileStorageId, x.FileName, x.FileExtention }).First();

                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.FileName = File.FileName;
                var dr = sfd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    SQLVarbinaryStreamClass sql = new SQLVarbinaryStreamClass(context.Database.Connection.ConnectionString, "FileStorage", "FileData", "Id");
                    await sql.CopyBinaryValueToFile(sfd.FileName, File.FileStorageId);
                }
            }
        }
    }
}
