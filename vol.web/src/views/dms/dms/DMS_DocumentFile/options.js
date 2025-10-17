// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'Id',
        footer: "Foots",
        cnName: '文档文件关联',
        name: 'DMS_DocumentFile',
        url: "/DMS_DocumentFile/",
        sortName: "CreateDate"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {"Sequence":"","FileId":"","FileType":""};
    const editFormOptions = [[{"title":"序号","required":true,"field":"Sequence","type":"number"},
                               {"dataKey":"FileType","data":[],"title":"文件类型","required":true,"field":"FileType","type":"select"},
                               {"dataKey":"FileList","data":[],"title":"文件id","required":true,"field":"FileId","type":"select"}]];
    const searchFormFields = {};
    const searchFormOptions = [];
    const columns = [{field:'Id',title:'ID',type:'guid',width:220,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'Sequence',title:'序号',type:'int',width:110,require:true,align:'left',sort:true},
                       {field:'DocumentId',title:'文档id',type:'guid',width:220,hidden:true,require:true,align:'left'},
                       {field:'FileId',title:'文件id',type:'guid',bind:{ key:'FileList',data:[]},width:220,require:true,align:'left'},
                       {field:'FileType',title:'文件类型',type:'string',bind:{ key:'FileType',data:[]},width:110,require:true,align:'left'},
                       {field:'Enable',title:'文件可用',type:'short',width:110,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',width:220,align:'left'},
                       {field:'ModifyID',title:'编辑者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'编辑者',type:'string',width:100,hidden:true,align:'left'},
                       {field:'ModifyDate',title:'编辑时间',type:'string ',width:220,hidden:true,align:'left'}];
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