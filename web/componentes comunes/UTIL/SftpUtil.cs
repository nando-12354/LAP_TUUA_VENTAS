using Renci.SshNet;
using Renci.SshNet;
using System;
using System.IO;

namespace LAP.TUUA.UTIL
{
    public class SftpUtil
    {
        private readonly string _sftpHost;
        private readonly int _sftpPort;
        private readonly string _sftpUser;
        private readonly string _sftpPassword;
        private readonly string _sftpFolder;

        public SftpUtil(string sftpHost, int sftpPort, string sftpUser, string sftpPassword, string sftpFolder)
        {
            _sftpHost = sftpHost;
            _sftpPort = sftpPort;
            _sftpUser = sftpUser;
            _sftpPassword = sftpPassword;
            _sftpFolder = sftpFolder;
        }

        public void EnviarArchivo(byte[] archivo, string nombreArchivo)
        {
            using (var client = new SftpClient(_sftpHost, _sftpPort, _sftpUser, _sftpPassword))
            {
                client.Connect();
                if (!client.IsConnected)
                {
                    throw new Exception("No se pudo conectar al servidor SFTP");
                }

                using (var ms = new MemoryStream(archivo))
                {
                    client.BufferSize = (uint)ms.Length;
                    client.UploadFile(ms, Path.Combine(_sftpFolder, nombreArchivo));
                }

            }
        }

        public byte[] DescargarArchivo(string nombreArchivo)
        {
            byte[] file = null;
            using (var client = new SftpClient(_sftpHost, _sftpPort, _sftpUser, _sftpPassword))
            {
                client.Connect();
                if (!client.IsConnected)
                {
                    throw new Exception("No se pudo conectar al servidor SFTP");
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    client.DownloadFile(Path.Combine(_sftpFolder, nombreArchivo), stream);

                    file = stream.ToArray();
                }

            }

            return file;
        }


    }
}
