/*****************************************************************************************
**  Author:xx
**  xx
**完整文档见：http://v2.volcore.xyz/document/api 【代码生成页面ViewGrid】
**常用示例见：http://v2.volcore.xyz/document/vueDev
**后台操作见：http://v2.volcore.xyz/document/netCoreDev
*****************************************************************************************/
//此js文件是用来自定义扩展业务代码，可以扩展一些自定义页面或者重新配置生成的代码
import gridHeader from './Sys_PrintOptionsGridHeader.vue'
let extension = {
  components: {
    //查询界面扩展组件
    gridHeader: gridHeader,
    gridBody: '',
    gridFooter: '',
    //新建、编辑弹出框扩展组件
    modelHeader: '',
    modelBody: '',
    modelFooter: ''
  },
  tableAction: '', //指定某张表的权限(这里填写表名,默认不用填写)
  buttons: { view: [], box: [], detail: [] }, //扩展的按钮
  methods: {
    //下面这些方法可以保留也可以删除
    onInit() {  //框架初始化配置前，
      this.setFiexdSearchForm(true);
      let data = []
      this.http.get("api/Sys_PrintOptions/getSelect", {}, false).then(result => {
        data.push(...result);
      })

      this.searchFormOptions.forEach(options => {
        options.forEach(op => {
          if (op.field == 'TableName') {
            op.type = "select";
            op.data = data;
          }
        })
      })

      this.editFormOptions.forEach(options => {
        options.forEach(op => {
          if (op.field == 'TableName') {
            op.type = "select";
            op.data = data;
            op.onChange = (val, items) => {
              this.editFormFields.TableCNName = items.find(c => { return c.key == val }).value
            }
          }
        })
      })

      if (this.buttons.some(c => { return c.value == 'Add' || c.value == 'Update' })) {
        this.columns.push({
          title: "操作",
          field: "op",
          align: "center",
          width: 120,
          render: (h, { row, column, index }) => {
            return (
              <div>
                <el-button
                  link
                  onClick={($e) => {
                    this.$refs.gridHeader.open(row,row.TableName);
                  }}
                  style="color: #2196F3;cursor: pointer;margin-right:10px"
                >
                  <Setting style="width: 1em; height: 1em; margin-right: 2px" />
                  {this.$ts('模板设置')}
                </el-button>
                <el-button
                  link
                  onClick={($e) => {
                    this.$tabs.open({
                      text: row.TableCNName,
                      path: '/'+row.TableName,
                    });
                  
                  }}
                  style="margin-left:1px;color: #2196F3;cursor: pointer;"
                >
                  <Printer style="width: 1em; height: 1em; margin-right: 2px" />{this.$ts('打印')}
                </el-button>
              </div>
            )
          }
          // formatter: (row) => {
          //   return '<a style="cursor: pointer;color: #2196F3;">'+this.$ts('模板设置')+'</a>'
          // },
          // click: (row) => {
          //  // console.log( this.$refs.gridHeader)
          //   this.$refs.gridHeader.open(row,row.TableName);
          // }
        })
      }


    },
    onInited() {
      // this.height = this.height - 90;
      // if (this.$global.labelPosition != 'top') {
      //   this.height += 20;
      // }
      //框架初始化配置后
      //如果要配置明细表,在此方法操作
      //this.detailOptions.columns.forEach(column=>{ });
    },
    searchBefore(param) {
      //界面查询前,可以给param.wheres添加查询参数
      //返回false，则不会执行查询
      return true;
    },
    searchAfter(result) {
      //查询后，result返回的查询数据,可以在显示到表格前处理表格的值
      return true;
    },
    addBefore(formData) {
      //新建保存前formData为对象，包括明细表，可以给给表单设置值，自己输出看formData的值
      return true;
    },
    updateBefore(formData) {
      //编辑保存前formData为对象，包括明细表、删除行的Id
      return true;
    },
    rowClick({ row, column, event }) {
      //查询界面点击行事件
      // this.$refs.table.$refs.table.toggleRowSelection(row); //单击行时选中当前行;
    },
    modelOpenAfter(row) {
      //点击编辑、新建按钮弹出框后，可以在此处写逻辑，如，从后台获取数据
      //(1)判断是编辑还是新建操作： this.currentAction=='Add';
      //(2)给弹出框设置默认值
      //(3)this.editFormFields.字段='xxx';
      //如果需要给下拉框设置默认值，请遍历this.editFormOptions找到字段配置对应data属性的key值
      //看不懂就把输出看：console.log(this.editFormOptions)
    }
  }
};
export default extension;
