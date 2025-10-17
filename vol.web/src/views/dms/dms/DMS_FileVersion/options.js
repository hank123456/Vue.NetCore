// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'FileVersionId',
        footer: "Foots",
        cnName: '归档文件明细',
        name: 'DMS_FileVersion',
        url: "/DMS_FileVersion/",
        sortName: "CreateDate"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {};
    const editFormOptions = [];
    const searchFormFields = {};
    const searchFormOptions = [];
    const columns = [{field:'FileVersionId',title:'文件版本id',type:'guid',width:220,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'FileRecordId',title:'文件归档id',type:'guid',width:220,require:true,align:'left',sort:true},
                       {field:'FileRecordName',title:'文件名',type:'string',width:220,require:true,align:'left'},
                       {field:'Major',title:'主版本',type:'int',width:110,require:true,align:'left'},
                       {field:'Minor',title:'小版本',type:'int',width:110,require:true,align:'left'},
                       {field:'Version',title:'版本',type:'string',width:110,align:'left'},
                       {field:'FileSize',title:'文件大小',type:'long',width:120,require:true,align:'left'},
                       {field:'ContentType',title:'文件类型',type:'string',width:120,align:'left'},
                       {field:'Extension',title:'文件后缀',type:'string',width:110,require:true,align:'left'},
                       {field:'Hash',title:'Hash值',type:'string ',width:220,require:true,align:'left'},
                       {field:'ChangeReason',title:'更改原因',type:'string',width:220,align:'left'},
                       {field:'MinioBucket',title:'Minio桶',type:'string',width:120,require:true,align:'left'},
                       {field:'MinioObject',title:'Minio对象',type:'string',width:220,require:true,align:'left'},
                       {field:'AuditId',title:'审核者id',type:'int',width:110,align:'left'},
                       {field:'AuditStatus',title:'审核状态',type:'int',width:110,require:true,align:'left'},
                       {field:'Auditor',title:'审核者',type:'string',width:120,align:'left'},
                       {field:'AuditDate',title:'审核时间',type:'string ',width:220,align:'left'},
                       {field:'AuditReason',title:'审批记录',type:'string',width:220,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',width:220,align:'left'},
                       {field:'ModifyID',title:'修改者id',type:'int',width:100,align:'left'},
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