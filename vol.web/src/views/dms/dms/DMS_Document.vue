<!--
 *Author：jxx
 *Contact：xx
 *业务请在@/extension/dms/dms/DMS_Document.jsx或DMS_Document.vue文件编写
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
               :modelOpenAfter="modelOpenAfter">
        <!-- 自定义组件数据槽扩展，更多数据槽slot见文档 -->
        <template #gridHeader>
        </template>
    </view-grid>
</template>
<script setup lang="jsx">
    import extend from "@/extension/dms/dms/DMS_Document.jsx";
    import viewOptions from './DMS_Document/options.js'
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
        //添加上传按钮
        // gridRef.detailOptions.buttons.unshift({
        //     name: '选择文件', 
        //     icon: 'el-icon-document', 
        //     //primary、success、warning、error、info、text、danger
        //     type: 'primary',
        //     plain: false,
        //     onClick: () => {
        //         const input = document.createElement('input');
        //         input.type = 'file';
        //         //input.multiple = true;
        //         input.onchange = (e) => {
        //             const files = e.target.files;
        //             if (!files || files.length === 0) {
        //                 return; // 如果没有选择文件，则不执行任何操作
        //             }
        //             //gridRef.add(); // 打开新建窗口
        //             const formData = new FormData();
        //             for (let i = 0; i < files.length; i++) {
        //                 console.log(files[i]);
        //                 formData.append('fileInput', files[i]);
        //             }
        //             console.log(formData);
        //             proxy.http.post('/api/DMS_DocumentDetail/upload', formData,true,
        //                 {headers: { 'Content-Type': 'multipart/form-data' }
        //             })
        //                 .then(apiResult => {
        //                     if (apiResult.status) {
        //                         proxy.$message.success('成功上传${files.length} 个文件');
        //                         gridRef.load(); // 刷新表格数据显示新记录
        //                     } else {
        //                         proxy.$message.error(apiResult.message || '上传失败');
        //                     }
        //                 }).catch(err => {
        //                     console.error(err);
        //                     proxy.$message.error('上传请求出错');
        //                 });
                    
                    
        //             // 清除input的值，以便可以再次选择同一个文件
        //             input.value = '';
        //         };
        //         input.click();
        //      }
        // });
        // //隐藏原有按钮
        // gridRef.detailOptions.buttons.forEach((btn) => {
        //     if ( btn.name == '删除行'||btn.name == '导入') {
        //         btn.hidden = true;
        //         //或者设置只读
        //         //btn.readonly=true;
        //     }
        // });
    }
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
        return true;
    }
    const updateBefore = async (formData) => {
        //编辑保存前formData为对象，包括明细表、删除行的Id
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
        //弹出框打开后方法,设置表单默认值,按钮操作等
    }
    //监听表单输入，做实时计算
    //watch(() => editFormFields.字段,(newValue, oldValue) => {	})
    //对外暴露数据
    defineExpose({})
</script>
<style lang="less" scoped>
</style>
