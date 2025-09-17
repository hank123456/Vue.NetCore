using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Security.Cryptography;

namespace VOL.Core.Utilities
{
    /// <summary>
    /// �ļ���ϣ���㹤����
    /// </summary>
    public static class FileHashHelper
    {
        /// <summary>
        /// ����IFormFile��SHA256��ϣֵ��ʮ�����Ƹ�ʽ��
        /// </summary>
        /// <param name="file">Ҫ�����ϣ���ļ�</param>
        /// <returns>ʮ�����Ƹ�ʽ��SHA256��ϣֵ</returns>
        public static string CalculateFileHash(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                return Convert.ToHexString(hash).ToLower();
            }
        }

        /// <summary>
        /// ����IFormFile��SHA256��ϣֵ��Base64��ʽ��
        /// </summary>
        /// <param name="file">Ҫ�����ϣ���ļ�</param>
        /// <returns>Base64��ʽ��SHA256��ϣֵ</returns>
        public static string CalculateFileHashBase64(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// ��������SHA256��ϣֵ��ʮ�����Ƹ�ʽ��
        /// </summary>
        /// <param name="stream">Ҫ�����ϣ����</param>
        /// <returns>ʮ�����Ƹ�ʽ��SHA256��ϣֵ</returns>
        public static string CalculateStreamHash(Stream stream)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                return Convert.ToHexString(hash).ToLower();
            }
        }

        /// <summary>
        /// �����ļ�·����SHA256��ϣֵ��ʮ�����Ƹ�ʽ��
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns>ʮ�����Ƹ�ʽ��SHA256��ϣֵ</returns>
        public static string CalculateFilePathHash(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                return Convert.ToHexString(hash).ToLower();
            }
        }
    }
}