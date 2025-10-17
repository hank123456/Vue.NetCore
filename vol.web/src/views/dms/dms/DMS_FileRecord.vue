<!--
 *Author：jxx
 *Contact：xx
 *业务请在@/extension/dms/dms/DMS_FileRecord.jsx或DMS_FileRecord.vue文件编写
 *新版本支持vue或【表.jsx]文件编写业务,文档见:https://v3.volcore.xyz/docs/view-grid、https://v3.volcore.xyz/docs/web
 -->
<template>
    <div class="tree">
        <div class="toggle-button">
            <el-icon @click="toggleLeftPanel" class="toggle-icon">
                <i :class="isLeftPanelVisible ? 'el-icon-arrow-left' : 'el-icon-d-arrow-right'"></i>
            </el-icon>
        </div>
        <div class="left" v-if="isLeftPanelVisible">
            <FileTree ref="treRef" @node-click="nodeClick"></FileTree>
        </div>

        <div class= "right">
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
        </div>

    </div>
</template>
<script setup lang="jsx">
    import FileTree from './DMS_FileRecord/DMS_FileTree.vue'
    import extend from "@/extension/dms/dms/DMS_FileRecord.jsx";
    import viewOptions from './DMS_FileRecord/options.js'
    import { ref, reactive, getCurrentInstance,nextTick, watch, onMounted } from "vue";
    const grid = ref(null);
    const { proxy } = getCurrentInstance()
    //http请求，proxy.http.post/get
    const { table, editFormFields, editFormOptions, searchFormFields, searchFormOptions, columns, detail, details } = reactive(viewOptions())

    let gridRef;//对应[表.jsx]文件中this.使用方式一样
    const isLeftPanelVisible = ref(true)

    const toggleLeftPanel = () => {
        isLeftPanelVisible.value = !isLeftPanelVisible.value
    }
    //生成对象属性初始化
    const onInit = async ($vm) => {
        gridRef = $vm;
        //与jsx中的this.xx使用一样，只需将this.xx改为gridRef.xx
        //更多属性见：https://v3.volcore.xyz/docs/view-grid
        gridRef.load = false;

    }
    //生成对象属性初始化后,操作明细表配置用到
    const onInited = async () => {
        gridRef.detailOptions.buttons.unshift({//这里可以使用push添加最后一个位置
            name: '添加文件', //按钮名称
            icon: 'el-icon-document', //按钮图标:组件示例->图标
            //primary、success、warning、error、info、text、danger
            type: 'primary',
            plain: true,
            onClick: () => { }
        });   

        const isAdd=gridRef.currentAction=='Add';//判断是否为新建操作
        //隐藏明细表按钮
        gridRef.detailOptions.buttons.forEach((btn) => {
            if (btn.name == '添加行') {
                btn.hidden = true;
                //或者设置只读
                //btn.readonly=true;
            }
        });
        const isDelete=gridRef.currentAction=='Delete';//判断是否为删除操作
        //隐藏明细表按钮
        gridRef.detailOptions.buttons.forEach((btn) => {
            if (btn.name == '删除行') {
                btn.hidden = true;
                //或者设置只读
                //btn.readonly=true;
            }
        });
    }
    let catalogIds
    let nodes
    //左边树点击事件
    const nodeClick = (ids, nodes) => {
        //左边树节点点击事件
        //左边树节点的甩有子节点，用于查询数据
        catalogIds = ids.join(',')
        //左侧树选中节点的所有父节点,用于新建时设置级联的默认值
        console.log("catalogIds = ", catalogIds)
        nodes = nodes
        nextTick(() => {
            //调用查询方法
            gridRef.search()
        })
    }


    const searchBefore = async (param) => {
        //界面查询前,可以给param.wheres添加查询参数
        //返回false，则不会执行查询
        if (catalogIds) {
            param.wheres.push({
            name: 'CatalogID', //查询分类字段，注意字段大小写
            value: catalogIds,
            displayType: 'selectList'
            })
        }
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
<style scoped lang="less">
.tree {
  display: flex;
  .left {
    width: 200px;
  }
  .right {
    flex: 1;
    width: 0;
  }
  .toggle-button {
    top: 10px;
    padding-top: 10px;
    left: 0px;
    z-index: 1000;
    border-radius: 4px;
    cursor: pointer;
    .toggle-icon {
      font-size: 16px;
      padding: 5px;
    }
  }
}
</style>

