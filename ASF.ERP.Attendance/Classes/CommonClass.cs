using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ASF.ERP.Classes
{
    public class CommonClass
    {
        public static bool SendEmailFirst(string sender, string displayNameSender, string senderPassword, string receiver, string displayNameReceiver, string subject, string message, string host, string port)
        {
            try
            {
                int portConverted = 0;
                if (!int.TryParse(port, out portConverted))
                    return false;

                var senderEmail = new MailAddress(sender, displayNameSender);
                var receiverEmail = new MailAddress(receiver, displayNameReceiver);
                var password = senderPassword;
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = portConverted,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static bool SendEmail(string sender, string displayNameSender, string senderPassword, List<MailAddress> receiversEmail, string subject, string message, string host, int port, bool sslEnabled, List<MailAddress> copyTo = null, List<Attachment> attatchmentList = null)
        {
            try
            {

                var senderEmail = new MailAddress(sender, displayNameSender);

                var password = senderPassword;
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = sslEnabled,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };

                var mess = new MailMessage();
                mess.From = senderEmail;
                mess.Subject = subject;
                mess.Body = body;

                if (attatchmentList != null)
                    foreach (var attatchment in attatchmentList)
                    {
                        mess.Attachments.Add(attatchment);
                    }

                if (copyTo != null)
                    foreach (MailAddress copy in copyTo)
                    {
                        mess.CC.Add(copy);
                    }

                foreach (MailAddress rec in receiversEmail)
                {
                    mess.To.Add(rec);
                }

                smtp.Send(mess);

                mess.Attachments.ToList().ForEach(x => x.ContentStream.Dispose());

                if (mess.Attachments != null)
                {
                    for (int i = mess.Attachments.Count - 1; i >= 0; i--)
                    {
                        mess.Attachments[i].Dispose();
                    }
                    mess.Attachments.Clear();
                    mess.Attachments.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static async Task<bool> SendEmailAsync(string sender, string displayNameSender, string senderPassword, string receiver, string displayNameReceiver, string subject, string message, string host, string port, string sslEnabled)
        {
            try
            {
                int portConverted = 0;
                if (!int.TryParse(port, out portConverted))
                    return false;

                bool sslConverted = false;
                bool.TryParse(sslEnabled, out sslConverted);

                var senderEmail = new MailAddress(sender, displayNameSender);
                var receiverEmail = new MailAddress(receiver, displayNameReceiver);
                var password = senderPassword;
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = portConverted,
                    EnableSsl = sslConverted,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    await smtp.SendMailAsync(mess);
                }
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static void SaveFile(string serverPath, string fileId, string fileExtension, HttpPostedFileBase file)
        {
            //Si el directorio no existe lo crea
            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            //Guarda el archivo original de manera temporal
            var path = Path.Combine(serverPath, fileId + "temp" + fileExtension);
            file.SaveAs(path);

            //Encripta el archivo y lo guarda agregandole la cadena '_enc' al nombre
            FileEncrypt fEncrypt = new FileEncrypt();
            string tempFilePath = Path.Combine(serverPath, fileId + "temp" + fileExtension);

            string encryptedFilePath = Path.Combine(serverPath, fileId + fileExtension);
            fEncrypt.Encrypt(tempFilePath, encryptedFilePath);

            //Elimina el archivo temporal que contiene la información desencriptada
            System.IO.File.Delete(tempFilePath);
        }

        public static void SaveFile(string serverPath, string fileId, string fileExtension, byte[] fileBytes)
        {
            //Si el directorio no existe lo crea
            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            //Guarda el archivo original de manera temporal
            var path = Path.Combine(serverPath, fileId + "temp" + fileExtension);
            File.WriteAllBytes(path, fileBytes);

            //Encripta el archivo y lo guarda agregandole la cadena '_enc' al nombre
            FileEncrypt fEncrypt = new FileEncrypt();
            string tempFilePath = Path.Combine(serverPath, fileId + "temp" + fileExtension);

            string encryptedFilePath = Path.Combine(serverPath, fileId + fileExtension);
            fEncrypt.Encrypt(tempFilePath, encryptedFilePath);

            //Elimina el archivo temporal que contiene la información desencriptada
            System.IO.File.Delete(tempFilePath);
        }

        public static bool NotIsNumeric(object o)
        {
            decimal result_ignored;
            return o == null ||
              (o is DBNull) ||
              !decimal.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool DeleteFile(string fileId, string fileName, string serverPath)
        {
            try
            {
                //Elimina el archivo del sistema
                string extension = Path.GetExtension(fileName);
                var path = Path.Combine(serverPath, fileId + extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool IsNumeric(object o)
        {
            decimal result_ignored;
            return o != null &&
              !(o is DBNull) &&
              decimal.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool NotIsInteger(object o)
        {
            int result_ignored;
            return o == null ||
              (o is DBNull) ||
              !int.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool NotIsDecimal(object o)
        {
            decimal result_ignored;
            return o == null ||
              (o is DBNull) ||
              !decimal.TryParse(Convert.ToString(o), out result_ignored);
        }

        public static bool CurpValida(string curp)
        {
            var re = @"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$";
            Regex rx = new Regex(re, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var validado = rx.IsMatch(curp);

            if (!validado)  //Coincide con el formato general?
                return false;

            //Validar que coincida el dígito verificador
            if (!curp.EndsWith(DigitoVerificador(curp.ToUpper())))
                return false;

            return true; //Validado
        }
        private static string DigitoVerificador(string curp17)
        {
            //Fuente https://consultas.curp.gob.mx/CurpSP/
            var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            var suma = 0.0;
            var digito = 0.0;
            for (var i = 0; i < 17; i++)
                suma = suma + diccionario.IndexOf(curp17[i]) * (18 - i);
            digito = 10 - suma % 10;
            if (digito == 10) return "0";
            return digito.ToString();
        }

        static Random rnd = new Random();
        public static string GetRandColor()
        {
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return "#" + hexOutput;
        }
    }

    public class FileBase64
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FileBase64String { get; set; }
        public string Description { get; set; }
    }

    public class ExistingFile
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string Extension
        {
            get
            {
                if (string.IsNullOrEmpty(this.FileName))
                    return string.Empty;

                FileInfo fi = new FileInfo(this.FileName);
                return fi.Extension;
            }
        }
    }

    public class ServerListFilter
    {
        public int ColumnIndex { get; set; }
        public List<FilterData> ListFilter { get; set; }
        public string CurrentFilter { get; set; }

        public ServerListFilter() { }
        public ServerListFilter(int columnIndex, List<FilterData> listFilter, string currentFilter)
        {
            ColumnIndex = columnIndex;
            ListFilter = listFilter;
            CurrentFilter = currentFilter;
        }
    }

    public class FilterData
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}