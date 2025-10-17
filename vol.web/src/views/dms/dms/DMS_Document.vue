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
               :modelOpenAfter="modelOpenAfter"
               :detailSortEnd="detailSortEnd"
               >
        <!-- 自定义组件数据槽扩展，更多数据槽slot见文档 -->
        <template #gridHeader>
        </template>
    </view-grid>

    <!-- 引入文件选择组件 -->  
    <select-file ref="fileRef" @onSelect="onFileSelect"></select-file>
</template>
<script setup lang="jsx">
    import extend from "@/extension/dms/dms/DMS_Document.jsx";
    import viewOptions from './DMS_Document/options.js'
    import { ref, reactive, getCurrentInstance, watch, onMounted } from "vue";

    import SelectFile from './DMS_Document/SelectFile.vue'  
    const fileRef = ref()  

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

        //启用明细表排序
        gridRef.detailOptions.sortable = true
        gridRef.detailOptions.pagination.sortName = "Sequence";  //明细表排序字字段
        gridRef.detailOptions.pagination.order = "asc"; //明细表排序方式desc或者asc

        // 在明细表按钮中添加"选择文件"按钮  
        gridRef.detailOptions.buttons.unshift({  
            name: '选择文件',  
            icon: 'el-icon-plus',  
            hidden: false,  
            onClick: () => {  
            fileRef.value.open()  
            }  
        })  
    }
    // 选择文件后的回调  
    const onFileSelect = (rows) => {  
        // 转换数据格式与 DMS_DocumentFile 字段一致  
        rows = rows.map((file) => {  
            return {  
            FileId: file.FileId, 
            Enable: 1,
            // 可以添加其他需要显示的字段  
            }  
        })  
        // 写入明细表  
        gridRef.getTable().rowData.unshift(...rows) 
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
        grid.value.toggleRowSelection(row); //单击行时选中当前行;
    }
    const modelOpenBefore = async (row) => {//弹出框打开后方法
        return true;//返回false，不会打开弹出框
    }
    const modelOpenAfter = (row) => {
        //弹出框打开后方法,设置表单默认值,按钮操作等
        if (gridRef.currentAction === 'Add') {  
            // 初始化版本控制字段  
            editFormFields.Version = "V0.1";
            editFormFields.Status = 0; //草稿
            editFormFields.ApproveStatus = 0; //未审核
            editFormFields.Enable = 1;
        }

    }
    const detailSortEnd = (rows, newIndex, oldIndex, table) => {
        rows.forEach((x, index) => {
            x.Sequence = index + 1
        })
    }
    //监听表单输入，做实时计算
    watch(() => editFormFields.DocType,(newValue, oldValue) => {
    
    })
    //对外暴露数据
    defineExpose({})
</script>
<style lang="less" scoped>
</style>
