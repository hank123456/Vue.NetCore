using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Security.Cryptography;

namespace VOL.Core.Utilities
{
    /// <summary>
    /// 文件哈希计算工具类
    /// </summary>
    public static class FileHashHelper
    {
        /// <summary>
        /// 计算IFormFile的SHA256哈希值（十六进制格式）
        /// </summary>
        /// <param name="file">要计算哈希的文件</param>
        /// <returns>十六进制格式的SHA256哈希值</returns>
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
        /// 计算IFormFile的SHA256哈希值（Base64格式）
        /// </summary>
        /// <param name="file">要计算哈希的文件</param>
        /// <returns>Base64格式的SHA256哈希值</returns>
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
        /// 计算流的SHA256哈希值（十六进制格式）
        /// </summary>
        /// <param name="stream">要计算哈希的流</param>
        /// <returns>十六进制格式的SHA256哈希值</returns>
        public static string CalculateStreamHash(Stream stream)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                return Convert.ToHexString(hash).ToLower();
            }
        }

        /// <summary>
        /// 计算文件路径的SHA256哈希值（十六进制格式）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>十六进制格式的SHA256哈希值</returns>
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