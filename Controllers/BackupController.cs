using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;

namespace CyComputer.Controllers
{
    public class BackupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GenerateBackup(string fileName, string format)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "CyComputer";
            }
            if (format != "bak" && format != "sql")
            {
                format = "bak";
            }

            string backupPath = $@"C:\Backup\{fileName}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.{format}";
            string command = string.Empty;

            if (format == "bak")
            {
                command = $"sqlcmd -S DESKTOP-P3DJB58\\SQLEXPRESS -Q \"BACKUP DATABASE [CyComputer] TO DISK='{backupPath}'\"";
            }
            else if (format == "sql")
            {
                command = $"sqlcmd -S DESKTOP-P3DJB58\\SQLEXPRESS -d CyComputer -Q \"DECLARE @path NVARCHAR(4000); SET @path='{backupPath}'; BACKUP DATABASE [CyComputer] TO DISK=@path\"";
            }

            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;
                processInfo.UseShellExecute = false;
                processInfo.CreateNoWindow = true;

                using (Process process = Process.Start(processInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0 && System.IO.File.Exists(backupPath))
                    {
                        var fileBytes = System.IO.File.ReadAllBytes(backupPath);
                        return File(fileBytes, "application/octet-stream", Path.GetFileName(backupPath));
                    }
                    else
                    {
                        ViewBag.Message = "Error al generar el backup: " + error;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View("Index");
        }
        [HttpPost]
        public IActionResult RestoreDatabase()
        {
            var file = Request.Form.Files["archivoBackup"];
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Por favor, seleccione un archivo de backup.";
                return View("Index");
            }

            string restorePath = Path.Combine(@"C:\Usuarios\Yohan\Descargas", Path.GetFileName(file.FileName));

            try
            {
                using (var stream = new FileStream(restorePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Comando para restaurar la base de datos
                string command = $"sqlcmd -S DESKTOP-P3DJB58\\SQLEXPRESS -d master -Q \"RESTORE DATABASE [CyComputer] FROM DISK='{restorePath}' WITH REPLACE\"";

                ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(processInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        ViewBag.Message = "Base de datos restaurada exitosamente.";
                    }
                    else
                    {
                        ViewBag.Message = "Error al restaurar la base de datos: " + error;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View("Index");
        }
    }
}
