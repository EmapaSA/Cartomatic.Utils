using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Cartomatic.Utils.Ftp
{
    public static partial class FtpUtils
    {
        public static IFtpRequestSettings Clone(this IFtpRequestSettings orig)
            => new FtpRequestSettings
            {
                Uri = orig.Uri,
                SubPath = orig.SubPath,
                UserName = orig.UserName,
                Password = orig.Password,
                KeepAlive = orig.KeepAlive,
                Method = orig.Method,
                UseBinary = orig.UseBinary,
                UsePassive = orig.UsePassive,
                EnableSsl = orig.EnableSsl,
                IgnoreInvalidSslCertificate = orig.IgnoreInvalidSslCertificate
            };


        /// <summary>
        /// Executes a configured task with some extra ssl related tweaks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ftpReq"></param>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        private static async Task<T> Execute<T>(this FtpWebRequest ftpReq, IFtpRequestSettings ftpRequestSettings, Func<Task<T>> task)
        {
            var serverCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;

            try
            {
                if (ftpRequestSettings.IgnoreInvalidSslCertificate == true)
                    ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

                return await task();
            }
            finally
            {
                ServicePointManager.ServerCertificateValidationCallback = serverCertificateValidationCallback;
            }
        }


        /// <summary>
        /// Whether or not a connection to an ftp server can be established
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <returns></returns>
        public static async Task<bool> CanConnectAsync(this IFtpRequestSettings ftpRequestSettings)
        {
            var ftpReq = ftpRequestSettings
                .Clone()
                .SetMethod(WebRequestMethods.Ftp.ListDirectory)
                .ConfigureRequest();

            return await ftpReq.Execute<bool>(ftpRequestSettings, async () =>
            {
                using var resp = (FtpWebResponse)await ftpReq.GetResponseAsync();
                //return resp.StatusCode == FtpStatusCode.CommandOK;
                return true;
            });
        }

        /// <summary>
        /// Downloads a file from ftp
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="fName"></param>
        /// <param name="savePath"></param>
        public static async Task<bool> DownloadFileAsync(this IFtpRequestSettings ftpRequestSettings, string fName, string savePath)
        {
            //code below fails if ftp requires SSL...
            //for convenience using a simple file download
            //var client = new WebClient { Credentials = new NetworkCredential(ftpRequestSettings.UserName, ftpRequestSettings.Password) };
            //var uri = ftpRequestSettings.GetEffectiveUri();
            //if (!uri.EndsWith("/"))
            //    uri += "/";
            //for convenience using a simple file download
            //var client = new WebClient { Credentials = new NetworkCredential(ftpRequestSettings.UserName, ftpRequestSettings.Password) };
            //await client.DownloadFileTaskAsync(new Uri($"{uri}{fName}"), savePath);


            if(File.Exists(savePath))
                File.Delete(savePath);

            var ftpReq = ftpRequestSettings
                .Clone()
                .AddSubPath(fName)
                .SetMethod(WebRequestMethods.Ftp.DownloadFile)
                .ConfigureRequest();

            return await ftpReq.Execute(ftpRequestSettings, async () =>
            {
                using var resp = (FtpWebResponse)await ftpReq.GetResponseAsync();

                using var fileStream = File.Open(savePath, FileMode.CreateNew);
                await resp.GetResponseStream().CopyToAsync(fileStream);

                return true;
            });
        }

        /// <summary>
        /// Uploads a file to ftp
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<bool> UploadFileAsync(this IFtpRequestSettings ftpRequestSettings, string filePath)
        {
            var ftpReq = ftpRequestSettings
                .Clone()
                .AddSubPath(Path.GetFileName(filePath))
                .SetMethod(WebRequestMethods.Ftp.UploadFile)
                .ConfigureRequest();

            return await ftpReq.Execute(ftpRequestSettings, async () =>
            {
                using var fStr = File.OpenRead(filePath);
                using var ftpReqStr = await ftpReq.GetRequestStreamAsync();

                //todo: copy in parts and report progress via evt handler
                await fStr.CopyToAsync(ftpReqStr);

                using var resp = (FtpWebResponse)await ftpReq.GetResponseAsync();

                //return resp.StatusCode == FtpStatusCode.OpeningData;
                //looks like if it does not throw upload succeeded
                return true;
            });
            
        }

        /// <summary>
        /// Whether or not a file system entry exists in given ftp context
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static async Task<bool> EntryExistsAsync(this IFtpRequestSettings ftpRequestSettings, string entry)
        {
            var exists = false;

            var ftpReq = ftpRequestSettings
                .Clone()
                .SetMethod(WebRequestMethods.Ftp.ListDirectory)
                .ConfigureRequest();

            return await ftpReq.Execute(ftpRequestSettings, async () =>
            {
                using var resp = (FtpWebResponse)await ftpReq.GetResponseAsync();
                using var str = resp.GetResponseStream();
                using var sr = new StreamReader(str);

                string line;
                while (!string.IsNullOrWhiteSpace(line = await sr.ReadLineAsync()))
                {
                    exists = line == entry;
                    if (exists)
                        break;
                }

                return exists;
            });
            
        }

        /// <summary>
        /// Whether or not a file exists in given ftp context
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="fName"></param>
        /// <returns></returns>
        public static Task<bool> FileExistsAsync(this IFtpRequestSettings ftpRequestSettings, string fName)
            => ftpRequestSettings.EntryExistsAsync(fName);

        /// <summary>
        /// Whether or not a directory exists in given ftp context
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="ftpDir"></param>
        /// <returns></returns>
        public static Task<bool> DirectoryExistsAsync(this IFtpRequestSettings ftpRequestSettings, string ftpDir)
            => ftpRequestSettings.EntryExistsAsync(ftpDir);

        /// <summary>
        /// Creates an ftp directory
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="ftpDir"></param>
        /// <returns></returns>
        public static async Task<bool> CreateDirectoryAsync(this IFtpRequestSettings ftpRequestSettings, string ftpDir)
        {
            var ftpReq = ftpRequestSettings
                .Clone()
                .AddSubPath(ftpDir.EndsWith("/") ? ftpDir : $"{ftpDir}/")
                .SetMethod(WebRequestMethods.Ftp.MakeDirectory)
                .ConfigureRequest();

            return await ftpReq.Execute(ftpRequestSettings, async () =>
            {
                using var resp = (FtpWebResponse)await ftpReq.GetResponseAsync();
                return resp.StatusCode == FtpStatusCode.PathnameCreated;
            });

        }


        /// <summary>
        /// Returns a last modified time for an entry
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public static async Task<DateTime> GetEntryLastModifiedTimeAsync(this IFtpRequestSettings ftpRequestSettings, string entryName)
        {
            var ftpReq = ftpRequestSettings
                .Clone()
                .AddSubPath(entryName)
                .SetMethod(WebRequestMethods.Ftp.GetDateTimestamp)
                .ConfigureRequest();

            return await ftpReq.Execute(ftpRequestSettings, async () =>
            {
                using var resp = (FtpWebResponse)await ftpReq.GetResponseAsync();

                return resp.LastModified;
            });

            
        }


        /// <summary>
        /// returns file last modified time
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="fName"></param>
        /// <returns></returns>
        public static Task<DateTime> GetFileLastModifiedTimeAsync(this IFtpRequestSettings ftpRequestSettings, string fName)
            => ftpRequestSettings.GetEntryLastModifiedTimeAsync(fName);

        /// <summary>
        /// returns directory last modified time
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public static Task<DateTime> GetDirLastModifiedTimeAsync(this IFtpRequestSettings ftpRequestSettings, string dirName)
            => ftpRequestSettings.GetEntryLastModifiedTimeAsync(dirName);


        private static string[] EntriesToSkip = new[] {".", ".."};

        /// <summary>
        /// Gets a list of entries available at given ftp context
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetEntriesAsync(this IFtpRequestSettings ftpRequestSettings)
        {
            var entries = new List<string>();

            var ftpReq = ftpRequestSettings
                .Clone()
                .SetMethod(WebRequestMethods.Ftp.ListDirectory)
                .ConfigureRequest();

            return await ftpReq.Execute(ftpRequestSettings, async () =>
            {
                using var resp = (FtpWebResponse)await ftpReq.GetResponseAsync();
                using var str = resp.GetResponseStream();
                using var sr = new StreamReader(str);

                string line;
                while (!string.IsNullOrWhiteSpace(line = await sr.ReadLineAsync()))
                {
                    if (EntriesToSkip.All(x => x != line))
                        entries.Add(line);
                }

                return entries;
            });
            
        }


        /// <summary>
        /// deletes a file
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <param name="fName"></param>
        /// <returns>Whether or not file deletion succeeded</returns>
        public static async Task<bool> DeleteFileAsync(this IFtpRequestSettings ftpRequestSettings, string fName)
        {
            var ftpReq = ftpRequestSettings
                .Clone()
                .AddSubPath(fName)
                .SetMethod(WebRequestMethods.Ftp.DeleteFile)
                .ConfigureRequest();

            return await ftpReq.Execute(ftpRequestSettings, async () =>
            {
                using var resp = (FtpWebResponse)await ftpReq.GetResponseAsync();
                return true;

            });
        }

        private static IFtpRequestSettings SetUseBinary(this IFtpRequestSettings ftpRequestSettings, bool useBinary)
        {
            ftpRequestSettings.UseBinary = useBinary;
            return ftpRequestSettings;
        }

        private static IFtpRequestSettings SetUsePassive(this IFtpRequestSettings ftpRequestSettings, bool usePassive)
        {
            ftpRequestSettings.UsePassive = usePassive;
            return ftpRequestSettings;
        }

        private static IFtpRequestSettings SetKeepAlive(this IFtpRequestSettings ftpRequestSettings, bool keepAlive)
        {
            ftpRequestSettings.KeepAlive = keepAlive;
            return ftpRequestSettings;
        }

        private static IFtpRequestSettings SetMethod(this IFtpRequestSettings ftpRequestSettings, string method)
        {
            ftpRequestSettings.Method = method;
            return ftpRequestSettings;
        }

        private static IFtpRequestSettings SetSubPath(this IFtpRequestSettings ftpRequestSettings, string subPath)
        {
            ftpRequestSettings.SubPath = subPath;
            return ftpRequestSettings;
        }

        private static IFtpRequestSettings AddSubPath(this IFtpRequestSettings ftpRequestSettings, string subPath)
            => ftpRequestSettings.SetSubPath(
                $"{(string.IsNullOrWhiteSpace(ftpRequestSettings.SubPath) ? string.Empty : $"{ftpRequestSettings.SubPath}/")}{subPath}");

        /// <summary>
        /// Gets an effective URI off ftp request cfg object
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <returns></returns>
        public static string GetEffectiveUri(this IFtpRequestSettings ftpRequestSettings)
            => string.IsNullOrWhiteSpace(ftpRequestSettings.SubPath)
                ? ftpRequestSettings.Uri
                : $"{ftpRequestSettings.Uri}/{ftpRequestSettings.SubPath}";


        private static string[] _secureFtpProtocols = new[] {"ftps", "fteps"};

        /// <summary>
        /// Configures an ftp request object
        /// </summary>
        /// <param name="ftpRequestSettings"></param>
        /// <returns></returns>
        private static FtpWebRequest ConfigureRequest(this IFtpRequestSettings ftpRequestSettings)
        {
            var uri = ftpRequestSettings.GetEffectiveUri();

            var ssl = false;
            foreach (var protocol in _secureFtpProtocols)
            {
                if (uri.StartsWith(protocol))
                {
                    ssl = true;
                    uri = uri.Replace(protocol, "ftp");
                    break;
                }
            }
            //use an explicit setting if required
            ssl = ftpRequestSettings.EnableSsl ?? ssl;

            var ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
            ftpRequest.EnableSsl = ssl;

            ftpRequest.Credentials = new NetworkCredential(ftpRequestSettings.UserName, ftpRequestSettings.Password);

            ftpRequest.Method = ftpRequestSettings.Method;

            ftpRequest.UseBinary = ftpRequestSettings.UseBinary ?? true;
            ftpRequest.UsePassive = ftpRequestSettings.UsePassive ?? true;

            ftpRequest.KeepAlive = ftpRequestSettings.KeepAlive ?? true;

            return ftpRequest;
        }
    }
}
