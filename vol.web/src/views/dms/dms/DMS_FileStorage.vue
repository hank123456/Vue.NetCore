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
</template>
<script setup lang="jsx">
    import extend from "@/extension/dms/dms/DMS_FileStorage.jsx";
    import viewOptions from './DMS_FileStorage/options.js'
    import { ref, reactive, getCurrentInstance, watch, onMounted } from "vue";
    const grid = ref(null);
    const { proxy } = getCurrentInstance()
    //http请求，proxy.http.post/get
    const { table, editFormFields, editFormOptions, searchFormFields, searchFormOptions, columns, detail, details } = reactive(viewOptions())

    let gridRef;//对应[表.jsx]文件中this.使用方式一样

    //生成对象属性初始化
    const onInit = async ($vm) => {
        gridRef = $vm;

        //与jsx中的this.xx使用一样，只需将this.xx改为gridRef.xx
        //更多属性见：https://v3.volcore.xyz/docs/view-grid
        gridRef.buttons = gridRef.buttons.filter(
            (btn) => ![ '新建', 'Add', 'Edit', 'Delete'].includes(btn.name));//'新建','编辑','删除'
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
            width: 100,
            align: 'center', // 'left',
            fixed:'right',//固定到最右边，也可以设置为left固定到左边
            render: (h, { row, column, index }) => {
                return (
                <div>
                    <el-button
                    onClick={($e) => {
                        confirmDownload(row, column, index, $e);
                    }}
                    type="primary" plain   style="height:26px; padding: 10px !important;" >
                    下载
                    </el-button>
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

    const confirmDownload = async (row, column, index, $e) => {
        if (!row) return;
        const ext = row.Extension || '';
        let fileName = `${row.FileName || 'file'}.${ext}`;
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
                        //formData.mainData.IsCurrentVersion = 1; // 新建文件肯定是当前最新版本
                        //formData.mainData.Enable = 1;
                        // formData.mainData.AuditStatus = 0;

                        // 清除选择的文件
                        gridRef.selectedFiles = [];

                        console.log("formData.mainData.MinioBucket ==",formData.mainData.MinioBucket);
                        console.log("formData.mainData.MinioObject ==",formData.mainData.MinioObject);

                        console.log('formData result:', formData);
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
        console.log('updateBefore formData:', formData);
        return true;
    }
    const rowClick = ({ row, column, event }) => {
        //查询界面点击行事件
        // grid.value.toggleRowSelection(row); //单击行时选中当前行;
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
</style>
