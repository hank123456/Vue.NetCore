// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'CatalogID',
        footer: "Foots",
        cnName: '文件分类',
        name: 'DMS_FileCatalog',
        url: "/DMS_FileCatalog/",
        sortName: "CatalogName"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {"CatalogCode":"","CatalogName":"","ParentId":[],"Enable":"","Remarks":""};
    const editFormOptions = [[{"title":"分类编码","required":true,"field":"CatalogCode"}],
                              [{"title":"分类名","required":true,"field":"CatalogName"}],
                              [{"dataKey":"文件分类","data":[],"title":"上级分类","field":"ParentId","type":"cascader"}],
                              [{"dataKey":"enable","data":[],"title":"是否可用","field":"Enable","type":"radio"}],
                              [{"title":"备注","field":"Remarks","type":"textarea"}]];
    const searchFormFields = {};
    const searchFormOptions = [];
    const columns = [{field:'CatalogID',title:'分类id',type:'guid',width:220,hidden:true,require:true,align:'left'},
                       {field:'CatalogName',title:'分类名',type:'string',width:120,require:true,align:'left',sort:true},
                       {field:'CatalogCode',title:'分类编码',type:'string',link:true,width:120,require:true,align:'left'},
                       {field:'CatalogType',title:'分类型号',type:'string',width:120,hidden:true,align:'left'},
                       {field:'ParentId',title:'上级分类',type:'string',bind:{ key:'文件分类',data:[]},width:110,hidden:true,align:'left'},
                       {field:'Enable',title:'是否可用',type:'int',bind:{ key:'enable',data:[]},width:110,align:'left'},
                       {field:'Remarks',title:'备注',type:'string',width:120,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',width:220,align:'left'},
                       {field:'ModifyID',title:'修改者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'修改者',type:'string',width:100,align:'left'},
                       {field:'ModifyDate',title:'修改时间',type:'string ',width:220,align:'left'}];
    const detail ={columns:[]};
    const details = [];

    return {
        table,
        key,
        tableName,
        tableCNName,
        newTabEdit,
        editFormFields,
        editFormOptions,
        searchFormFields,
        searchFormOptions,
        columns,
        detail,
        details
    };
}