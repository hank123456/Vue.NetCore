<template>  
    <vol-box :lazy="true" v-model="model" title="选择文件" :width="900" :padding="0">  
      <div>  
        <!-- 搜索配置 -->  
        <div class="search-form">  
          <label>文件名称：</label>  
          <el-input style="width: 150px" v-model="FileName"></el-input>  
          <label style="margin-left: 10px">文件扩展名：</label>  
          <el-input style="width: 100px" v-model="Extension"></el-input>  
          <el-button size="small" type="primary" @click="search">搜索</el-button>  
        </div>  
        <!-- 表格数据 -->  
        <vol-table  
          ref="tableRef"  
          :loadKey="true"  
          :columns="columns"  
          :pagination-hide="false"  
          :height="370"  
          :url="url"  
          @loadBefore="loadBefore"
          @rowClick="onRowClick" 
        ></vol-table>  
      </div>  
      <template #footer>  
        <el-button type="primary" @click="detailSelectClick" size="small">选择数据</el-button>  
      </template>  
    </vol-box>  
</template>

<script setup>  
import { ref, nextTick, getCurrentInstance } from 'vue'  
const { proxy } = getCurrentInstance()  
const tableRef = ref()  
const model = ref(false)  
// 调用 DMS_File 的接口  
const url = ref('api/DMS_File/getPageData')  
const FileName = ref('') // 文件名称  
const Extension = ref('') // 文件扩展名  
  
// 表格列配置  
const columns = ref([  
  { field: 'FileId', title: '文件ID', type: 'string', width: 90, hidden: true },  
  { field: 'FileName', title: '文件名称', type: 'string', width: 200 },  
  { field: 'Extension', title: '扩展名', type: 'string', width: 80 },  
  { field: 'FileSize', title: '文件大小(字节)', type: 'int', width: 120 },  
  { field: 'ContentType', title: '内容类型', type: 'string', width: 150 },  
  { field: 'Creator', title: '创建人', type: 'string', width: 90 },  
  { field: 'CreateDate', title: '创建时间', type: 'datetime', width: 150 }  
])  
const onRowClick = ({row}) => {
  tableRef.value.toggleRowSelection(row)
}
// 弹出框打开  
const open = () => {  
  model.value = true  
  nextTick(() => {  
    tableRef.value?.load(null, true)  
  })  
}  
  
const emit = defineEmits(['onSelect'])  
  
// 选择数据按钮  
const detailSelectClick = () => {  
  let rows = tableRef.value.getSelected()  
  if (!rows.length) {  
    return proxy.$message.error('请选择数据')  
  }  
  emit('onSelect', rows)  
  model.value = false  
}  
  
// 查询点击事件  
const search = () => {  
  tableRef.value.load(null, true)  
}  
  
// 表数据加载设置查询条件  
const loadBefore = (param, callBack) => {  
  param.wheres = [  
    { name: 'FileName', value: FileName.value, displayType: 'like' },  
    { name: 'Extension', value: Extension.value, displayType: 'like' }  
  ]  
  callBack(true)  
}  
  
// 暴露方法给按钮使用  
defineExpose({  
  open  
})  
</script>

<style lang="less" scoped>  
.search-form {  
  display: flex;  
  padding: 10px;  
  line-height: 34px;  
  button {  
    margin-left: 10px;  
  }  
}  
</style>