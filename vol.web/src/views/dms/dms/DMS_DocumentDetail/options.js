// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'Id',
        footer: "Foots",
        cnName: '文件详情',
        name: 'DMS_DocumentDetail',
        url: "/DMS_DocumentDetail/",
        sortName: "CreateDate"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {"FileName":"","Extension":"","Major":"","Minor":"","Revision":"","Hash":"","MinioBucket":"","MinioObject":""};
    const editFormOptions = [[{"title":"文件名","required":true,"field":"FileName"},
                               {"title":"扩展名","field":"Extension","type":"text"},
                               {"title":"主版本号","required":true,"field":"Major","type":"text"},
                               {"title":"小版本号","required":true,"field":"Minor","type":"text"},
                               {"title":"修正版本号","required":true,"field":"Revision","type":"text"}],
                              [{"title":"哈希值","field":"Hash","disabled":true}],
                              [{"title":"Minio桶","field":"MinioBucket","disabled":true}],
                              [{"title":"Minio对象","field":"MinioObject","disabled":true}]];
    const searchFormFields = {"FileName":"","Extension":""};
    const searchFormOptions = [[{"title":"文件名","field":"FileName"},{"title":"扩展名","field":"Extension"}]];
    const columns = [{field:'Id',title:'详情id',type:'guid',width:220,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'DocId',title:'文件id',type:'guid',width:220,require:true,align:'left',sort:true},
                       {field:'FileName',title:'文件名',type:'string',link:true,sort:true,width:180,require:true,align:'left'},
                       {field:'FileSize',title:'文件大小',type:'long',width:120,align:'left'},
                       {field:'Extension',title:'扩展名',type:'string',width:110,align:'left'},
                       {field:'ContentType',title:'类型',type:'string',width:120,align:'left'},
                       {field:'Major',title:'主版本号',type:'int',width:110,require:true,align:'left'},
                       {field:'Minor',title:'小版本号',type:'int',width:110,require:true,align:'left'},
                       {field:'Revision',title:'修正版本号',type:'int',width:110,require:true,align:'left'},
                       {field:'Version',title:'版本号',type:'string',width:110,align:'left'},
                       {field:'Hash',title:'哈希值',type:'string ',width:220,readonly:true,align:'left'},
                       {field:'MinioBucket',title:'Minio桶',type:'string',width:120,readonly:true,align:'left'},
                       {field:'MinioObject',title:'Minio对象',type:'string',width:220,readonly:true,align:'left'},
                       {field:'CurrentState',title:'当前状态',type:'short',width:110,align:'left'},
                       {field:'AuditId',title:'审核人id',type:'int',width:110,align:'left'},
                       {field:'AuditStatus',title:'审核状态',type:'int',width:110,align:'left'},
                       {field:'Auditor',title:'审核人',type:'string',width:110,align:'left'},
                       {field:'AuditDate',title:'审核时间',type:'datetime',width:150,align:'left',sort:true},
                       {field:'AuditReason',title:'审核理由',type:'string',width:220,align:'left'},
                       {field:'DownloadCount',title:'下载次数',type:'long',width:120,align:'left'},
                       {field:'IsDeleted',title:'是否删除',type:'bool',width:220,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',sort:true,width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',sort:true,width:220,align:'left'},
                       {field:'ModifyID',title:'修改者id',type:'int',width:100,align:'left'},
                       {field:'Modifier',title:'修改者',type:'string',width:100,align:'left'},
                       {field:'ModifyDate',title:'修改时间',type:'string ',sort:true,width:220,align:'left'},
                       {field:'Enable',title:'是否启用',type:'short',width:110,align:'left'}];
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