using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOL.Core.Utilities;
using VOL.Entity;

namespace VOL.Core.Services
{
    /// <summary>
    /// 文件存储服务接口
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="folder">文件夹</param>
        /// <returns>返回文件访问路径</returns>
        Task<WebResponseContent> UploadAsync(IFormFile file, string folder = null);

        /// <summary>
        /// 批量上传文件（同步）
        /// </summary>
        /// <param name="files">文件列表</param>
        /// <param name="folder">文件夹</param>
        /// <returns>返回上传结果</returns>
        WebResponseContent UploadFiles(List<IFormFile> files, string folder = null);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string filePath);

        /// <summary>
        /// 获取文件访问URL
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        string GetFileUrl(string filePath);

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string filePath);
    }
}