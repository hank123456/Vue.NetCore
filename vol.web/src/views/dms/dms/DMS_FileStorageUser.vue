<!--
 *Author：jxx
 *Contact：xx
 *业务请在@/extension/dms/dms/DMS_FileStorage.jsx或DMS_FileStorage.vue文件编写
 *新版本支持vue或【表.jsx]文件编写业务,文档见:https://v3.volcore.xyz/docs/view-grid、https://v3.volcore.xyz/docs/web
 -->
<template>
    <view-grid ref="grid"
               :columns="columns"
               :detail="detail"
               :details="details"
               :editFormFields="editFormFields"
               :editFormOptions="editFormOptions"
               :searchFormFields="searchFormFields"
               :searchFormOptions="searchFormOptions"
               :table="table"
               :extend="extend"
               :onInit="onInit"
               :onInited="onInited"
               :searchBefore="searchBefore"
               :searchAfter="searchAfter"
               :addBefore="addBefore"
               :updateBefore="updateBefore"
               :rowClick="rowClick"
               :modelOpenBefore="modelOpenBefore"
               :modelOpenAfter="modelOpenAfter"
               :spanMethod="spanMethod"
               >
        <!-- 自定义组件数据槽扩展，更多数据槽slot见文档 -->
        <template #gridHeader>
        </template>
    </view-grid>
    <!-- 下载方式弹窗 -->
    <el-dialog
        v-model="downloadDialogVisible"
        width="400px"
        destroy-on-close
        center
    >
        <template #header>
            <div class="my-title">
            <el-icon :size="24" color="var(--el-color-primary)">
                <Download />
            </el-icon>
            <span>选择下载方式</span>
            </div>
        </template>
        <div class="btn-box">
            <el-button
                type="primary"
                class="full-btn"
                @click="confirmDownload('original')"
            >
            原始文件名下载
            </el-button>

            <el-button
                type="success"
                class="full-btn"
                @click="confirmDownload('file')"
            >
            文件下载
            </el-button>

            <el-button
                type="warning"
                class="full-btn"
                @click="confirmDownload('versioned')"
            >
            文件名_版本信息下载
            </el-button>
        </div>
        <template #footer>
            <el-button text @click="downloadDialogVisible = false">关 闭</el-button>
        </template>
    </el-dialog>

    <!-- 历史版本弹窗 -->
    <el-dialog
        v-model="historyDialogVisible"
        width="800px"
        destroy-on-close
        center
    >
        <template #header>
            <div class="my-title">
                <el-icon :size="24" color="var(--el-color-warning)">
                    <Clock />
                </el-icon>
                <span>历史版本</span>
            </div>
        </template>
        <div v-if="historyVersions.length === 0" class="no-history">
            <el-empty description="当前是唯一版本，没有历史版本" />
        </div>
        <div v-else>
            <el-table :data="historyVersions" style="width: 100%">
                <el-table-column label="文件名" width="320" >
                    <template #default="{ row }">
                        {{ row.fileName }}
                    </template>
                </el-table-column>
                <el-table-column label="版本号" width="100">
                    <template #default="{ row }">
                        {{ row.version }}
                    </template>
                </el-table-column>
                <el-table-column prop="FileSize" label="文件大小" width="100">
                    <template #default="{ row }">
                        {{ formatFileSize(row.fileSize) }} KB
                    </template>
                </el-table-column>
                <el-table-column prop="CreateDate" label="创建时间" width="160">
                    <template #default="{ row }">
                        {{ row.createDate }}
                    </template>
                </el-table-column>
                <el-table-column label="操作" width="120">
                    <template #default="{ row }">
                        <el-button
                            type="primary"
                            size="small"
                            @click="downloadHistoryFile(row)"
                        >
                            下载
                        </el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <template #footer>
            <el-button @click="historyDialogVisible = false">关 闭</el-button>
        </template>
    </el-dialog>
</template>

<script setup lang="jsx">
    import extend from "@/extension/dms/dms/DMS_FileStorage.jsx";
    import viewOptions from './DMS_FileStorage/optionsUser.js'
    import { ref, reactive, getCurrentInstance, watch, onMounted } from "vue";
    const grid = ref(null);
    const { proxy } = getCurrentInstance()
    const downloadDialogVisible = ref(false);
    const downloadRow = ref(null);

    const historyDialogVisible = ref(false);
    const historyVersions = ref([]);

    //http请求，proxy.http.post/get
    const { table, editFormFields, editFormOptions, searchFormFields, searchFormOptions, columns, detail, details } = reactive(viewOptions())

    let gridRef;//对应[表.jsx]文件中this.使用方式一样

    //生成对象属性初始化
    const onInit = async ($vm) => {
        gridRef = $vm;
        gridRef.single = false;//单选
        gridRef.ck = true;//不显示复选框
        //与jsx中的this.xx使用一样，只需将this.xx改为gridRef.xx
        //更多属性见：https://v3.volcore.xyz/docs/view-grid
        gridRef.buttons = gridRef.buttons.filter(
            (btn) => ![ '新建','编辑','删除', 'Add', 'Edit', 'Delete'].includes(btn.name));//'新建','编辑','删除'
        gridRef.buttons.splice(3, 0, {
            name: '新文件', //名称
            icon: 'el-icon-upload', //图标
            type: 'primary',
            onClick: () => {
                chooseFiles(); // 打开新建窗口
            }
        });

        columns.push({
            title: '操作',
            field: '操作',
            width: 250,
            align: 'center', // 'left',
            fixed:'right',//固定到最右边，也可以设置为left固定到左边
            render: (h, { row, column, index }) => {
                return (
                <div>
                    <el-button
                    onClick={($e) => {
                        DownloadBtnClick(row, column, index, $e);
                    }}
                    type="primary" plain   style="height:26px; padding: 10px !important;" >
                    下载
                    </el-button>

                    <el-button
                    onClick={($e) => {
                        btn2Click(row, $e);
                    }}
                    type="success"  plain  style="height:26px;padding: 10px !important;" >
                    更新
                    </el-button>

                    {/* <el-button
                    onClick={($e) => {
                        btn2Click(row, $e);
                    }}
                    v-show={index % 2 === 0}
                    type="warning" plain  style="height:26px;padding: 10px !important;" >
                    设置
                    </el-button> */}

                    <el-dropdown
                    trigger="click"
                    v-slots={{
                        dropdown: () => (
                        <el-dropdown-menu>
                            <el-dropdown-item>
                            <div
                                onClick={($e) => {
                                dropdownClick('历史版本', row,column,index,$e);
                                }}
                            >
                                历史版本
                            </div>
                            </el-dropdown-item>
                            
                            <el-dropdown-item style={{ '--el-dropdown-menuItem-hover-fill': 'var(--el-color-danger-light-9)' }}>
                            <div
                                onClick={($e) => {
                                dropdownClick('删除', row,column,index,$e);
                                }}
                            >
                                删除文件
                            </div>
                            </el-dropdown-item>
                        </el-dropdown-menu>
                        )
                    }}
                    >
                        <span style="font-size: 13px;color: #409eff;margin: 5px 0 0 10px;" class="el-dropdown-link">
                            更多<i class="el-icon-arrow-right"></i>
                        </span>
                    </el-dropdown>
                </div>
                );
            }
        });
    }
    const chooseFiles = ()=>{
        const input = document.createElement('input');
        input.type = 'file';
        input.multiple = false;
        input.onchange = (e) => {
            const files = e.target.files;
            if (!files || files.length === 0) {
                return; 
            }
            // 存储选择的文件
            gridRef.selectedFiles = Array.from(files);
            // console.log('Selected files after select File:', gridRef.selectedFiles);
            gridRef.add(); // 打开新建窗口
            input.value = '';
        };
        input.click();
    }

    const dropdownClick = async (name, row, column, index, $e) => {
        if (name === '删除') {
            try {
                await proxy.$confirm('确认删除该文件？', '提示', { 
                    type: 'warning',
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    autofocus: '取消'
                });
                //下面这断代码是可用的，但是写了单独的后台接口，后面部分是用的通用update接口
                // const updateData  = {
                //     Id: row.Id,
                //     Enable: 0
                // };
                // const res = await proxy.http.post('/api/DMS_FileStorage/deletFileStorageItem', updateData, true);
                // if (res) {
                //     proxy.$message.success('删除成功');
                //     row.Enable = 0;
                //     // 重新加载
                //     await gridRef.search();
                // } else {
                //     proxy.$message.error(res.message || '删除失败');
                // }

                //下面使用通用update接口
                const updateStatus = {   
                    MainData: {
                        Id: row.Id,
                        FileGroupId: row.FileGroupId,
                        FileName: row.FileName,
                        Extension: row.Extension,
                        Hash: row.Hash,
                        FileSize: row.FileSize,
                        FileType: row.FileType,
                        Major: row.Major,
                        Minor: row.Minor,
                        Revision: row.Revision,
                        MinioBucket: row.MinioBucket,
                        MinioObject: row.MinioObject,
                        IsCurrentVersion: row.IsCurrentVersion,
                        Enable: 0  
                    }  
                };
                const res = await proxy.http.post("api/DMS_FileStorage/update", updateStatus, true);
                if(res && res.status){
                    proxy.$message.success("操作删除成功"); 
                    // 重新加载 
                    await gridRef.search();  
                } else {
                    proxy.$message.error(res.message || "操作删除失败");
                }
            } catch (err) {
                // 取消或异常
                // proxy.$message.info("操作删除已取消");  
            }
            return;
        }
        if (name === '历史版本') {
            // 打开历史版本弹窗
            // proxy.$message.info('历史版本功能待实现');
            await showHistoryVersions(row);
        }
    }

    const DownloadBtnClick = (row, column, index, $e) => {
        if (!row.MinioBucket || !row.MinioObject) {
            proxy.$message.error('文件信息不完整，无法下载');
            return;
        }
        downloadRow.value = row; // 保存当前行数据
        downloadDialogVisible.value = true; // 打开下载方式选择弹窗
    };

    // 显示历史版本
    const showHistoryVersions = async (row) => {
        try {
            const queryHistoryData  = {
                FileGroupId: row.FileGroupId,
            };

            const result = await proxy.http.post('/api/DMS_FileStorage/getHistorys', queryHistoryData, true);

            if (result && result.status) {
                historyVersions.value = result.data.rows || [];
                historyDialogVisible.value = true;
                console.log("historyVersions.value:", historyVersions.value);
                if (historyVersions.value.length === 0) {
                    proxy.$message.info('当前是唯一版本，没有历史版本');
                }
            } else {
                proxy.$message.error(result.message || '查询历史版本失败');
            }
        } catch (error) {
            console.error('查询历史版本错误:', error);
            proxy.$message.error('查询历史版本失败，请重试');
        }
    };
    // 将字节转换为KB的方法
    const formatFileSize = (bytes) => {
        if (!bytes || bytes === 0) return '0';
        return Math.round(bytes / 1024 * 100) / 100; // 保留两位小数
    };
    const downloadHistoryFile = async (row) => {
        //console.log("row value = " , row)
        if (!row.minioBucket || !row.minioObject) {
            proxy.$message.error('文件信息不完整，无法下载');
            return;
        }
        
        try {
            // 构建文件名（历史版本使用带版本号的文件名）
            const ext = row.extension || '';
            const version = row.version;
            const hash = row.hash;
            const fileName = `${row.fileName || 'file'}_${version}_${hash}.${ext}`;
            // 构建下载URL
            const downloadData = {
                MinioBucket: row.minioBucket,
                MinioObject: row.minioObject,
            };
            
            const downloadResult = await proxy.http.post('/api/DMS_FileStorage/download', downloadData, true);
            const downloadUrl = downloadResult.data.url;
            //console.log('历史版本下载链接:', downloadUrl);
            
            if (downloadResult && !downloadResult.status) {
                proxy.$message.error(downloadResult.message || '下载服务异常');
                return;
            } else {
                // 先通过 fetch 把 MinIO 文件拉成 Blob（浏览器会走一次 OPTIONS，MinIO 需允许 CORS）
                const res = await fetch(downloadUrl, { method: 'GET', mode: 'cors' });
                const blob = await res.blob();
                // 生成一个同源 blob: URL
                const blobUrl = URL.createObjectURL(blob);
                // 创建下载链接
                const link = document.createElement('a');
                link.href = blobUrl;
                link.download = fileName;
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
                URL.revokeObjectURL(blobUrl);   // 内存回收
                
                proxy.$message.success('开始下载历史文件：' + fileName);
            }
        } catch (error) {
            console.error('下载历史文件失败:', error);
            proxy.$message.error('下载历史文件失败：' + error.message );
        }
    };   

    const confirmDownload = async (mode) => {
        if (!downloadRow.value) return;
        
        const row = downloadRow.value;
        const ext = row.Extension || '';
        let fileName = '';
        
        switch (mode) {
            case 'original':
                fileName = `${row.OriginalFileName || row.FileName || 'file'}.${ext}`;
                break;
            case 'file':
                fileName = `${row.FileName || 'file'}.${ext}`;
                break;
            case 'versioned':
                const version = `v${row.Major || 0}.${row.Minor || 0}.${row.Revision || 0}`;
                fileName = `${row.FileName || 'file'}_${version}.${ext}`;
                break;
            default:
                fileName = `${row.FileName || 'file'}.${ext}`;
        }
        console.log('Downloading file with name:', fileName);
        try {
            // 构建下载URL
            const downloadData  = {
                MinioBucket: row.MinioBucket,
                MinioObject: row.MinioObject,
                };
            const downloadResult = await proxy.http.post('/api/DMS_FileStorage/download', downloadData,true);
            const downloadUrl = downloadResult.data.url;
            console.log('下载链接:', downloadUrl);
            
            if(downloadResult && !downloadResult.status){
                proxy.$message.error(downloadResult.message || '下载服务异常');
                return;
            }else{
                // 先通过 fetch 把 MinIO 文件拉成 Blob（浏览器会走一次 OPTIONS，MinIO 需允许 CORS）
                const res = await fetch(downloadUrl, { method: 'GET', mode: 'cors' });
                const blob = await res.blob();
                // 生成一个同源 blob: URL
                const blobUrl = URL.createObjectURL(blob);
                // 创建下载链接
                const link = document.createElement('a');
                link.href = blobUrl;
                link.download = fileName;
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
                URL.revokeObjectURL(blobUrl);   // 内存回收
                
                proxy.$message.success('开始下载文件：'+fileName);
                downloadDialogVisible.value = false;
            }   
        } catch (error) {
            console.error('下载失败:', error);
            proxy.$message.error('下载失败：' + error.message );    
        }
    };
    //生成对象属性初始化后,操作明细表配置用到
    const onInited = async () => {
    }
    const searchBefore = async (param) => {
        //界面查询前,可以给param.wheres添加查询参数
        //返回false，则不会执行查询
        return true;
    }
    const searchAfter = async (rows, result) => {
        return true;
    }
    const addBefore = async (formData) => {
        //新建保存前formData为对象，包括明细表，可以给给表单设置值，自己输出看formData的值
        if(gridRef.currentAction === 'Add'){
            // 如果有选择的文件，先上传文件
            if (gridRef.selectedFiles && gridRef.selectedFiles.length > 0) {
                const file = gridRef.selectedFiles[0];
                try {
                    // 创建FormData对象用于文件上传
                    const uploadFormData = new FormData();
                    uploadFormData.append('fileInput', file);
                    
                    // 上传文件到服务器
                    const uploadResult = await proxy.http.post('/api/DMS_FileStorage/upload', uploadFormData,true, {
                        headers: {
                            'Content-Type': 'multipart/form-data'
                        }
                    });
                    console.log('uploadResult.data:', uploadResult.data);
                    // 将上传结果的文件路径等信息添加到表单数据中
                    if (uploadResult.status) {
                        formData.mainData.MinioBucket = uploadResult.data[0].minioBucket;
                        formData.mainData.MinioObject = uploadResult.data[0].minioObject;
                        formData.mainData.Hash = uploadResult.data[0].hash;
                        // 清除选择的文件
                        gridRef.selectedFiles = [];
                    } else {
                        proxy.$message.error('文件上传失败：' + uploadResult.message);
                        return false; // 阻止保存
                    }
                } catch (error) {
                    console.error('文件上传错误:', error);
                    proxy.$message.error('文件上传失败，请重试');
                    return false; // 阻止保存
                }
            }
        }
        return true;
    }
    const updateBefore = async (formData) => {
        //编辑保存前formData为对象，包括明细表、删除行的Id
        return true;
    }
    const rowClick = ({ row, column, event }) => {
        //查询界面点击行事件
        //grid.value.toggleRowSelection(row); //单击行时选中当前行;
    }
    const modelOpenBefore = async (row) => {//弹出框打开后方法
        return true;//返回false，不会打开弹出框
    }
    const modelOpenAfter = (row) => {
        console.log("modelOpenAfter selectedFiles ==",gridRef.selectedFiles);
        console.log("editFormFields ==",editFormFields);
        if (gridRef.currentAction === 'Add' && gridRef.selectedFiles.length > 0) {  
            const file = gridRef.selectedFiles[0];
            // 初始化版本控制字段  
            editFormFields.FileName = file.name.split('.').slice(0, -1).join('.');
            editFormFields.OriginalFileName = file.name.split('.').slice(0, -1).join('.');
            editFormFields.FileSize = file.size;
            editFormFields.FileType = file.type;
            editFormFields.Extension = file.name.split('.').pop();
            editFormFields.Major = 0;
            editFormFields.Minor = 0;
            editFormFields.Revision = 0;
            editFormFields.IsCurrentVersion = 1;
            editFormFields.Enable = 1;
        }
    }
    const spanMethod = ({ row, column, rowIndex, columnIndex }) => {
        //合并单元格
        // if (columnIndex === 0) {
        //     if (rowIndex % 2 === 0) {
        //         return { rowspan: 1, colspan: 2 };
        //     } else {
        //         return { rowspan: 0, colspan: 0 };
        //     }
        // }
    }
    //监听表单输入，做实时计算
    //watch(() => editFormFields.字段,(newValue, oldValue) => {	})
    //对外暴露数据
    defineExpose({})
</script>
<style lang="less" scoped>
    .btn-box {
        display: flex;
        flex-direction: column;
        gap: 12px;
        padding: 8px 8px 8px 8px;
    }
    .full-btn {
        width: 100%;
        margin: 0 !important;   /* 强制覆盖 El 默认 margin */
    }
    .my-title {
        display: flex;
        align-items: center;
        gap: 6px;
        font-size: 16px;
        font-weight: 500;
        color: #0e0e0e;   /* 想再深就 #000 */
    }
</style>
