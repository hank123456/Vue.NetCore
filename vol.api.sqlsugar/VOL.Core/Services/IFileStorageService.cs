using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOL.Core.Utilities;
using VOL.Entity;

namespace VOL.Core.Services
{
    /// <summary>
    /// �ļ��洢����ӿ�
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="file">�ļ�</param>
        /// <param name="folder">�ļ���</param>
        /// <returns>�����ļ�����·��</returns>
        Task<WebResponseContent> UploadAsync(IFormFile file, string folder = null);

        /// <summary>
        /// �����ϴ��ļ���ͬ����
        /// </summary>
        /// <param name="files">�ļ��б�</param>
        /// <param name="folder">�ļ���</param>
        /// <returns>�����ϴ����</returns>
        WebResponseContent UploadFiles(List<IFormFile> files, string folder = null);

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string filePath);

        /// <summary>
        /// ��ȡ�ļ�����URL
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns></returns>
        string GetFileUrl(string filePath);

        /// <summary>
        /// ����ļ��Ƿ����
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string filePath);
    }
}